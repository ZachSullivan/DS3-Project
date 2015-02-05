using UnityEngine;
using System.Collections;
//using System.IO.Ports;

public class inputController : MonoBehaviour {
	/**
	SerialPort sp = new SerialPort("/dev/tty.usbmodemfa131", 9600 );
	public int readValue = 0;
	public int lastReadValue = 0;
	public GameObject cubeObject;
	**/
	// Use this for initialization
	void Start () {
		/**
		sp.Open();
		sp.ReadTimeout = 1;
		**/

	}
	
	// Update is called once per frame
	void Update () {
		/**
		if (sp.IsOpen) {
			try {
				readValue = int.Parse ( sp.ReadLine() );
				if( readValue > 600 && Mathf.Abs( readValue - lastReadValue ) >= 5 ){
					Debug.Log ( readValue );
					if( lastReadValue - readValue >=  5){
						createCube(lastReadValue - readValue);
					}
					lastReadValue = readValue;

				}
				else 
					readValue = lastReadValue;

			} catch ( System.Exception ){
				//Debug.Log ( "Error: System Exception Caught" );
			}
		}
		**/
	}
	void createCube( int numCubes ){
		/**
		for( int i = 0; i < numCubes; i++){
			float randX = Random.Range (-2, 2);
			Instantiate ( cubeObject, new Vector3 (randX, 6, 0), Quaternion.identity);
		}
		**/
	}
}
