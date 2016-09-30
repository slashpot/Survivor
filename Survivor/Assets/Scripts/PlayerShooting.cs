using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerShooting : MonoBehaviour {

	public GameObject bullet;
	Dictionary<int, Weapon> weapons = new Dictionary<int, Weapon> ();

	//Texts
	public Text pistolBullets;
	public Text clipStatus;

	//AudioClips
	public AudioClip knifeSound;
	public AudioClip pistolSound;

	//Weapon Positions
	public Transform pistolPos;

	//Weapons
	public Weapon knife;
	public Weapon pistol;
	public Weapon currentWeapon;

	private Animator animator;
	private AudioSource audiosource;
	private CapsuleCollider knifeRange;
	private int weaponNumber = 2;
	private float timer = 0f;

	void Awake() {
		animator = GetComponentInChildren<Animator> ();
		audiosource = GetComponent<AudioSource> ();
		knifeRange = GetComponent<CapsuleCollider> ();

		knife = new Weapon(0, 0, 0, 0.25f, knifeSound);
		pistol = new Weapon (22, 7, 7, 0.25f, pistolSound);
	}

	void Start () {
		AddWeapons ();
		currentWeapon = weapons [2];
	}

	void Update () {
		timer += Time.deltaTime;
		if (Input.GetButton("Fire1") && timer >= currentWeapon.rate) {
			Attack ();
		}
		if (timer >= 0.1f && knifeRange.enabled == true) {
			knifeRange.enabled = false;
		}
	}

	public class Weapon {
		public int ammo;
		public int clipSize;
		public int currentClip;
		public float rate;
		public AudioClip sound;

		public Weapon (int _ammo, int _clipSize, int _currentClip, float _rate, AudioClip _sound) {
			ammo = _ammo;
			clipSize = _clipSize;
			currentClip = _currentClip;
			rate = _rate;
			sound = _sound;
		}
	}

	void Attack () {
		timer = 0f;

		if (weaponNumber == 1) {
			knifeRange.enabled = true;
			audiosource.Play ();
			animator.SetTrigger ("Attack");
		} else {
			if (currentWeapon.currentClip > 0) {
				audiosource.Play ();
				animator.SetTrigger ("Attack");
				currentWeapon.currentClip--;
				clipStatus.text = currentWeapon.currentClip + " / " + currentWeapon.clipSize;

				switch (weaponNumber) {
				case 2:
					Instantiate (bullet, pistolPos.position, pistolPos.rotation);
					break;
				}
			}
		}
	}

	void AddWeapons () {
		weapons.Add (1, knife);
		weapons.Add (2, pistol);
	}

}
