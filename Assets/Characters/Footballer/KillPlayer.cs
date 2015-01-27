using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour {

    /* Rework this in the future, so hacky atm
     */

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
			BaseCharacterController controller = Player.characterController;
			if (!controller.isKnockedDown)
            {
                StartCoroutine("HitPlayer");
                //StartCoroutine("SlowMotionDeath");
				controller.isKnockedDown = true;
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
			GameObject player = Player.gameObject;
			player.transform.position += direction * hitForce;

            yield return null;
        }
    }

    IEnumerator SlowMotionDeath()
    {
        float timer = 0;

        //Small pause before activating slowmotion
        while (timer < 0.04f)
        {
            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        GameSystem.slowMotion.SlowMo();
        timer = 0;

        while (timer < 2)
        {
            timer += Time.unscaledDeltaTime;
            yield return null;
        }

		GameSystem.slowMotion.NormalMo();
    }
}
