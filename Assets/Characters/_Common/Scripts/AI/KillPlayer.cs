using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour {

    float hitTime = 0.1f;
    float hitForce = 0.15f;

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "PlayerMesh")
		{
            if (!ReferenceManager.GetPlayer().GetComponent<BaseCharacterController>().isKnockedDown)
            {
                StartCoroutine("HitPlayer");
                ReferenceManager.GetPlayer().GetComponent<BaseCharacterController>().isKnockedDown = true;
            }
		}
	}

    IEnumerator HitPlayer()
    {
        float timer = 0;
        Vector3 direction = this.transform.forward;

        while (timer < hitTime)
        {
            timer += Time.deltaTime;
            ReferenceManager.GetPlayer().transform.position += direction * hitForce;

            yield return null;
        }
    }
}
