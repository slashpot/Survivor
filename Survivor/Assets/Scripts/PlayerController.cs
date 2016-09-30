using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using RAIN.Entities;
using RAIN.Entities.Aspects;

public class PlayerController : MonoBehaviour {

	Rigidbody rb;
	int currentweapon = 2;
	int changeweapon =2;
	int pistolclip = 7;
	int rifleclip = 30;
	public int pistolbullets = 21;
	public int riflebullets = 60;
	bool reloading = false;
	float pitchrange = 0.1f;
	float originalpitch;

	float timer;
	public float speed = 15f;
	float vertical;
	float horizontal;
	Vector3 movement;

	public GameObject pistolshot;
	public Transform pistol;
	public Transform rifle;
	public float fireRate;
	Collider attackcollider;
	Animator anima;

	public AudioSource weaponsound;
	public AudioSource reloadsound;
	public AudioSource changesound;
	public AudioClip pistolsound;
	public AudioClip knifesound;
	public AudioClip riflesound;

	public Image cursor;
	public GameObject Waiting;
	public Slider waitingdisplay;
	public Text waitingtext;
	public Text showclip;
	public Text PistolBullets;
	public Text RifleBullets;
	public ScoreManager scoremanager;
	float waitingtime = 0f;

	public GameObject blood;
	Quaternion flipblood = Quaternion.Euler(90f,0f,0f);
	Quaternion accuracy;
	CameraManager cameracontroller;
	EntityRig rig;

	public bool dead = false;

	// Use this for initialization
	void Awake(){
		anima = GetComponentInChildren<Animator> ();
		rb = GetComponent<Rigidbody> ();
		cameracontroller = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraManager> ();
		rig = GetComponentInChildren<EntityRig> ();
		attackcollider = GetComponent<CapsuleCollider> ();
		Waiting.SetActive (false);
		Cursor.visible = false;
	}

	void Start(){
		originalpitch = weaponsound.pitch;
	}

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if (timer >= 0.1f && attackcollider.enabled == true) {
			attackcollider.enabled = false;
		}

		if (waitingtime <= 0f) {
			if (waitingtext.enabled == true) {
				Waiting.SetActive (false);
				ChangeWeapon ();
			}
			if (Input.GetKeyDown (KeyCode.Alpha1)&&currentweapon!=1) {
				changeweapon = 1;
				//waitingtext.text = "Changing";
			}
			if (Input.GetKeyDown (KeyCode.Alpha2)&&currentweapon!=2) {
				changeweapon = 2;
				//waitingtext.text = "Changing";
			}
			if (Input.GetKeyDown (KeyCode.Alpha3)&&currentweapon!=3) {
				changeweapon = 3;
				//waitingtext.text = "Changing";
			}
			if (Input.GetAxis ("Mouse ScrollWheel") > 0.95f) {
				changeweapon++;
				if (changeweapon == 4)
					changeweapon = 1;
			}
			if (Input.GetAxis ("Mouse ScrollWheel") < -0.95f) {
				changeweapon--;
				if (changeweapon == 0)
					changeweapon = 3;
			}


			if (reloading == true) {
				Reload();
			}
			if (Input.GetKeyDown (KeyCode.R)) {
				switch (currentweapon) {
				case 2:
					if (pistolclip != 7&&pistolbullets!=0) {
						reloadsound.Play ();
						Wait (1.2f);
						reloading = true;
						waitingtext.text = "Reloading";
						anima.SetTrigger ("Reload");
					}
					break;
				case 3:
					if (rifleclip != 30&&riflebullets!=0) {
						reloadsound.Play ();
						Wait (1.2f);
						reloading = true;
						waitingtext.text = "Reloading";
						anima.SetTrigger ("Reload");
					}
					break;
				}
			}


			// If the Fire1 button is being press and it's time to fire...
			if (Input.GetButton ("Fire1") && timer >= fireRate) {
				Attack ();
			}

		} else {
			waitingtime -= Time.deltaTime;
			waitingdisplay.value = 1f - waitingtime;
		}
			
		if (Time.timeScale==0) {
			timer = 0f;
			Cursor.visible = true;
			cursor.enabled = false;
		} else {
			Cursor.visible = false;
			cursor.transform.position = Input.mousePosition;
			cursor.enabled = true;
		}

	}


	void FixedUpdate(){
		vertical = Input.GetAxis ("Vertical");
		horizontal = Input.GetAxis ("Horizontal");
		Move (vertical, horizontal);
		Turn ();
	}

	public class Weapon {
		public int bullets;
		public int clip;
		public int rate;
		public float reloadTime;
		public AudioSource sound;

		public Weapon(int _bullets, int _clip, int _rate, float _reloadTime, AudioSource _sound){
			bullets = _bullets;
			clip = _clip;
			rate = _rate;
			reloadTime = _reloadTime;
			sound = _sound;
		}
	}
		
	void Move(float v,float h){
		movement.Set (h, 0f, v);
		rb.velocity = movement*speed;

		if (rb.velocity.magnitude == 0f && anima.GetBool ("Walking") == true) {
			anima.SetBool ("Walking", false);
		} else if (rb.velocity.magnitude != 0f && anima.GetBool ("Walking") == false) {
			anima.SetBool ("Walking", true);
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


	void Attack(){
		switch (currentweapon) {
		case 1:
			timer = 0f;
			weaponsound.pitch = Random.Range (originalpitch - pitchrange, originalpitch + pitchrange);
			weaponsound.Play ();
			anima.SetTrigger ("Attack");
			attackcollider.enabled = true;
			break;

		case 2:
			if (pistolclip == 0&&pistolbullets!=0) {
				reloadsound.Play ();
				Wait (1.2f);
				reloading = true;
				waitingtext.text = "Reloading";
				anima.SetTrigger ("Reload");
			} else if(pistolclip>0){
				timer = 0f;
				pistolclip -= 1;
				showclip.text = pistolclip+" / 7";
				Instantiate (pistolshot, pistol.position, pistol.rotation);
				anima.SetTrigger ("Attack");
				weaponsound.pitch = Random.Range (originalpitch - pitchrange, originalpitch + pitchrange);
				weaponsound.Play ();
			}
			break;

		case 3:
			if (rifleclip == 0&&riflebullets!=0) {
				reloadsound.Play ();
				Wait (1.2f);
				reloading = true;
				waitingtext.text = "Reloading";
				anima.SetTrigger ("Reload");
			} else if(rifleclip>0){
				timer = 0f;
				rifleclip -= 1;
				showclip.text = rifleclip+" / 30";
				accuracy = Quaternion.Euler (0f, rifle.rotation.eulerAngles.y+Random.Range (-5f, 5f), 0f);
				Instantiate (pistolshot, rifle.position, accuracy);
				anima.SetTrigger ("Attack");
				weaponsound.pitch = Random.Range (originalpitch - pitchrange, originalpitch + pitchrange);
				weaponsound.Play ();
			}
			break;
		}
	}


	void ChangeWeapon(){
		if (changeweapon != currentweapon) {
			currentweapon = changeweapon;
			changesound.Play ();
			switch (currentweapon) {
			case 1:
				fireRate = 0.25f;
				anima.SetInteger ("Change Weapon", 1);
				showclip.text = "";
				weaponsound.clip = knifesound;
				rig.Entity.GetAspect ("Sound").IsActive = false;
				break;
			case 2:
				fireRate = 0.25f;
				anima.SetInteger ("Change Weapon", 2);
				showclip.text = pistolclip+" / 7";
				weaponsound.clip = pistolsound;
				rig.Entity.GetAspect ("Sound").IsActive = true;
				break;
			case 3:
				fireRate = 0.10f;
				anima.SetInteger ("Change Weapon", 3);
				showclip.text = rifleclip+" / 30";
				weaponsound.clip = riflesound;
				rig.Entity.GetAspect ("Sound").IsActive = true;
				break;
			}
		}
	}


	void Reload(){
		int tempclip;
		switch (currentweapon) {
		case 2:
			tempclip = 7 - pistolclip;
			if (pistolbullets >= tempclip) {
				pistolbullets -= tempclip;
				pistolclip += tempclip;
				showclip.text = pistolclip + " / 7";
				PistolBullets.text = pistolbullets + "";
			}else {
				pistolclip += pistolbullets;
				pistolbullets = 0;
				showclip.text = pistolclip + " / 7";
				PistolBullets.text = pistolbullets + "";
			}
			break;
		case 3:
			tempclip = 30 - rifleclip;
			if (riflebullets >= tempclip) {
				riflebullets -= tempclip;
				rifleclip += tempclip;
				showclip.text = rifleclip + " / 30";
				RifleBullets.text = riflebullets + "";
			}else {
				rifleclip += riflebullets;
				riflebullets = 0;
				showclip.text = rifleclip + " / 30";
				RifleBullets.text = riflebullets + "";
			}
			break;
		}
		reloading = false;
	}


	void Wait(float time){
		waitingtime = time;
		Waiting.SetActive (true);
		waitingdisplay.value = waitingtime;
	}


	void OnTriggerEnter(Collider other){
		if (other.tag == "Enemy") {
			scoremanager.point += 200;
			Instantiate (blood, other.transform.position, flipblood);
			Destroy (other.gameObject);
		}

	}

	public void Dead(){
		dead = true;
		Instantiate (blood, transform.position, flipblood);
		anima.SetTrigger ("Dead");
		cameracontroller.dead = true;
		this.enabled = false;
		Destroy (gameObject);
	}
}
