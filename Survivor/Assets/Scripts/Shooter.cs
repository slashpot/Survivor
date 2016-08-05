using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {

	public float speed;

	private Rigidbody rb;
	private float lifetime = 3f;

	void Start () {
		rb=GetComponent<Rigidbody> ();
		rb.velocity = transform.forward * speed;
	}

	void Update(){
		lifetime -= Time.deltaTime;
		if (lifetime <= 0f)
			Destroy (gameObject);
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Ammo") {
			return;
		}
		Destroy(gameObject);
	}
}
