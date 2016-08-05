using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

	public Transform target;
	public float smoothing = 5f;
	public bool dead = false;
	Vector3 offset;

	// Use this for initialization
	void Start () {
		
		offset = transform.position - target.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (dead == false) {
			Vector3 targetCampos = target.position + offset;
			transform.position = Vector3.Lerp (transform.position, targetCampos, smoothing * Time.deltaTime);
		}

	}
}
