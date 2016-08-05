using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	Text scoretext;
	public int point = 0;

	void Awake(){
		scoretext = GetComponent<Text> ();
	}

	void Update () {
		scoretext.text = "Score: " + point;
	}
}
