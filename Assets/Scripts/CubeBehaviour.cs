using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeBehaviour : MonoBehaviour {

	public static bool otherCubeActivated;
	bool thisCubeActive;
	bool justActivated;

	// Use this for initialization
	void Start () {
		
		CubeBehaviour.otherCubeActivated = false;
		thisCubeActive = false;
		justActivated = false;
	}

	void OnMouseDown () {
		if (thisCubeActive == false) {
			thisCubeActive = true;
			CubeBehaviour.otherCubeActivated = true;
			justActivated = true;
			this.GetComponent <Renderer> ().material.color = Color.red;
			}
			
		}

	void Update() {
		if (thisCubeActive == true && justActivated == false && otherCubeActivated == true) {
			thisCubeActive = false;
			this.GetComponent <Renderer> ().material.color = Color.white;
		}
		if (justActivated == true) {
			justActivated = false;
			otherCubeActivated = false;
		} 

}
}
