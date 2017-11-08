using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P1GameControllerScript : MonoBehaviour {

	//airplane variables
	public GameObject AirplanePreFab;
	public static GameObject Airplane;
	public static bool activeAirplane;
	Vector3 airplaneSpawn;
	Vector3 airplaneLocation;
	int maxCargo;
	int cargo;
	public Text displayCargo;
	int aX, aY;

	//sky variables
	public GameObject cubePrefab;
	GameObject myCube;
	Vector3 [,] cubePosition;
	Vector3 deliveryDepot;
	int maxX;
	int maxY;
	int gridSize;

	//other variables
	float turnTime;
	float turnSpeed;
	bool newTurn;
	int score;
	public Text displayScore;
	KeyCode lastHitKey;


	// Use this for initialization

	void Start () {

		turnTime = 1.5f;
		turnSpeed = 1.5f;
		newTurn = false;
		cargo = 0;
		maxX = 16;
		maxY = 9;
		aX = 0;
		aY = 8;

		cubePosition = new Vector3[maxX, maxY];
		deliveryDepot = new Vector3 (15, 0);

		for (int y = 0; y < maxY; y++) {
			for (int x = 0; x < maxX; x++) {

				cubePosition [x,y] = new Vector3 (x * 2 - 15, y * 2 - 8, 0);
				myCube = Instantiate (cubePrefab, cubePosition [x,y], Quaternion.identity);
				//myCube.GetComponent<CubeBehaviour> ().x = x;
				//myCube.GetComponent<CubeBehaviour> ().y = y;

				if (deliveryDepot == new Vector3 (x,y)) {
					myCube.GetComponent<Renderer> ().material.color = Color.black;
				}
			}
		}

		deliveryDepot = cubePosition [15,0];
		airplaneSpawn = cubePosition [0,8];
		airplaneLocation = airplaneSpawn;
		Airplane = Instantiate (AirplanePreFab, airplaneSpawn, Quaternion.identity);
		Airplane.GetComponent<Renderer> ().material.color = Color.red;
		print ("Airplane made!");

	}
		
	public static void AirplaneClick (){
		//click on deactive airplane
		if (!activeAirplane) {
			activeAirplane = true;
			Airplane.transform.localScale *= 1.2f;
			print ("Airplane Activated!");
		}
		//click on active airplane
		else if (activeAirplane) {
			Airplane.transform.localScale /=1.2f;
			activeAirplane = false;
			print ("Airplane Deactivated!");
		}
	}

	void lastHitKeyMethod (){
		foreach (KeyCode c in Input.inputString) {
			lastHitKey = c;
		}
	}

	void AirplaneMovement (){
		if (activeAirplane && newTurn) { 
			if (aX < maxX-1 && lastHitKey == KeyCode.D) {
				aX++;
			}
			if (aX > 0 && lastHitKey == KeyCode.A) {
				aX--;
			}

			if (aY > 0 && lastHitKey == KeyCode.S) {
				aY--;
			}
			if (aY < maxY-1 && lastHitKey == KeyCode.W) {
					aY++;
			}
			airplaneLocation = cubePosition [aX, aY];
			Airplane.transform.position = airplaneLocation;
			//so that airplane doesn't auto move after pressing x key once
			lastHitKey = KeyCode.Keypad0;
		}
	}

	void CargoRules (){
		//game controls about cargo here
		maxCargo = 90;
		if (newTurn) {
			if (airplaneLocation == airplaneSpawn && cargo < maxCargo) {
				cargo += 10;
			}
			if (airplaneLocation == deliveryDepot) {
				score += cargo;
				cargo = 0;
			}
		}
	}

	// Update is called once per frame
	void Update () {
		//directions don't specify that turns are 1.5 after last click, so I made them just every 1.5 regardless of last click
		lastHitKeyMethod ();
		if (Time.time > turnTime) {
			turnTime += turnSpeed;
			newTurn = true;
			print ("New Turn");
			CargoRules ();
			AirplaneMovement ();
			print ("last key:" + lastHitKey);
			print ("Cargo:" + cargo + "....Score:" + score);
			newTurn = false;
		}

		displayCargo.text = "Cargo:" + cargo;
		displayScore.text = "Score:" + score;
			
	}
}
