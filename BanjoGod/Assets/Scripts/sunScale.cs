using UnityEngine;
using System.Collections;

public class sunScale : MonoBehaviour {
	public gameController gameController;
	float maxScaleX = 0.5f;
	float maxScaleY = 0.5f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float temp = gameController.temperatureLevel;
		temp += 30;
		transform.localScale = new Vector3 ( maxScaleX * ( temp / 60), maxScaleY  * (temp / 60), 0);
	}
}
