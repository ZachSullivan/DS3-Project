using UnityEngine;
using System.Collections;

public class changeBackground : MonoBehaviour
{
	//Sprite rotator
	public Quaternion from;
	public Quaternion to;
	public float speed = 0.1F;

	void FixedUpdate ()
	{
		/**
		transform.rotation = Quaternion.Slerp (from, to, Time.time * speed);
		transform.rotation = Quaternion.Euler (new Vector3 (0f, transform.rotation.eulerAngles.y, 0f));
		**/
		transform.Rotate (Vector3.forward * -speed);
	}
}
/**
public class changeBackground : MonoBehaviour
{
	//Sprite Switch
	public Sprite rainySky1;
	public Sprite rainySky2;
	public Sprite rainySky3;

	float timer = 1f;
	float delay = 1f;

	void Start ()
	{
		this.gameObject.GetComponent<SpriteRenderer>().sprite = rainySky1;
	}

	void Update ()
	{
		timer -= Time.deltaTime;

		if (timer <= 0)
		{

			if (this.gameObject.GetComponent<SpriteRenderer>().sprite == rainySky1){
				this.gameObject.GetComponent<SpriteRenderer>().sprite = rainySky2;
				timer = delay;
				return;
			}

			else if (this.gameObject.GetComponent<SpriteRenderer>().sprite == rainySky2){
				this.gameObject.GetComponent<SpriteRenderer>().sprite = rainySky3;
				timer = delay;
				return;
			}

			else if (this.gameObject.GetComponent<SpriteRenderer>().sprite == rainySky3){
				this.gameObject.GetComponent<SpriteRenderer>().sprite = rainySky1;
				timer = delay;
				return;
			}
		}
	}
}
**/