using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeBehaviour : MonoBehaviour {

	public int x, y;
	//need bool for if I'm an airplane
	public bool airplane;

	// Use this for initialization
	void Start () {

	}

	void OnMouseDown () {

		if (airplane == true) {
			P1GameControllerScript.AirplaneClick (gameObject);

		}

		if (airplane == false) {
			P1GameControllerScript.SkyClick (gameObject);
		}
	}

	void Update() {

	}
}
