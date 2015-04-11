using UnityEngine;
using System.Collections;

public class charController : MonoBehaviour {

	public Vector3 targetPos;
	public GameObject targetNode;
	private NavMeshAgent navComponent;
	public bool targetReached, facingRight;
	
	public Vector3 lastTransform;

	public Animator anim;

	public int currentState = 0;
	public int lastState = 0;

	// Use this for initialization
	void Start () {
		navComponent = this.transform.gameObject.GetComponent<NavMeshAgent>();
		targetPos = new Vector3 ( (float) Random.Range (-5, 5), 0.3499999f, (float) Random.Range ((float)-2.75, (float)-1.75));
		Instantiate (targetNode, targetPos, Quaternion.identity);
		facingRight = true;

		Animator[] animArray;
		
		animArray = GetComponentsInChildren<Animator>();
		foreach (Animator an in animArray) {
			anim = an;
		}
		lastTransform = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		if (facingRight) {
			if (gameObject.transform.position.x < lastTransform.x) {
				flip ();
			}
		} else {
			if (gameObject.transform.position.x > lastTransform.x) {
				flip ();
			}
		}

		float hor = gameObject.transform.position.x - lastTransform.x;
		float ver = gameObject.transform.position.z - lastTransform.z;

		if ( ( Mathf.Abs(ver) ) > Mathf.Abs(hor) ) {
			if( ver > 0 ){
				currentState = 1;
			}
			if( ver < 0 ){
				currentState = 2;
			}
		} else if ( ( Mathf.Abs(ver) ) < Mathf.Abs(hor) ){
			currentState = 3;
		} else {
			currentState = 0;
		}
		if( currentState != lastState ){
			if( currentState == 0 ){
				anim.SetBool ("Up",false);
				anim.SetBool ("Down",false);
				anim.SetBool ("Forward",false);
			} else if ( currentState == 1){
				anim.SetBool ("Up",true);
				anim.SetBool ("Down",false);
				anim.SetBool ("Forward",false);
			} else if ( currentState == 2){
				anim.SetBool ("Up",false);
				anim.SetBool ("Down",true);
				anim.SetBool ("Forward",false);
			} else if ( currentState == 3){
				anim.SetBool ("Up",false);
				anim.SetBool ("Down",false);
				anim.SetBool ("Forward",true);
			}
		}

		lastState = currentState;

		lastTransform = gameObject.transform.position;

		targetReached = false;

		if (!targetReached) {
			navComponent.SetDestination(targetPos);

		}

		if ( Vector3.Distance(targetPos, this.transform.position) <= 0.5) {
			targetReached = true;
			targetPos = new Vector3 ( (float) Random.Range (-5, 5), 0.3499999f, (float) Random.Range ((float)-2.75, (float)-1.75));
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
