using UnityEngine;
using System.Collections;

public class GameOverManager : MonoBehaviour {

	public PlayerController player;
	PlayMusic playmusic;
	AudioSource audio;
	Animator anim;
	float restartTimer = 0f;
	float restartDelay = 5f;

	void Awake(){
		anim = GetComponent<Animator> ();
		audio = GetComponent<AudioSource> ();
		playmusic = GameObject.FindGameObjectWithTag ("UI").GetComponent<PlayMusic> ();
	}

	void Update () {
		
		if(player.dead == true)
		{
			// ... tell the animator the game is over.
			audio.Play();
			anim.SetTrigger ("GameOver");
			playmusic.FadeDown (12f);

			// .. increment a timer to count up to restarting.
			restartTimer += Time.deltaTime;

			// .. if it reaches the restart delay...
			if(restartTimer >= restartDelay)
			{
				// .. then reload the currently loaded level.
				Application.LoadLevel(Application.loadedLevel);
			}
		}
	}
}
