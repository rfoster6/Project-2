using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P1GameControllerScript : MonoBehaviour {

	public GameObject cubePrefab;
	Vector3 [,] cubePosition;
	public static bool activeAirplane;
	public static Vector3 airplaneSpawn;
	static Vector3 airplaneLocation;
	GameObject myCube;
	public static float turnTime;
	float turnSpeed;
	public static bool moveEvents;
	public static int cargo;
	static bool cargoGain;
	static Vector3 deliveryDepot;
	static bool depoCargo;
	static int score;
	static bool cargoEvents;
	static int maxCargo;
	static KeyCode lastHitKey;
	int maxX;
	int maxY;
	int gridSize;
	public static GameObject Airplane;
	public GameObject AirplanePreFab;
	public Text displayCargo;
	public Text displayScore;


	// Use this for initialization

	void Start () {

		turnTime = 1.5f;
		turnSpeed = 1.5f;
		moveEvents = false;
		cargo = 0;
		cargoEvents = false;
		maxX = 16;
		maxY = 9;

		deliveryDepot = new Vector3 (15, 0);
		cubePosition = new Vector3[maxX, maxY];

		for (int y = 0; y < maxY; y++) {
			for (int x = 0; x < maxX; x++) {

				cubePosition [x,y] = new Vector3 (x * 2 - 15, y * 2 - 8, 0);
				myCube = Instantiate (cubePrefab, cubePosition [x,y], Quaternion.identity);
				//myCube.GetComponent<CubeBehaviour> ().x = x;
				//myCube.GetComponent<CubeBehaviour> ().y = y;

				if (deliveryDepot == new Vector3 (x, y)) {
					myCube.GetComponent<Renderer> ().material.color = Color.black;
				}
			}
		}

		airplaneSpawn = cubePosition [0, 8];
		Airplane = Instantiate (AirplanePreFab, airplaneSpawn, Quaternion.identity);
		Airplane.GetComponent<Renderer> ().material.color = Color.red;
		airplaneLocation = airplaneSpawn;
		print ("Airplane made!");

	}

	//click process if cube is airplane
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
		if (Input.GetKeyDown (KeyCode.D)){
			lastHitKey = KeyCode.D;
		}
		if (Input.GetKeyDown (KeyCode.S)){
				lastHitKey = KeyCode.S;
		}
		if (Input.GetKeyDown (KeyCode.A)){
					lastHitKey = KeyCode.A;
		}
		if (Input.GetKeyDown (KeyCode.W)){
			lastHitKey = KeyCode.W;
		}
	}
		
	public static void CargoRules (){
		//game controls about cargo here
		maxCargo = 90;
		if (cargoEvents == true) {
			if (airplaneLocation == airplaneSpawn) {
				cargoGain = true;
			} else {
				cargoGain = false;
			}
			if (airplaneLocation == deliveryDepot) {
				depoCargo = true;
			} else {
				depoCargo = false;
			}
			if (cargo < maxCargo && cargoGain == true) { 
				cargo += 10;
			}

			if (depoCargo == true) {
				score += cargo / 10;
				cargo = 0;
			}

			cargoEvents = false;
		}
	}

	// Update is called once per frame
	void Update () {
		//directions don't specify that turns are 1.5 after last click, so I made them just every 1.5 regardless of last click
		lastHitKeyMethod ();
		if (Time.time > turnTime) {
			turnTime += turnSpeed;
			print ("New Turn");
			cargoEvents = true;
			CargoRules ();
			print ("last key:" + lastHitKey);
			print ("Cargo:" + cargo + "....Score:" + score);

		}

		displayCargo.text = "Cargo:" + cargo;
		displayScore.text = "Score:" + score;
			
	}
}
