using UnityEngine;
using System.Collections;

public class terrainSwitcher : MonoBehaviour {
	public gameController gameController;
	public SpriteRenderer mySprite;
	public Sprite[] islandSprite;
	// Use this for initialization
	void Start () {
		mySprite = gameObject.GetComponent<SpriteRenderer> ();
		mySprite.sprite = islandSprite [0];
	}
	
	// Update is called once per frame
	void Update () {
		if( gameController.temperatureLevel > -10 ){
			mySprite.sprite = islandSprite[0];
		} else if( gameController.temperatureLevel > -20 && gameController.rainLevel > 0){
			mySprite.sprite = islandSprite[1];
		} else if( gameController.temperatureLevel > -25 && gameController.rainLevel > 50){
			mySprite.sprite = islandSprite[2];
		} else if( gameController.rainLevel > 250 )  {
			mySprite.sprite = islandSprite[3];
		}
	}
}
