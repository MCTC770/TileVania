﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour {

	//todo: re-enable jumping while falling (not a fall after a jump), if player has more then 1 character

	public bool playerDeath = false;
	public bool grounded = false;

	[SerializeField] float movementSpeed = 1200f;
	[SerializeField] float runSpeed = 800f;
	[SerializeField] float climbSpeed = 500f;
	[SerializeField] float jumpHeight = 5000f;
	[SerializeField] float maxJumpTime = 0.135f;
	[SerializeField] float fallGravity = 100f;
	[SerializeField] float ladderWalkSlowdown = 4f;
	[SerializeField] float deathAnimationUplift = 0.1f;
	[SerializeField] float deathGravity = 5f;
	[SerializeField] float invokeDeathFall = 0.5f;
	[SerializeField] float deathSequenceTime = 0.8f;
	[SerializeField] float raycastLength = 1.246f;
	[SerializeField] float raycastLengthAddValue = 0.9f;
	[SerializeField] int maxJumps = 1;
	[SerializeField] int currentMaxJumps;
	[SerializeField] LayerMask layer;
	[SerializeField] Animator characterAnimator;
	[SerializeField] CapsuleCollider2D playerCollider;
	[SerializeField] PlayerPickup playerPickups;
	[SerializeField] GameObject[] virtualCameras;
	[SerializeField] List<GameObject> playerList = new List<GameObject>();

	float jumpInput = 0;
	float jumpTime = 0;
	float setGravityScale;
	float movementFixSpeed;
	float movementSpeedAtStart;
	bool jumpEnd = false;
	bool jumpNotStarted = true;
	bool canJump = true;
	bool currentJump = false;
	bool climbAvailable = false;
	bool noJumpWhenFalling = true;
	bool spawnedPlayerPickup = false;
	bool enemyDefeated = false;
	int jumpCounter = 0;
	Rigidbody2D characterRigidbody;
	GameSession gameSession;
	LayerMask playerPickupsLayer = 17;
	LayerMask hazardLayer = 14;
	LayerMask enemyLayer = 13;
	LayerMask ladderLayer = 10;
	LayerMask foregroundLayer = 9;
	CircleCollider2D PlayerFeetTrigger;
	RaycastHit2D checkPlayerPickupRaycastHit;

	// Use this for initialization
	void Start () {
		PlayerFeetTrigger = GetComponent<CircleCollider2D>();
		characterRigidbody = GetComponent<Rigidbody2D>();
		characterAnimator.SetBool("Running", false);
		setGravityScale = characterRigidbody.gravityScale;
		movementFixSpeed = movementSpeed;
		gameSession = FindObjectOfType<GameSession>();
		currentMaxJumps = maxJumps;
	}
	
	void Update ()
	{
		for (var i = 0; i <= (virtualCameras.Length - 1); i++)
		{
			print("i: " + i + " maxJumps - 1: " + (maxJumps - 1));
			if (i == (maxJumps - 1))
			{
				virtualCameras[i].SetActive(true);
			}
			else
			{
				virtualCameras[i].SetActive(false);
			}
		}

		CheckForPlayerDeath();
	}

	private bool CheckRaycastHit(string directionCheck)
	{
		if (directionCheck == "top")
		{
			Vector2 position = transform.position;
			Vector2 direction = Vector2.up * (raycastLength + (raycastLengthAddValue * (maxJumps - 1)));
			float distance = direction.y;

			Debug.DrawRay(position, direction, Color.green);

			if (Physics2D.Raycast(position, direction, distance, layer))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		else
		{
			return false;
		}
	}

	private void CheckForPlayerDeath()
	{
		if (playerDeath == false)
		{
			ControllerInputHandler();
		}
		else
		{
			playerCollider.enabled = false;
			characterRigidbody.gravityScale = 1f;
			characterRigidbody.velocity = Vector2.up * deathAnimationUplift;
			Invoke("DeathFall", invokeDeathFall);

			characterAnimator.SetBool("Climbing", false);
			characterAnimator.SetBool("Running", false);
			characterAnimator.SetBool("Dying", true);
			Invoke("LoseALive", deathSequenceTime);
		}
	}

	private void LoseALive()
	{
		gameSession.playerLives -= 1;
		gameSession.ProcessPlayerDeath();
	}

	private void DeathFall()
	{
		characterRigidbody.gravityScale = deathGravity;
	}

	void ControllerInputHandler()
	{
		HorizontalMovementInput();
		JumpInput();
		ClimbLadder();
		DebugControlls();
	}

	private void HorizontalMovementInput()
	{
		float horizontalMovementInput = CrossPlatformInputManager.GetAxis("Horizontal");
		float runInput = CrossPlatformInputManager.GetAxis("Run");
		float walkSpeed;
		if (runInput == 1f)
		{
			walkSpeed = runSpeed;
		}
		else
		{
			walkSpeed = movementSpeed;
		}
		float movementSpeedTimesInput = walkSpeed * horizontalMovementInput * Time.deltaTime;
		characterRigidbody.velocity = Vector2.right * movementSpeedTimesInput;

		CharacterRunAnimation(horizontalMovementInput);
		CharacterTurningWhenWalking(horizontalMovementInput);
	}

	private void CharacterRunAnimation(float horizontalMovementInput)
	{
		if (horizontalMovementInput != 0)
		{
			characterAnimator.SetBool("Running", true);
		}
		else
		{
			characterAnimator.SetBool("Running", false);
		}

		/*if (maxJumps == 1)
		{
			virtualCameras[0].SetActive(true);
			virtualCameras[1].SetActive(false);
			virtualCamera3.SetActive(false);
			virtualCamera4.SetActive(false);
			virtualCamera5.SetActive(false);
		}
		else if (maxJumps == 2)
		{
			virtualCamera1.SetActive(false);
			virtualCamera2.SetActive(true);
			virtualCamera3.SetActive(false);
			virtualCamera4.SetActive(false);
			virtualCamera5.SetActive(false);
		}
		else if (maxJumps == 3)
		{
			virtualCamera1.SetActive(false);
			virtualCamera2.SetActive(false);
			virtualCamera3.SetActive(true);
			virtualCamera4.SetActive(false);
			virtualCamera5.SetActive(false);
		}
		else if (maxJumps == 4)
		{
			virtualCamera1.SetActive(false);
			virtualCamera2.SetActive(false);
			virtualCamera3.SetActive(false);
			virtualCamera4.SetActive(true);
			virtualCamera5.SetActive(false);
		}
		else if (maxJumps == 5)
		{
			virtualCamera1.SetActive(false);
			virtualCamera2.SetActive(false);
			virtualCamera3.SetActive(false);
			virtualCamera4.SetActive(false);
			virtualCamera5.SetActive(true);
		}*/
	}

	private void CharacterTurningWhenWalking(float horizontalMovementInput)
	{
		if (horizontalMovementInput > 0)
		{
			transform.rotation = new Quaternion(transform.rotation.x, 0, transform.rotation.z, transform.rotation.w);
		}
		else if (horizontalMovementInput < 0)
		{
			transform.rotation = new Quaternion(transform.rotation.x, 180, transform.rotation.z, transform.rotation.w);
		}
	}

	private void JumpInput()
	{
		if (jumpInput == 0 && !grounded)
		{
			if (jumpCounter >= maxJumps)
			{
				canJump = false;
			}
			characterRigidbody.gravityScale = fallGravity;
		}
		else
		{
			characterRigidbody.gravityScale = setGravityScale;
			if (grounded)
			{
				jumpCounter = 0;
			}
		}

		jumpEnd = CrossPlatformInputManager.GetButtonUp("Jump");

		if (jumpCounter < maxJumps && !canJump)
		{
			if (jumpEnd)
			{
				canJump = true;
			}
		}

		if (noJumpWhenFalling && !grounded) { }
		else if ((canJump && grounded && jumpNotStarted) || (canJump && !grounded))
		{
			jumpInput = CrossPlatformInputManager.GetAxis("Jump");
		}

		float jumpHeightTimesInput = jumpHeight * jumpInput * (1f/60f); //Time.deltaTime;

		CheckForHoldedJumpButton();
		DoJump(jumpHeightTimesInput);
	}

	private void CheckForHoldedJumpButton()
	{
		if (jumpInput == 1 && !grounded)
		{
			jumpNotStarted = false;
		}

		if (jumpEnd)
		{
			jumpNotStarted = true;
		}
	}

	private void DoJump(float jumpHeightTimesInput)
	{
		Vector2 jumpOver = new Vector2(transform.position.x, 0);

		if (jumpInput == 1f && jumpCounter < maxJumps)
		{
			characterRigidbody.velocity = new Vector2(characterRigidbody.velocity.x, 1f * jumpHeightTimesInput);
			jumpTime += Time.deltaTime;
			if (jumpTime >= (maxJumpTime * (Time.deltaTime * 60)))
			{
				canJump = false;
				jumpInput = 0;
			}
			currentJump = true;
			noJumpWhenFalling = false;
		}
		else if (jumpInput == 0f && currentJump == true)
		{
			jumpCounter += 1;
			jumpTime = 0f;
			currentJump = false;
		}
		else if (grounded)
		{
			jumpCounter = 0;
			noJumpWhenFalling = true;
			maxJumps = currentMaxJumps;
			if (maxJumps >= 2)
			{
				for(var i = 0; i <= maxJumps; i++)
				{
					if (i <= maxJumps - 2)
					{
						playerList[i].SetActive(true);
					}
				}
			}
			if (maxJumps >= 1)
				for (var i = playerList.Count; i >= currentMaxJumps; i--)
				{
					if (i - 1 > currentMaxJumps - 1)
					{
						playerList[i - 2].SetActive(false);
					}
				}
		}

		if (jumpInput == 1f && jumpCounter > 0 && spawnedPlayerPickup == false)
		{
			Instantiate(playerPickups, new Vector2 (transform.position.x, transform.position.y), Quaternion.identity);

			currentMaxJumps -= 1;

			if (currentMaxJumps < 1)
			{
				currentMaxJumps = 1;
			}

			spawnedPlayerPickup = true;

			for (var i = playerList.Count; i >= currentMaxJumps; i--)
			{
				if (playerList[i-1] != playerList[currentMaxJumps-1])
				{
					playerList[i-2].SetActive(false);
				}
				else { }
			}
		}
		else if (jumpInput == 0f && jumpCounter > 0 && spawnedPlayerPickup == true)
		{
			spawnedPlayerPickup = false;
		}
	}

	private void ClimbLadder()
	{
		float verticalMovementInput = CrossPlatformInputManager.GetAxis("Vertical");
		bool enterClimb = false;

		if (climbAvailable)
		{
			DoClimb(verticalMovementInput);
			CharacterClimbAnimation(verticalMovementInput);
			enterClimb = true;
		}
		else
		{
			if (enterClimb == true)
			{
				characterRigidbody.gravityScale = setGravityScale;
				enterClimb = false;
			}
			characterAnimator.SetBool("Climbing", false);
		}
	}

	private void DoClimb(float verticalMovementInput)
	{
		float movementSpeedTimesInput = climbSpeed * verticalMovementInput * Time.deltaTime;
		if (!grounded)
		{
			characterRigidbody.velocity = new Vector2(characterRigidbody.velocity.x / ladderWalkSlowdown, 1f * movementSpeedTimesInput);
		}
		else
		{
			characterRigidbody.velocity = new Vector2(characterRigidbody.velocity.x, 1f * movementSpeedTimesInput);
		}
		characterRigidbody.gravityScale = 0;
	}

	private void CharacterClimbAnimation(float verticalMovementInput)
	{
		if (verticalMovementInput != 0 && !grounded)
		{
			characterAnimator.SetBool("Climbing", true);
		}
		else if (grounded)
		{
			characterAnimator.SetBool("Climbing", false);
		}
	}

	private void DebugControlls()
	{
		if(Input.GetKey("p"))
		{
			transform.position = new Vector2(transform.position.x, transform.position.y + 0.2f);
		}
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.layer == ladderLayer)
		{
			climbAvailable = true;
		}

		if (collider.gameObject.layer == foregroundLayer)
		{
			jumpTime = 0;
			canJump = true;
			grounded = true;
		}

		if (collider.gameObject.layer == playerPickupsLayer)
		{
			//transform.position = new Vector2(transform.position.x, transform.position.y + 0.3f);
		}

		if (collider.gameObject.layer == enemyLayer && !collider.isTrigger)
		{
			Destroy(collider.gameObject);
			enemyDefeated = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collider)
	{
		if (collider.gameObject.layer == ladderLayer)
		{
			climbAvailable = false;
		}

		if (collider.gameObject.layer == foregroundLayer)
		{
			grounded = false;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.gameObject.layer == playerPickupsLayer && grounded && maxJumps < 10 && !CheckRaycastHit("top"))
		{
			Destroy(collision.collider.gameObject);
			maxJumps += 1;

			currentMaxJumps = maxJumps;
		}

		if (collision.collider.gameObject.layer == enemyLayer && !enemyDefeated)
		{
			playerDeath = true;
		}

		if (collision.collider.gameObject.layer == hazardLayer)
		{
			playerDeath = true;
		}

		enemyDefeated = false;
	}
}
