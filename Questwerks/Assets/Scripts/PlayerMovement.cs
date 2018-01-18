using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    // This is a reference to the Character Controller component. It controls how the player reacts
    // to physics, and how they move. We'll need to give it commands to make the player move
    CharacterController playerCharacterController;

    // This is a reference to how fast we want the player to move in meters per second.
    // We'll make it public so we can adjust it in the editor
    public float movementSpeed = 5.0f;

    // This is how high we want to jump, and how many seconds we want the jump to last.
    // Again, we'll make them public so we can adjust it
    public float jumpHeight = 1f;
    public float jumpTime = 0.3f;

    // This is how much gravity we want to apply to the player when then are not jumping
    // We'll set this to public as well so we can tweak this
    public float gravity = 9.8f;

    // This is a timer variable, which we'll use to keep track of how long we've been jumping
    // We don't want anyone to mess with this variable since it's just for our internal use,
    // so we'll set it it to private
    float currentJumpTimer = 0f;

    // Here we also have a float for the total ammount of gravity we apply to the player
    // We add gravity on every frame to simulate realism, so this is important to keep tracks of
    public float currentGravity = 0f;

	// Start runs as soon as the game starts up. Use it to set initial variables and
    // perform any necessary setup that can only be done in code
	void Start () {

        // Even though we have a variable for the Character Controller, right now, it's empty.
        // To fix this, we call GetComponent of type Character Controller to grab the first
        // rigidbody we find attached to this gameobject: aka, the gameobject this
        // script is attached to
        playerCharacterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame. We use this for functions that need to be
    // constantly calculated
	void Update () {
        // First, we'll check to see if the D (forward) key is pressed dowb
        if (Input.GetKey(KeyCode.D))
        {
            // If it is pressed, we want to move to the right. We do this by
            // telling the character controller to move Right times our movement speed.
            playerCharacterController.Move(Vector3.right * movementSpeed * Time.deltaTime);
        }

        // Next, we'll check for A (left)
        if (Input.GetKey(KeyCode.A))
        {
            // For left, we do just the opposite by multiplying our movement 
            // direction and speed by -1
            playerCharacterController.Move(Vector3.right * movementSpeed * Time.deltaTime * -1f);
        }

        // Finally, we want to check if we pressed Jump (space) AND we are on the ground
        if (Input.GetKey(KeyCode.Space) && playerCharacterController.isGrounded)
        {

            // If we are, then we start a jump timer to tell our character to begin moving up
            currentJumpTimer = jumpTime;
        }

        // Now, we'll check to see if there is still time left in our current jump timer
        if (currentJumpTimer > 0f)
        {
            // If it is, we want to move our character up. We do this with a timer instead of
            // with a keypress because otherwise the player would move for one frame,
            // no longer be grounded and no longer be able to jump. This way, the keypress signals
            // the timer to start, and while the timer is going, we keep going up

            // First we need to figure out the speed of our jump
            // We know that Distance = Speed * Time
            // So we can divide the height (distance) of the jump by the time of the jump
            // to calculate what the speed should be
            float jumpSpeed = jumpHeight / jumpTime;

            // Now that we have the speed, we can use it to move the character up
            playerCharacterController.Move(Vector3.up * jumpSpeed * Time.deltaTime);

            // Finally, we want to decrease our timer by the time that has passed
            currentJumpTimer -= Time.deltaTime;
        }

        // If our current jump timer is NOT larger than 0, AND we are not on the ground.
        // then we should fall by applying gravity to our movement
        if (currentJumpTimer <= 0f && playerCharacterController.isGrounded == false)
        {
            // First we add the gravity per second to the current gravity
            currentGravity += gravity * Time.deltaTime;

            // Then we move down by our current gravity
            playerCharacterController.Move(Vector3.up * -1f * currentGravity * Time.deltaTime);
        }

        // Very lastly, if our player has landed and we are not jumping,
        // we want to reset our current gravity to use next time we fall or jump
        if (playerCharacterController.isGrounded && currentJumpTimer <= 0f)
        {
            currentGravity = 0f;
        }

    }
}
