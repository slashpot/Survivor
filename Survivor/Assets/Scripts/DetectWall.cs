using UnityEngine;
using System.Collections;

public class DetectWall : MonoBehaviour {

	public bool detect = false;
	Rigidbody body;
	float speed = 100f;
	Quaternion turn;

	void Start(){
		body = GetComponent<Rigidbody> ();
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Wall"&&this.enabled==true) {
			turn = Quaternion.LookRotation (transform.forward * (-1f));
			body.MoveRotation (turn);
			body.velocity = transform.forward * speed * Time.deltaTime*(-1f);
		}
	}

}
