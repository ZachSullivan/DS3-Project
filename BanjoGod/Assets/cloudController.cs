using UnityEngine;
using System.Collections;

public class cloudController : MonoBehaviour {
	public gameController gameController;
	public GameObject cloudObject;
	public float windSpeed;

	public Sprite[] cloudSprites;

	float scaleFactor;
	float speedVarier;
	// Use this for initialization
	void Start () {
		//transform.position = new Vector3 (Random.Range (7.0f, 11.0f), -0.25f, Random.Range (1, 5));

		if (transform.rotation.x != 90)
			transform.rotation = Quaternion.AngleAxis(90, Vector3.right);
	
		speedVarier = Random.Range (1.0f, 2.0f);

		scaleFactor = Random.Range (0.1f, 1.0f);

		transform.localScale = new Vector3 (scaleFactor, scaleFactor, 0);

		int loadSprite = Random.Range (0, 4);
		gameObject.GetComponent<SpriteRenderer> ().sprite = cloudSprites [loadSprite];
	}
	
	// Update is called once per frame
	void Update () {

		windSpeed = ( (0.1f) + (gameController.windLevel) ) / speedVarier;

		transform.Translate( Vector3.left * Time.deltaTime * windSpeed );

		if( transform.position.x <= -10 ){
			Instantiate ( cloudObject, new Vector3 (Random.Range (7.0f, 11.0f), Random.Range (-0.9f, -0.25f), Random.Range (1, 5)), Quaternion.identity );
			Destroy( gameObject );
		}
	}
}
