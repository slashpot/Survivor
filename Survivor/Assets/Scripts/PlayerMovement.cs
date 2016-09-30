using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public Image cursor;

	private Rigidbody rb;
	private Animator animator;
	private float speed = 15f;
	private Vector3 movement;

	PlayerShooting a;
	void Awake(){
		rb = GetComponent<Rigidbody> ();
		animator = GetComponentInChildren<Animator> ();
		Cursor.visible = false;
	}

	void Update() {
		if (Time.timeScale!=0) {
			Cursor.visible = false;
			cursor.transform.position = Input.mousePosition;
			cursor.enabled = true;
		} else {
			Cursor.visible = true;
			cursor.enabled = false;
		}
	}

	void FixedUpdate(){
		float vertical = Input.GetAxis ("Vertical");
		float horizontal = Input.GetAxis ("Horizontal");
		Move (vertical, horizontal);
		Turn ();
	}

	void Move(float v,float h){
		movement.Set (h, 0f, v);
		rb.velocity = movement*speed;

		if (rb.velocity.magnitude == 0f && animator.GetBool ("Walking") == true) {
			animator.SetBool ("Walking", false);
		} else if (rb.velocity.magnitude != 0f && animator.GetBool ("Walking") == false) {
			animator.SetBool ("Walking", true);
		}
	}

	void Turn(){
		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

		// Create a vector from the player to the point on the floor the raycast from the mouse hit.
		Vector3 playerToMouse = mousePos - transform.position;

		// Ensure the vector is entirely along the floor plane.
		playerToMouse.y = 0f;

		// Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
		Quaternion newRotation = Quaternion.LookRotation (playerToMouse);

		// Set the player's rotation to this new rotation.
		rb.MoveRotation (newRotation);
	}

}
