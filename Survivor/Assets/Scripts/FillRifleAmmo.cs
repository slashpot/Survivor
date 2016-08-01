using UnityEngine;
using System.Collections;

public class FillRifleAmmo : MonoBehaviour {

	public PlayerController player;
	AudioSource sound;
	SpriteRenderer render;

	void Awake(){
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
		sound = GetComponent<AudioSource> ();
		render = GetComponent<SpriteRenderer> ();
	}

	void OnTriggerEnter(Collider other){
		if (other.tag=="Body" && player.riflebullets != 60) {
			render.enabled = false;
			sound.Play ();
			player.riflebullets = 60;
			player.RifleBullets.text = "60";
			Invoke ("disappear", 0.5f);
		}
	}

	void disappear(){
		Destroy (gameObject);
	}
}
