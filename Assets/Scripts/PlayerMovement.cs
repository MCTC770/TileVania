using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour {

	[SerializeField] float movementSpeed = 500f;
	[SerializeField] float jumpHeight = 2000f;
	[SerializeField] float maxJumpTime = 0.3f;
	[SerializeField] Animator characterAnimator;
	[SerializeField] CapsuleCollider2D playerCollider;
	[SerializeField] CapsuleCollider2D playerFeetCollider;

	float horizontalMovementInput;
	float jumpInput = 0;
	float jumpTime = 0;
	bool isJumping = false;
	bool canJump = true;
	Rigidbody2D characterRigidbody;

	// Use this for initialization
	void Start () {
		characterRigidbody = GetComponent<Rigidbody2D>();
		characterAnimator.SetBool("Running", false);
		//playerCollider = GetComponent<CapsuleCollider2D>();
		//playerFeetCollider = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		ControllerInputHandler();
	}

	void ControllerInputHandler()
	{
		HorizontalMovementInput();
		JumpInput();
	}

	private void HorizontalMovementInput()
	{
		horizontalMovementInput = CrossPlatformInputManager.GetAxis("Horizontal");
		float movementSpeedTimesInput = movementSpeed * horizontalMovementInput * Time.deltaTime;
		characterRigidbody.AddRelativeForce(Vector2.right * movementSpeedTimesInput);

		CharacterRunAnimation();
		CharacterTurningWhenWalking();
	}

	private void CharacterRunAnimation()
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

	private void CharacterTurningWhenWalking()
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

	private void OnCollisionEnter2D(Collision2D collision)
	{
		ContactPoint2D newContact = collision.contacts[0];
		if (newContact.otherCollider == playerFeetCollider)
		{
			jumpTime = 0;
			canJump = true;
		}
	}
}
