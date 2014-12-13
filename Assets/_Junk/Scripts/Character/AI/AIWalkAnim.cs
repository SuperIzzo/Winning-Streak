using UnityEngine;
using System.Collections;

public class AIWalkAnim : MonoBehaviour
{	
	public Animator animator;
	public float goofiness = 0f;

	Vector3 lastPos;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if( animator && Time.deltaTime>0 )
		{
			Vector3 deltaPos = transform.position - lastPos;
			lastPos = transform.position;

			float speed = Mathf.Clamp01( deltaPos.magnitude/Time.deltaTime );
			animator.SetFloat( "speed", speed );
		}
	}
}
