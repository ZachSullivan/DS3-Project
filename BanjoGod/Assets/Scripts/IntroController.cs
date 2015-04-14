using UnityEngine;
using System.Collections;

public class IntroController : MonoBehaviour {

	public GameObject CrashSmoke;
	public GameObject CrashFire;

	public bool hasCrashed;

	public int transisionSpeed;
	public float killDelay;

	private float startTime;

	public int timePassed = 0;

	void Start () {

		hasCrashed = false;
		killDelay = 3.0f;

		startTime = Time.time;

		CrashSmoke.GetComponent<Renderer> ().sortingLayerName = "front";
		CrashFire.GetComponent<Renderer> ().sortingLayerName = "front";
	}
	
	// Update is called once per frame
	void Update () {
		introController ();
	}

	IEnumerator killSmoke(){

		yield return new WaitForSeconds (killDelay);

		float currentSEmmision = CrashSmoke.GetComponent<ParticleSystem> ().emissionRate;
		float currentFEmmision = CrashFire.GetComponent<ParticleSystem> ().emissionRate;

		float time = (Time.time - startTime) / transisionSpeed;

		float smokeSmooth = Mathf.SmoothStep (0.0f, currentSEmmision, time);
		float fireSmooth = Mathf.SmoothStep (0.0f, currentFEmmision, time);

		CrashSmoke.GetComponent<ParticleSystem> ().emissionRate -= smokeSmooth;
		CrashFire.GetComponent<ParticleSystem>().emissionRate -= fireSmooth;

	}

	void introController(){

		timePassed += (int) ( Time.time - timePassed );
		
		if (timePassed >= 9) {
			hasCrashed = true;
		}

		if (hasCrashed == true) {
			CrashSmoke.SetActive(true);
			CrashFire.SetActive(true);

			//Once the crash has happened, start a countdown before the smoke and fire dissipates
			StartCoroutine(killSmoke());
			//CrashSmoke.GetComponent<ParticleSystem>().emissionRate -= Time.deltaTime * transisionSpeed;
		}

		if (hasCrashed == false){
			if (CrashSmoke.GetComponent<ParticleSystem>().emissionRate == 0){
				CrashSmoke.GetComponent<ParticleSystem>().emissionRate = 1000;
			}
		}

	}

}
