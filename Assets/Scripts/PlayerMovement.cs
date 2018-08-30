using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour {

	[SerializeField] float movementSpeed = 1200f;
	[SerializeField] float climbSpeed = 500f;
	[SerializeField] float jumpHeight = 5000f;
	[SerializeField] float maxJumpTime = 0.135f;
	[SerializeField] Animator characterAnimator;
	[SerializeField] CapsuleCollider2D playerCollider;
	[SerializeField] CapsuleCollider2D playerFeetCollider;

	float jumpInput = 0;
	float jumpTime = 0;
	float setGravityScale;
	float movementFixSpeed;
	bool grounded = false;
	bool isJumping = false;
	bool canJump = true;
	bool climbAvailable = false;
	Rigidbody2D characterRigidbody;

	// Use this for initialization
	void Start () {
		characterRigidbody = GetComponent<Rigidbody2D>();
		characterAnimator.SetBool("Running", false);
		setGravityScale = characterRigidbody.gravityScale;
		movementFixSpeed = movementSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		ControllerInputHandler();
	}

	void ControllerInputHandler()
	{
		HorizontalMovementInput();
		JumpInput();
		ClimbLadder();
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
		if (canJump == true)
		{
			jumpInput = CrossPlatformInputManager.GetAxis("Jump");
		}

		float jumpHeightTimesInput = jumpHeight * jumpInput * Time.deltaTime;

		DoJump(jumpHeightTimesInput);
	}

	private void DoJump(float jumpHeightTimesInput)
	{
		if (jumpInput == 1f)
		{
			characterRigidbody.AddRelativeForce(Vector2.up * jumpHeightTimesInput);
			jumpTime += Time.deltaTime;
			if (jumpTime >= maxJumpTime)
			{
				canJump = false;
				jumpInput = 0;
			}
		}
	}

	private void ClimbLadder()
	{
		float verticalMovementInput = CrossPlatformInputManager.GetAxis("Vertical");

		if (climbAvailable)
		{
			DoClimb(verticalMovementInput);
			CharacterClimbAnimation(verticalMovementInput);

		}
		else
		{
			characterRigidbody.gravityScale = setGravityScale;
			characterAnimator.SetBool("Climbing", false);
		}
	}

	private void DoClimb(float verticalMovementInput)
	{
		float movementSpeedTimesInput = climbSpeed * verticalMovementInput * Time.deltaTime;
		characterRigidbody.velocity = Vector2.up * movementSpeedTimesInput;
		characterRigidbody.gravityScale = 0;
	}

	private void CharacterClimbAnimation(float verticalMovementInput)
	{
		if (verticalMovementInput != 0)
		{
			characterAnimator.SetBool("Climbing", true);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		ContactPoint2D newContact = collision.contacts[0];
		if (newContact.otherCollider == playerFeetCollider)
		{
			jumpTime = 0;
			canJump = true;
			grounded = true;
		}
	}

	void OnCollisionExit2D(Collision2D collision)
	{
		grounded = false;
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
