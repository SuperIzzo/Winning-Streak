using UnityEngine;
using System.Collections;

public class KillPlayerZone : MonoBehaviour
{
	void OnCollisionEnter(Collision col)
	{
        if(col.gameObject.GetComponentInParent<BaseCharacterController>())
            col.gameObject.GetComponentInParent<BaseCharacterController>().KnockDown();
	
	}
}
