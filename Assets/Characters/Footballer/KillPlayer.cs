using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour {

    float hitTime = 0.1f;
    float hitForce = 0.03f;

    public GameObject ThisGO;

    void Start()
    {
        //ThisGO = gameObject;
    }

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "PlayerMesh")
		{
            if (!ReferenceManager.GetPlayer().GetComponent<BaseCharacterController>().isKnockedDown)
            {
                StartCoroutine("HitPlayer");
                StartCoroutine("SlowMotionDeath");
                ReferenceManager.GetPlayer().GetComponent<BaseCharacterController>().isKnockedDown = true;
            }
		}
	}

    IEnumerator HitPlayer()
    {
        float timer = 0;
        Vector3 direction = ThisGO.transform.forward;

        while (timer < hitTime)
        {
            timer += Time.deltaTime;
            ReferenceManager.GetPlayer().transform.position += direction * hitForce;

            yield return null;
        }
    }

    IEnumerator SlowMotionDeath()
    {
        float timer = 0;

        //Small pause before activating slowmotion
        while (timer < 0.01f)
        {
            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        ReferenceManager.GetCamera().GetComponent<SloMoManager>().SlowMo();
        timer = 0;

        while (timer < 2)
        {
            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        ReferenceManager.GetCamera().GetComponent<SloMoManager>().NormalMo();
    }
}
