using UnityEngine;
using UnityEngine.UI;  // add to the top
using System.Collections;

public class Lightning : MonoBehaviour {

	/* **************************************

	TODO, ADD IN AUDIO SOUND EFFECT WHEN LIGHTNING IS TRIGGERED
	I left an audio source component for this.

	*****************************************/



	public Light LightningLight;
	public bool flash = false;
	public int fadeOffSpeed;

	private float startTime;
	private float maxIntensity;

	void Start(){

		startTime = Time.time;
		maxIntensity = 8;
	}

	void Update (){


		if (flash) {
			LightningLight.intensity = maxIntensity;

			float time = (Time.time - startTime) / fadeOffSpeed;
			
			float lightSmooth = Mathf.SmoothStep (0.0f, maxIntensity, time);


			LightningLight.intensity -= lightSmooth;

			if (LightningLight.intensity == 0){
				flash = false;
			}
				
		} else {

			LightningLight.intensity = 0;
		}

		if (flash == false){
			if (LightningLight.intensity ==  0){
				LightningLight.intensity = 8;
			}
		}

	}

}
