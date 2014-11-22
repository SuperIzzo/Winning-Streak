using UnityEngine;
using System.Collections;

public class ItemStats : MonoBehaviour {

	public float weight = 1;

	void Update()
	{
		if(this.rigidbody.velocity.x < 0.01f && this.rigidbody.velocity.y < 0.01f && this.rigidbody.velocity.z < 0.01f)
			this.rigidbody.Sleep();
	}
}
