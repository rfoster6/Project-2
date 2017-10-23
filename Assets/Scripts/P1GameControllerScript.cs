using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1GameControllerScript : MonoBehaviour {
	//instantiate a row of 16 cubes using a for loop and an array
	//make all of the cubes white except
	//turn cube red when clicked; when another cube is clicked turn that one red and the other back to white

	public GameObject cubePrefab;
	GameObject [] cubeLine = new GameObject [16];
	Vector3 cubePosition;
	int cubeXPosition;

	// Use this for initialization

	void Start () {
		cubePosition = new Vector3 (-15, 0, 0);
		cubeXPosition = 2;
		for (int i = 0; i < cubeLine.Length; i++) {
			
			cubeLine [i] = (GameObject)Instantiate (cubePrefab, cubePosition, Quaternion.identity);
			cubeLine [i].GetComponent<Renderer> ().material.color = Color.white;
			//this line doesn't work, no error message, but my cubes are not white.


			cubePosition += new Vector3 (cubeXPosition, 0, 0);
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
