using UnityEngine;
using System.Collections;

public class ZombieHealth : MonoBehaviour {

	public GameObject blood;
	public GameObject[] items;
	public int droprate;
	ScoreManager scoremanager;

	Quaternion flip = Quaternion.Euler(90f,0f,0f);
	Vector3 spawnpoint;

	void Awake(){
		scoremanager = GameObject.FindGameObjectWithTag ("Score").GetComponent<ScoreManager> ();
	}

	void Start(){
		spawnpoint.Set (transform.position.x, transform.position.y - 1f, transform.position.z);
	}
		
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Bullet") {
			scoremanager.point += 100;
			spawnpoint.Set (transform.position.x, transform.position.y - 1f, transform.position.z);
			dropammo ();
			Instantiate (blood, spawnpoint, flip);
			Destroy(gameObject);
		}
	}

	void dropammo(){
		int temp;
		temp = Random.Range (1, droprate - 1);
		if (temp == 1) {
			Instantiate (items[0], transform.position, flip);
		}else if(temp == 2){
			Instantiate (items[1], transform.position, flip);
		}
	}
}
