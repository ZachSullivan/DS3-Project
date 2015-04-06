using UnityEngine;
using System.Collections;

public class BirdController : MonoBehaviour {

	public Vector3 targetPos;
	public GameObject targetNode;
	public GameObject idleNode;
	private NavMeshAgent navComponent;

	public bool targetReached;
	public bool perch;

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
				float randVal = Random.Range(0,100);
				if( (perch == false && randVal >= 20) || (perch == true && randVal >= 99) ){
					perch = false;
					anim.SetBool("Idle",false);
					targetReached = true;
					targetPos = new Vector3 ( (float) Random.Range (-15, 15), 0.0f, (float) Random.Range (0, 7));
					targetNode.transform.position = targetPos;
				} else {
					if( targetPos == idleNode.transform.position ){
						anim.SetBool("Idle",true);
					}
					targetReached = true;
					targetPos = idleNode.transform.position;
					targetNode.transform.position = targetPos;
					perch = true;
				}
				
			}
	}

	void flip(){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;

		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
