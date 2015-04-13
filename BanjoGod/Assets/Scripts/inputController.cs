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

	public GameObject cubeObject;

	public gameController gameController;

	public int lastVal1, lastVal2, lastVal3;

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

				readValue = int.Parse ( sp.ReadLine() );
				Debug.Log ("Read Value: " + readValue);

				int remapVal1 = readValue & 0xF;
				int remapVal2 = (readValue >> 4) & 0xF;
				int remapVal3 = (readValue >> 8) & 0xF;
				/*
				Debug.Log ( "remapVal1: " + remapVal1 );
				Debug.Log ( "remapVal2: " + remapVal2 );
				Debug.Log ( "remapVal3: " + remapVal3 );
				*/

				if( Mathf.Abs ( remapVal1 - lastVal1 ) >= 5  ){
					gameController.arduino1 = remapVal1;
				} 
				lastVal1 = remapVal1;

				if( Mathf.Abs ( remapVal2 - lastVal2 ) >= 5 ){
					gameController.arduino2 = remapVal2;
				} 
				lastVal2 = remapVal2;

				if( Mathf.Abs ( remapVal3 - lastVal3 ) >= 5 ){
					gameController.arduino3 = remapVal3;
				} 
				lastVal3 = remapVal3;
				/**
				if( readValue > 200 && Mathf.Abs( readValue - lastReadValue ) >= 5 ){
					Debug.Log ( readValue );
					if( lastReadValue - readValue >=  5){
						gameController.arduino1 = readValue;
					}
					lastReadValue = readValue;

				}
				else 
					readValue = lastReadValue;
				**/
				/**
				if( readValue > 200 && Mathf.Abs( readValue - lastReadValue ) >= 5 ){
					Debug.Log ( readValue );
					if( lastReadValue - readValue >=  5){
						gameController.arduino2 = readValue;
					}
					lastReadValue = readValue;
					
				}
				else 
					readValue = lastReadValue;
				**/

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
