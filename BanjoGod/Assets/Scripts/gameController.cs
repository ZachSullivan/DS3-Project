using UnityEngine;
using System.Collections;

public class gameController : MonoBehaviour {

	/**
	 * 
	 * 
	 * 
	 * */

	public int arduino1, arduino2; // Arduino Input Values ( this will come from the inputController file later )

	// Rain and Temperature Levels
	public int rainLevel;
	public int windLevel;
	public float temperatureLevel;

	// newInput boolean for testing purposes
	public bool newInput = false;

	// DecrementBuffer to make simulation more readable in testing 
	public int decrementBuffer = 0;

	// Amplitude Variables for Temperature Fluctuation
	public int amplitude = 30;
	public int targetAmplitude;

	// This version of the simulation runs based on frame count rather than actual time, as such a time index is created to monitor time
	public int timeIndex = 0;


	//Emission source for rain system
	public GameObject RainSystem;
	public GameObject SpitSystem;

	//Old rain emission system
	//public ParticleSystem rainSystem;
	//public ParticleEmitter rainEmmitter;

	// Use this for initialization
	void Start () {
		RainSystem.GetComponent("EllipsoidParticleEmitter");
		SpitSystem.GetComponent("EllipsoidParticleEmitter");
		// Initialization
		rainLevel = 0;
		windLevel = 0;
		temperatureLevel = 0.0f;
		arduino1 = 0;
		arduino2 = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		bool buffered;

		if (arduino1 > 0)
			newInput = true;

		if (decrementBuffer == 0)
			buffered = true;
		else
			buffered = false;

		// Call functions related to individual simulation elements
		// These will be adapted into their own scripts when they become more complex
		rainUpdate (buffered);
		temperatureUpdate (buffered);



		// Buffer Management
		if( buffered )
			decrementBuffer = 59;
		else 
			decrementBuffer--;

		if (newInput)
			newInput = false;
	}

	void temperatureUpdate(bool buffered){
		// Temerature State Machine
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


		// Temperature is a sinosodal relationship. User effects Amplitude of wave
		temperatureLevel = amplitude * Mathf.Sin ( ( 2*Mathf.PI / Mathf.Abs(period) ) * ( timeIndex ) );

	}

	void rainUpdate(bool buffered){
		// Rain State Machine

		//if (buffered)
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
				//rainSystem.emissionRate = rainLevel;

				RainSystem.particleEmitter.maxEmission = rainLevel;
				SpitSystem.particleEmitter.maxEmission = rainLevel;
				break;
			case 1:
				Debug.Log ( "Light Rain " );
					
				//rainSystem.emissionRate = rainLevel;
				RainSystem.particleEmitter.maxEmission = rainLevel;
				SpitSystem.particleEmitter.maxEmission = rainLevel;
				break;
			case 2: 
				Debug.Log ( "Medium Rain ");

				//rainSystem.emissionRate = rainLevel;
				RainSystem.particleEmitter.maxEmission = rainLevel;
				SpitSystem.particleEmitter.maxEmission = rainLevel;
				break;
			case 3: 
				Debug.Log ( "Heavy Rain ");
				//rainSystem.emissionRate = rainLevel;
				RainSystem.particleEmitter.maxEmission = rainLevel;
				SpitSystem.particleEmitter.maxEmission = rainLevel;
				break;
			default:
				Debug.Log("Error: no current rain state");
				break;
			}
		}
	}
}
