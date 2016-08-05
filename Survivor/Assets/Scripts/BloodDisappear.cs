using UnityEngine;
using System.Collections;

public class BloodDisappear : MonoBehaviour {

	float timer = 0f;

	void Update () {
		timer += Time.deltaTime;
		if (timer >= 10f) {
			Destroy (gameObject);
		}
	}
}
