using UnityEngine;
using System.Collections;

public class ItemStats : MonoBehaviour {

	public float weight = 1;

	public Vector3 hatSlotPos = new Vector3();

	void Update()
	{
		if(this.rigidbody.velocity.x < 0.01f && this.rigidbody.velocity.y < 0.01f && this.rigidbody.velocity.z < 0.01f)
			this.rigidbody.Sleep();
	}
}
