using UnityEngine;
using System.Collections;

public class DropHelmet : MonoBehaviour
{
	public BaseCharacterController charater;
	
	// Update is called once per frame
	void Update ()
	{
		if( charater!=null && charater.isKnockedDown )
		{
			rigidbody.isKinematic = false;
			transform.parent = null;
			charater = null;
		}
	}
}
