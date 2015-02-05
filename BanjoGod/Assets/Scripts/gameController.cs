using UnityEngine;
using System.Collections;

public class gameController : MonoBehaviour {

	public int arduino1, arduino2; 
	public int rainLevel;
	public float temperatureLevel;
	public bool newInput = false;
	public int decrementBuffer = 0;
	public int amplitude = 30;
	public int targetAmplitude;
	public int timeIndex = 0;
	// Use this for initialization
	void Start () {
		rainLevel = 0;
		temperatureLevel = 0.0f;
		arduino1 = 0;
		arduino2 = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		bool buffered;

		if (decrementBuffer == 0)
			buffered = true;
		else
			buffered = false;

		rainUpdate (buffered);
		temperatureUpdate (buffered);

		if( buffered )
			decrementBuffer = 59;
		else 
			decrementBuffer--;

		if (newInput)
			newInput = false;
	}

	void temperatureUpdate(bool buffered){

		int period = 60 * 60;

		if (timeIndex == (60 * 60) )
			timeIndex = 0;
		else
			timeIndex++;

		if( buffered ){
			amplitude--;

			if (amplitude < 30)
				amplitude = 30;

			if (amplitude < targetAmplitude) {
				amplitude += 2;
			} else {
				targetAmplitude = 0;
			}
		}


		if (newInput) {
			targetAmplitude = arduino2;
			arduino2 = 0;
		}



		temperatureLevel = amplitude * Mathf.Sin ( ( 2*Mathf.PI / Mathf.Abs(period) ) * ( timeIndex ) );

	}

	void rainUpdate(bool buffered){

		if (buffered)
			rainLevel--;

		if (rainLevel < 0)
			rainLevel = 0;

		if ( newInput ) {
			rainLevel += arduino1;
			arduino1 = 0;
		}
		
		int rainState = 0;

		if (rainLevel > 40)
			rainState = 3;
		else if (rainLevel > 20)
			rainState = 2;
		else if (rainLevel > 0)
			rainState = 1;

		if( buffered ){
			switch (rainState)
			{
			case 0:
				Debug.Log ( "No Rain " );
				break;
			case 1:
				Debug.Log ( "Light Rain " );
				break;
			case 2: 
				Debug.Log ( "Medium Rain ");
				break;
			case 3: 
				Debug.Log ( "Heavy Rain ");
				break;
			default:
				Debug.Log("Error: no current rain state");
				break;
			}
		}
	}
}
