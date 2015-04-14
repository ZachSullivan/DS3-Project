using UnityEngine;
using System.Collections;

public class gameController : MonoBehaviour {


	public int arduino1;
	public int arduino2; 
	public int arduino3; // Arduino Input Values ( this will come from the inputController file later )


	// Event Driven System /// <Event Delegates and associated variables>
	/// Occurs as the player successfully triggers collection parameter.
	/// </summary>
	public delegate void CollectionEvent();
	public static event CollectionEvent balloonEvent;
	public static event CollectionEvent featherEvent;
	public static event CollectionEvent generatorEvent;
	public static event CollectionEvent takeOffEvent;
	
	public bool balloon, feather, generator;

	public bool bird, crops, lightning;
	/// //////////////////////////////////////////////////////////////


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
	
	//Variable used to define the transision speed between raining and snowing
	public int transisionSpeed;

	//Emission source for rain system//Emission source for rain system
	public GameObject RainSystem;
	public GameObject SpitSystem;
	public GameObject SnowSystem;
	
	public GameObject DefaultBG;
	public GameObject MedRainBG;

	public GameObject SummerIsland;
	public GameObject WinterIsland;

	/** Outdated?
	public Material material1;
	public Material material2;
	**/

	//GameObject references
	public GameObject cropsObject;
	public GameObject charObject;
	public GameObject intro;
	public GameObject birdObject;

	//Primary Animator
	Animator anim;

	//bool gates
	bool isWinter;
	bool hasDefaultFaded;
	
	public int timePassed = 0;


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
		arduino3 = 0;
		
		transisionSpeed = 200;
		
		isWinter = false;
		hasDefaultFaded = false;

		RainSystem.GetComponent<Renderer> ().sortingLayerName = "front";
		SnowSystem.GetComponent<Renderer> ().sortingLayerName = "front";

		WinterIsland.GetComponent<SpriteRenderer>().enabled = false;

		charObject.SetActive (false);
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		bool buffered;
		timePassed += (int) ( Time.time - timePassed );

		if (timePassed >= 10) {
			intro.SetActive( false );
		}

<<<<<<< HEAD
		if (timePassed >= 11) {
=======
		if (timePassed >= 14) {
>>>>>>> origin/post_beta_TylerBranch
			charObject.SetActive (true);
		}

		if (arduino1 > 0)
			newInput = true;
		if (arduino2 > 0)
			newInput = true;

		if (decrementBuffer == 0)
			buffered = true;
		else
			buffered = false;

		// Call functions related to individual simulation elements
		// These will be adapted into their own scripts when they become more complex
		rainUpdate (buffered);
		temperatureUpdate (buffered);
		windUpdate (buffered);
		cropGrowth(buffered);

		// Buffer Management
		if( buffered )
			decrementBuffer = 59;
		else 
			decrementBuffer--;

		if (newInput)
			newInput = false;


		if (crops == true && windLevel >= 500) {
			Instantiate (birdObject, new Vector3( 10, 0 ,(float) Random.Range(0,4)), Quaternion.identity );
			bird = true;
			windLevel -= 50;
		}

		
		if (temperatureLevel <= -10 && rainLevel >= 50 && balloon == false) {
			balloonEvent();
			balloon = true;
		}
		if (temperatureLevel >= 30 &&  rainLevel >= 500 && generator == false) {
			generatorEvent();
			generator = true;
		}
		if (bird == true && crops == true && feather == false ) {
			featherEvent();
			feather = true;
		}
		if (balloon == true && generator == true && feather == true) {
			takeOffEvent();
			balloon = false;
			generator = false;
			feather = false;
		}
	}

	void cropGrowth(bool buffered){
		
		if(temperatureLevel >= 15 && (rainLevel > 50 )){
			//Debug.Log("Crop Spawned");
			cropsObject.GetComponent<Animator>().SetBool("isGrowing", true);
			crops = true;
		}
	}

	IEnumerator islandTransition(){


		SummerIsland.GetComponent<Animator>().SetBool("IsWinter", true);
		
		WinterIsland.GetComponent<Animator>().SetBool("isWinter", true);

		yield return new WaitForSeconds (1.0f);

		WinterIsland.GetComponent<SpriteRenderer>().enabled = true;
	}

	IEnumerator snowIslandTransition(){
		
		
		SummerIsland.GetComponent<Animator>().SetBool("IsWinter", false);
		
		WinterIsland.GetComponent<Animator>().SetBool("isWinter", false);
		
		yield return new WaitForSeconds (3.0f);
		
		WinterIsland.GetComponent<SpriteRenderer>().enabled = false;
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
			targetAmplitude = arduino3;
			arduino3 = 0;
		}
		
		
		// Temperature is a sinosodal relationship. User effects Amplitude of wave
		temperatureLevel = amplitude * Mathf.Sin ( ( 2 * Mathf.PI / Mathf.Abs(period) ) * ( timeIndex ));
		
		if (temperatureLevel <= -0) {
			
			isWinter = true;

			/*WinterIsland.GetComponent<SpriteRenderer>().enabled = true;
			SummerIsland.GetComponent<Animator>().SetBool("IsWinter", true);
			
			WinterIsland.GetComponent<Animator>().SetBool("isWinter", true);*/
			//hasDefaultFaded = false;

			StartCoroutine(islandTransition());

			//SnowSystem.SetActive(true);
			if(SnowSystem.GetComponent<ParticleEmitter>().maxEmission < rainLevel){
				SnowSystem.GetComponent<ParticleEmitter>().maxEmission += Time.deltaTime * transisionSpeed;
			} else {
				SnowSystem.GetComponent<ParticleEmitter>().maxEmission = rainLevel;
				//Do Nothing
			}
			
			if(RainSystem.GetComponent<ParticleEmitter>().maxEmission != 0){
				RainSystem.GetComponent<ParticleEmitter>().maxEmission -= Time.deltaTime * transisionSpeed;
				SpitSystem.GetComponent<ParticleEmitter>().maxEmission -= Time.deltaTime * transisionSpeed;
			} else {
				//Do Nothing
			}
		}
		
		if (temperatureLevel >= 1) {
			//SnowSystem.SetActive(true);

			StartCoroutine(snowIslandTransition());

			/*SummerIsland.GetComponent<Animator>().SetBool("IsWinter", false);
			
			WinterIsland.GetComponent<Animator>().SetBool("isWinter", false);*/

			if(RainSystem.GetComponent<ParticleEmitter>().maxEmission < rainLevel){
				RainSystem.GetComponent<ParticleEmitter>().maxEmission += Time.deltaTime * transisionSpeed;
				SpitSystem.GetComponent<ParticleEmitter>().maxEmission -= Time.deltaTime * transisionSpeed;
			} else {
				isWinter = false;
				//System.GetComponent<ParticleEmitter>().maxEmission = rainLevel;
				//Do Nothing
			}
			
			if(SnowSystem.GetComponent<ParticleEmitter>().maxEmission != 0){
				SnowSystem.GetComponent<ParticleEmitter>().maxEmission -= Time.deltaTime * transisionSpeed;
			} else {
				//SnowSystem.SetActive(false);
				//Do Nothing
			}
			
		}
		


	}

	void windUpdate(bool buffered){
		
		//Save the original particle velocity so that we can assign a new wind velocity later
		Vector3 originalVelocity = RainSystem.GetComponent<ParticleEmitter>().localVelocity;
		
		if (buffered) {
			windLevel--;
		}
		
		if (windLevel < 0) {
			windLevel = 0;		
		}
		
		if (newInput) {
			windLevel += arduino2;	
			arduino2 = 0;
		}
		
		int windState = 0;

		cropsObject.GetComponent<Animator>().SetFloat("WindLevel", windLevel);

		//Intesity of wind is based on a scale value, if the windLevel is between an arbitrary value of > 90 then a heavy wind will be applied to the rain
		if (windLevel > 90)
			windState = 3;
		else if (windLevel > 50)
			windState = 2;
		else if (windLevel > 0)
			windState = 1;
		
		
		if(buffered){
			switch (windState){
			case 0:
				//Debug.Log ("No Wind");
				
				RainSystem.GetComponent<ParticleEmitter>().localVelocity = new Vector3( 0 , originalVelocity.y, originalVelocity.z);
				SnowSystem.GetComponent<ParticleEmitter>().localVelocity = new Vector3( 0 , originalVelocity.y, originalVelocity.z);
				break;
			case 1:
				//Debug.Log ("Light Wind");
				
				RainSystem.GetComponent<ParticleEmitter>().localVelocity = new Vector3(-5, originalVelocity.y, originalVelocity.z);
				SnowSystem.GetComponent<ParticleEmitter>().localVelocity = new Vector3(-1 , originalVelocity.y, originalVelocity.z);
				break;
			case 2: 
				//Debug.Log ("Medium Wind");
				
				RainSystem.GetComponent<ParticleEmitter>().localVelocity = new Vector3(-10, originalVelocity.y, originalVelocity.z);
				SnowSystem.GetComponent<ParticleEmitter>().localVelocity = new Vector3(-1 , originalVelocity.y, originalVelocity.z);
				break;
			case 3: 
				//Debug.Log ("Heavy Wind");
				
				RainSystem.GetComponent<ParticleEmitter>().localVelocity = new Vector3(-20, originalVelocity.y, originalVelocity.z);
				SnowSystem.GetComponent<ParticleEmitter>().localVelocity = new Vector3(-1, originalVelocity.y, originalVelocity.z);
				break;
			default:
				Debug.Log("Error: no current rain state");
				break;
			}
		}
		
		
	}

	void rainUpdate(bool buffered){
		// Rain State Machine
		if (buffered)
			rainLevel--;
		
		if (rainLevel < 0) {
			rainLevel = 0;

			MedRainBG.GetComponent<Animator>().SetBool("MedBG_Visible", false);
			
			DefaultBG.GetComponent<Animator>().SetBool("Default_BG_Fade", false);
			hasDefaultFaded = false;

		}

		if ( newInput ) {
			rainLevel += arduino1;
			arduino1 = 0;
		}
		int rainState = 0;

		if (rainLevel > 500) {
			rainState = 3;
			if (hasDefaultFaded == false) {
				
				DefaultBG.GetComponent<Animator> ().SetBool ("Default_BG_Fade", true);
				hasDefaultFaded = true;
				
			} else {
				//Do nothing
			}
			
			MedRainBG.GetComponent<Animator> ().SetBool ("MedBG_Visible", true);
			
		} else if (rainLevel > 200) {
			rainState = 2;
			if (hasDefaultFaded == false) {
				
				DefaultBG.GetComponent<Animator> ().SetBool ("Default_BG_Fade", true);
				hasDefaultFaded = true;
				
			} else {
				//Do nothing
			}
			
			MedRainBG.GetComponent<Animator> ().SetBool ("MedBG_Visible", true);
			
		} else if (rainLevel > 0) {
			rainState = 1;
			if (hasDefaultFaded == false) {
				
				DefaultBG.GetComponent<Animator> ().SetBool ("Default_BG_Fade", true);
				hasDefaultFaded = true;
				
			} else {
				//Do nothing
			}
			
			MedRainBG.GetComponent<Animator> ().SetBool ("MedBG_Visible", true);
		}
		
		
		if( buffered && isWinter == false){
			switch (rainState)
			{
			case 0:
				//Debug.Log ( "No Rain " );
				//rainSystem.emissionRate = rainLevel;
				
				RainSystem.GetComponent<ParticleEmitter>().maxEmission = rainLevel;
				//SnowSystem.GetComponent<ParticleEmitter>().maxEmission = rainLevel;
				SpitSystem.GetComponent<ParticleEmitter>().maxEmission = rainLevel;
				
				/**
				Color noAplha = DefaultBG.GetComponent<Renderer>().material.color;
				noAplha.a = 0.0f;
				
				Color alpha = DefaultBG.GetComponent<Renderer>().material.color;
				alpha.a = 1.0f;
				//renderer.material.color = color;
				
				DefaultBG.GetComponent<Renderer>().material.color = alpha;
				LightRainBG.GetComponent<Renderer>().material.color = noAplha;
				MedRainBG.GetComponent<Renderer>().material.color = noAplha;
				HeavyRainBG.GetComponent<Renderer>().material.color = noAplha;
				**/
				break;
			case 1:
				//Debug.Log ( "Light Rain " );
				
				//rainSystem.emissionRate = rainLevel;
				RainSystem.GetComponent<ParticleEmitter>().maxEmission = rainLevel;
				//SnowSystem.GetComponent<ParticleEmitter>().maxEmission = rainLevel;
				SpitSystem.GetComponent<ParticleEmitter>().maxEmission = rainLevel;
				break;
			case 2: 
				//Debug.Log ( "Medium Rain ");
				
				//rainSystem.emissionRate = rainLevel;
				RainSystem.GetComponent<ParticleEmitter>().maxEmission = rainLevel;
				//SnowSystem.GetComponent<ParticleEmitter>().maxEmission = rainLevel;
				SpitSystem.GetComponent<ParticleEmitter>().maxEmission = rainLevel;
				break;
			case 3: 
				//Debug.Log ( "Heavy Rain ");
				//rainSystem.emissionRate = rainLevel;
				RainSystem.GetComponent<ParticleEmitter>().maxEmission = rainLevel;
				//SnowSystem.GetComponent<ParticleEmitter>().maxEmission = rainLevel;
				SpitSystem.GetComponent<ParticleEmitter>().maxEmission = rainLevel;
				
				//float lerp = Mathf.PingPong(Time.time, 2.0f) / 2.0f;
				//DefaultBG.GetComponent<Renderer>().material.Lerp(material1, material2, lerp);
				
				break;
			default:
				Debug.Log("Error: no current rain state");
				break;
			}
		}
	}
}
