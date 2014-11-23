using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour {

	public GameObject thisGO;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			thisGO.GetComponent<AIAttack>().KillThePlayer();
			DisableColliders();
		}
	}

	void EnableColliders()
	{
		if(GetComponent<BoxCollider>())
		{
			GetComponent<BoxCollider>().enabled = true;
		}
		
		if(GetComponent<CapsuleCollider>())
		{
			GetComponent<CapsuleCollider>().enabled = true;
		}
	}

	void DisableColliders()
	{
		if(GetComponent<BoxCollider>())
		{
			GetComponent<BoxCollider>().enabled = false;
		}

		if(GetComponent<CapsuleCollider>())
		{
			GetComponent<CapsuleCollider>().enabled = false;
		}
	}
}
