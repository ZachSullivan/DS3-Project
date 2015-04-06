using UnityEngine;
using System.Collections;

public class EscapeBallonScript : MonoBehaviour {
	public bool balloon, feather, generator;

	void OnEnable(){
		gameController.balloonEvent += collectBalloons;
		gameController.featherEvent += collectFeathers;
		gameController.generatorEvent += collectGenerator;
		gameController.takeOffEvent += takeOff;
	}

	void OnDisable(){
		gameController.balloonEvent -= collectBalloons;
		gameController.featherEvent -= collectFeathers;
		gameController.generatorEvent -= collectGenerator;
		gameController.takeOffEvent -= takeOff;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void collectBalloons(){
		balloon = true;
		Debug.Log ("Balloon Collected");
	}

	void collectFeathers(){
		feather = true;
		Debug.Log ("Feather Collected");
	}
	void collectGenerator(){
		generator = true;
		Debug.Log ("Generator Collected");
	}

	void takeOff(){
		balloon = false;
		feather = false;
		generator = false;
		Debug.Log ("TAKE OFF" );
	}
}
