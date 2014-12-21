using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NeighbourAvoidence : MonoBehaviour {

    Transform[] children;

	// Use this for initialization
	void Start () {
        children = GetComponentsInChildren<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider other)
	{
        if (other.transform == this.transform)
            return;

		if(other.gameObject.tag == "enemy")
		{
            Debug.Log("Avoiding");
			float angle = Mathf.Atan2(this.transform.position.x - (other.transform.position.x),// + offset.x),
			                          this.transform.position.z - (other.transform.position.z));// + offset.z));
			
			Vector3 velocity = new Vector3(Mathf.Sin (angle),
			                        0,
			                        Mathf.Cos (angle));
			
			this.transform.position = Vector3.Lerp(this.transform.position, 
			                                       this.transform.position + velocity, Time.deltaTime / 6);
		}
	}
}
