﻿using UnityEngine;
using System.Collections;

public class charController : MonoBehaviour {

	public Vector3 targetPos;
	public GameObject targetNode;
	private NavMeshAgent navComponent;
	public bool targetReached;


	// Use this for initialization
	void Start () {
		navComponent = this.transform.gameObject.GetComponent<NavMeshAgent>();
		targetPos = new Vector3 ( (float) Random.Range (-4, 4), 0.25f, (float) Random.Range (-2, 2));
		Instantiate (targetNode, targetPos, Quaternion.identity);

	}
	
	// Update is called once per frame
	void Update () {
		targetReached = false;

		if (!targetReached) {
			navComponent.SetDestination(targetPos);

		}

		if ( Vector3.Distance(targetPos, this.transform.position) <= 0.5f ) {
			targetReached = true;
			targetPos = new Vector3 ( (float) Random.Range (-4, 4), 0.25f, (float) Random.Range (-2, 2));
			targetNode.transform.position = targetPos;

		}
	}
}
