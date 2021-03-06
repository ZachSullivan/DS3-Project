﻿using UnityEngine;
using System.Collections;

public class BirdController : MonoBehaviour {

	public Vector3 targetPos;
	public GameObject targetNode;
	private NavMeshAgent navComponent;
	public bool targetReached;

	public Vector3 lastTransform;

	public Animator anim;

	public bool facingRight = true;

	// Use this for initialization
	void Start () {
		navComponent = this.transform.gameObject.GetComponent<NavMeshAgent>();
		targetPos = new Vector3 ( (float) Random.Range (-15, 15), 0.0f, (float) Random.Range (0, 7));
		Instantiate (targetNode, targetPos, Quaternion.identity);

		Animator[] animArray;

		animArray = GetComponentsInChildren<Animator>();
		foreach (Animator an in animArray) {
			anim = an;
		}
		lastTransform = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
			targetReached = false;
			
			if (gameObject.transform.position.x != lastTransform.x) {
				anim.SetBool("Forward", true);
			} else {
				anim.SetBool ("Forward", false);
			}
			
			if (facingRight) {
					if (gameObject.transform.position.x < lastTransform.x) {
							flip ();
					}
			} else {
				if (gameObject.transform.position.x > lastTransform.x) {
							flip ();
				}
			}
			if (gameObject.transform.position.z > lastTransform.z + 0.02) {
					anim.SetBool("Up", true);
			} else {
					anim.SetBool("Up", false);
			}
			if (gameObject.transform.position.z < lastTransform.z - 0.02) {
					anim.SetBool("Down", true);
			} else {
					anim.SetBool("Down", false);
			}
			
			lastTransform = gameObject.transform.position;
		
			if (!targetReached) {
				navComponent.SetDestination(targetPos);
				
			}
			
			if ( Vector3.Distance(targetPos, this.transform.position) <= 0.5) {
				targetReached = true;
				targetPos = new Vector3 ( (float) Random.Range (-15, 15), 0.0f, (float) Random.Range (0, 7));
				targetNode.transform.position = targetPos;
				
			}
	}

	void flip(){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;

		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
