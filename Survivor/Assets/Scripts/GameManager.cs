using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public PlayerController player;
	public AudioClip scream;
	public AudioClip[] zombieclips;
	public bool test;

	PlayMusic playmusic;
	AudioSource audio;
	bool played = false;
	Animator anim;
	float shoutrate;
	float restartTimer = 0f;
	float restartDelay = 5f;
	float timer;
	int chooseclip;

	void Awake(){
		anim = GetComponent<Animator> ();
		audio = GetComponent<AudioSource> ();
		if(test == false)
			playmusic = GameObject.FindGameObjectWithTag ("UI").GetComponent<PlayMusic> ();
	}

	void Start(){
		shoutrate = Random.Range (4f, 8f);
		chooseclip = Random.Range (0, 5);
	}

	void Update () {
		timer += Time.deltaTime;
		if (timer >= shoutrate) {
			randomsound();
		}
		/*
		if(player.dead == true){
			gameover ();
		}
		*/
	}

	void gameover(){
		if (played == false) {
			played = true;
			audio.clip = scream;
			audio.Play();
		}

		anim.SetTrigger ("GameOver");

		if(test == false)
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

	void randomsound(){
		audio.clip = zombieclips [chooseclip];
		audio.Play ();
		timer = 0f;
		shoutrate = Random.Range (4f, 8f);
		chooseclip = Random.Range (0, 5);
	}
}
