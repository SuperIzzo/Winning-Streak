using UnityEngine;
using System.Collections;

public class GoalPostDance : MonoBehaviour
{
	public float scoreInterval;
	float timer = 0;

	void OnTriggerStay(Collider other)
	{
		if( other.CompareTag(Tags.player) )
		{
			if(other.GetComponent<BaseCharacterController>().isDancing)
			{
				timer += Time.deltaTime;

				if(timer > scoreInterval)
				{
					ScoreManager.AddScore(1);
					ScoreManager.AddMultPoint(0.1f);
					timer = 0;
				}

				if( GameSystem.audio )
				{
					if( GameSystem.audio.GetHype() > 0.8f && Random.value < 0.01f)
					{
						GameSystem.audio.PlayCelebrate();
						Debug.Log ("celebrate called");
					}

					//soundManager.GetComponent<DialogueManager>().PlaySpeech("PLAYER_DANCE");
					GameSystem.audio.KeepHypeUp();
				}
			}
		}
	}
}
