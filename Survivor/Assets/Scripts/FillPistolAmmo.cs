using UnityEngine;
using System.Collections;

public class FillPistolAmmo : MonoBehaviour {

	public PlayerController player;
	AudioSource sound;
	SpriteRenderer render;

	void Awake(){
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
		sound = GetComponent<AudioSource> ();
		render = GetComponent<SpriteRenderer> ();
	}

	void OnTriggerEnter(Collider other){
		if (other.tag=="Body" && player.pistolbullets != 21) {
			render.enabled = false;
			sound.Play ();
			player.pistolbullets = 21;
			player.PistolBullets.text = "21";
			Invoke ("disappear", 0.5f);
		}
	}

	void disappear(){
		Destroy (gameObject);
	}
}
