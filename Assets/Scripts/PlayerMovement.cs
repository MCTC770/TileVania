using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour {

	//todo: prevent jumping while falling (not a fall after a jump) -> Done!
	//todo: you can't jump, if you glide to ground from wall
	//todo: double jump currently doesn't work, when you reached maxJumpTime -> Done!
	//todo: tweak fallGravity for multi jumping

	public bool playerDeath = false;
	public LayerMask groundLayer;

	[SerializeField] float movementSpeed = 1200f;
	[SerializeField] float climbSpeed = 500f;
	[SerializeField] float jumpHeight = 5000f;
	[SerializeField] float maxJumpTime = 0.135f;
	[SerializeField] float fallGravity = 100f;
	[SerializeField] float ladderWalkSlowdown = 4f;
	[SerializeField] float deathAnimationUplift = 0.1f;
	[SerializeField] float deathGravity = 5f;
	[SerializeField] float invokeDeathFall = 0.5f;
	[SerializeField] float deathSequenceTime = 0.8f;
	[SerializeField] int maxJumps = 1;
	[SerializeField] int currentMaxJumps;
	[SerializeField] Animator characterAnimator;
	[SerializeField] CapsuleCollider2D playerCollider;
	[SerializeField] CapsuleCollider2D playerFeetCollider;
	[SerializeField] GameObject playerPickups;
	[SerializeField] List<GameObject> playerList = new List<GameObject>();

	float jumpInput = 0;
	float jumpTime = 0;
	float setGravityScale;
	float movementFixSpeed;
	bool jumpEnd = false;
	bool jumpNotStarted = true;
	bool grounded = false;
	bool canJump = true;
	bool currentJump = false;
	bool climbAvailable = false;
	bool noJumpWhenFalling = true;
	bool spawnedPlayerPickup = false;
	int jumpCounter = 0;
	Rigidbody2D characterRigidbody;
	GameSession gameSession;

	// Use this for initialization
	void Start () {
		characterRigidbody = GetComponent<Rigidbody2D>();
		characterAnimator.SetBool("Running", false);
		setGravityScale = characterRigidbody.gravityScale;
		movementFixSpeed = movementSpeed;
		gameSession = FindObjectOfType<GameSession>();
		currentMaxJumps = maxJumps;
	}
	
	// Update is called once per frame
	void Update () {
		IsGrounded();

		if (playerDeath == false)
		{
			ControllerInputHandler();
		}
		else
		{
			playerCollider.enabled = false;
			playerFeetCollider.enabled = false;
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

	bool IsGrounded()
	{
		Vector2 position = transform.position;
		Vector2 direction = Vector2.down * 0.5f;
		float distance = 1.0f;

		Debug.DrawRay(position, direction, Color.green);

		RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
		if (hit.collider != null)
		{
			return true;
		}

		return false;
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
		float movementSpeedTimesInput = movementSpeed * horizontalMovementInput * Time.deltaTime;
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

		float jumpHeightTimesInput = jumpHeight * jumpInput * Time.deltaTime;

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
			if (jumpTime >= maxJumpTime)
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
					if (playerList[i] == playerList[maxJumps - 2])
					{
						playerList[i].SetActive(true);
					}
				}
			}
		}

		if (jumpInput == 1f && jumpCounter > 0 && spawnedPlayerPickup == false)
		{
			Instantiate(playerPickups, new Vector2 (transform.position.x, transform.position.y - 1), Quaternion.identity);
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

	private void OnCollisionEnter2D(Collision2D collision)
	{
		ContactPoint2D newContact = collision.contacts[0];

		if ((newContact.collider.gameObject.name == "PlayerPickup" || newContact.collider.gameObject.name == "PlayerPickup(Clone)") && grounded && maxJumps < 10)
		{
			Destroy(newContact.collider.gameObject);
			maxJumps += 1;

			currentMaxJumps = maxJumps;
		}

		if (newContact.otherCollider == playerFeetCollider)
		{
			jumpTime = 0;
			canJump = true;
			grounded = true;
		}

		if (newContact.otherCollider == playerCollider && newContact.collider.gameObject.name == "Enemy")
		{
			playerDeath = true;
		}
		else if (newContact.otherCollider == playerFeetCollider && newContact.collider.gameObject.name == "Enemy")
		{
			Destroy(collision.gameObject);
		}

		if (newContact.collider.gameObject.name == "Hazards")
		{
			playerDeath = true;
		}
	}

	void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.otherCollider == playerFeetCollider)
		{
			grounded = false;
		}
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.name == "Ladders")
		{
			climbAvailable = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collider)
	{
		if (collider.gameObject.name == "Ladders")
		{
			climbAvailable = false;
		}
	}
}
