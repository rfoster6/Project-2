using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1GameControllerScript : MonoBehaviour {
	//instantiate a row of 16 cubes using a for loop and an array
	//make all of the cubes white except
	//turn cube red when clicked; when another cube is clicked turn that one red and the other back to white

	public GameObject cubePrefab;
	Vector3 cubePosition;
	public static GameObject selectedCube;
	public static GameObject activeAirplane;
	public static int airplaneX, airplaneY;
	GameObject myCube;


	// Use this for initialization

	void Start () {
		
		airplaneX = 0;
		airplaneY = 8;

		for (int y = 0; y < 9; y++) {
			for (int x = 0; x < 16; x++) {

				cubePosition = new Vector3 (x * 2 - 15, y * 2 - 8, 0);
				myCube = Instantiate (cubePrefab, cubePosition, Quaternion.identity);
				myCube.GetComponent<CubeBehaviour> ().x = x;
				myCube.GetComponent<CubeBehaviour> ().y = y;

				if (x == airplaneX && y == airplaneY) {
					myCube.GetComponent<Renderer> ().material.color = Color.red;
					//tell cube "Hey, I'm an airplane!"
					myCube.GetComponent<CubeBehaviour> ().airplane = true;
					print ("Airplane made!");
				} else {
					//tell cube "I'm not an airplane!"
					myCube.GetComponent<CubeBehaviour> ().airplane = false;
				}
			}
		}
	}

	//click process if cube is airplane
	public static void AirplaneClick (GameObject clickedCube){
		//click on deactive airplane
		if (activeAirplane == null) {
			activeAirplane = clickedCube;
			activeAirplane.transform.localScale *= 1.2f;
			print ("Airplane Activated!");
		}
		//click on active airplane
		else if (clickedCube == activeAirplane) {
			activeAirplane.transform.localScale /=1.2f;
			activeAirplane = null;
			print ("Airplane Deactivated!");
		}

	}

	//click process if cube is sky
	public static void SkyClick (GameObject clickedCube){
		if (activeAirplane != null) {
			//Previous cube is no longer airplane
			activeAirplane.GetComponent<Renderer> ().material.color = Color.white;
			activeAirplane.transform.localScale /= 1.2f;
			activeAirplane.GetComponent<CubeBehaviour> ().airplane = false;

			//clickedCube is now activeAirplane
			activeAirplane = clickedCube;
			activeAirplane.GetComponent<Renderer> ().material.color = Color.red;
			activeAirplane.transform.localScale *= 1.2f;
			activeAirplane.GetComponent<CubeBehaviour> ().airplane = true;
			print ("Active Airplane Moved!");
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
