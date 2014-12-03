using UnityEngine;
using System.Collections;

public class KillPlayerZone : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			other.GetComponent<BaseCharacterController>().KnockDown();
		}
	}
}
