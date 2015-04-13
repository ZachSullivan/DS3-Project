using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class inputController : MonoBehaviour {

	/*
	NOTE THIS CODE WILL NOT RUN IN THE EVENT THAT THE ARDUINO IS NOT PLUGGED IN
	COMMENT OUT ALL ARDUINO RELATED CODE IN THE EVENT OF TESTING
	*/
	// Serial Port Global Variables and Declaration

	SerialPort sp = new SerialPort("/dev/tty.usbmodemfd121", 9600 );
	public int readValue = 0;
	public int lastReadValue = 0;
	public GameObject cubeObject;

	public gameController gameController;

	// Use this for initialization
	void Start () {
		// Serial Port initialization
		sp.Open();
		sp.ReadTimeout = 1;


	}
	
	// Update is called once per frame
	void Update () {
		// Update function that checks for input from the serial ports
		if (sp.IsOpen) {
			try {
				string charValue = sp.ReadLine();
				//Debug.Log ( charValue );
				if( charValue.Contains("A") ){
					//Debug.Log ("A");
					readValue = int.Parse ( sp.ReadLine() );
					//readValue = int.Parse( charValue );
					Debug.Log (readValue);
					if( readValue > 200 && Mathf.Abs( readValue - lastReadValue ) >= 5 ){
						Debug.Log ( readValue );
						if( lastReadValue - readValue >=  5){
							gameController.arduino1 = readValue;
						}
						lastReadValue = readValue;

					}
					else 
						readValue = lastReadValue;
				} else if( charValue.Contains("B") ){
					//Debug.Log ("B");
					//readValue = int.Parse ( sp.ReadLine() );
					readValue = int.Parse( charValue );
					if( readValue > 200 && Mathf.Abs( readValue - lastReadValue ) >= 5 ){
						Debug.Log ( readValue );
						if( lastReadValue - readValue >=  5){
							gameController.arduino2 = readValue;
						}
						lastReadValue = readValue;
						
					}
					else 
						readValue = lastReadValue;
				}

			} catch ( System.Exception ){
				//Debug.Log ( "Error: System Exception Caught" );
			}
		}

		if (Input.GetKeyDown (KeyCode.Q)) {
			gameController.arduino1 = 200;
		}
		if (Input.GetKeyDown (KeyCode.W)) {
			gameController.arduino2 = 0.1f;
		}
		if (Input.GetKeyDown (KeyCode.E)) {
			gameController.arduino3 = 200;
		}
	}
	void createCube( int numCubes ){
		// Cube creation for testing purposes
		/**
		for( int i = 0; i < numCubes; i++){
			float randX = Random.Range (-2, 2);
			Instantiate ( cubeObject, new Vector3 (randX, 6, 0), Quaternion.identity);
		}
		**/
	}
}
