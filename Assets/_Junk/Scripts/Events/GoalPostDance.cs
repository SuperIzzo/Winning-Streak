using UnityEngine;
using System.Collections;

public class GoalPostDance : MonoBehaviour {

	private GameObject SoundManager;

	public float scoreInterval;

	float timer = 0;

	void Update()
	{
        if (!SoundManager)
            SoundManager = ReferenceManager.GetSoundManager();
	}

	void OnTriggerStay(Collider other)
	{
		if(other.tag == "Player")
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

                if (SoundManager.GetComponent<AudioMan>().GetHype() > 0.8f)
				{
					if(Random.value < 0.01f)
					{
                        SoundManager.GetComponent<AudioMan>().PlayCelebrate();
						Debug.Log ("celebrate called");
					}
				}

				//soundManager.GetComponent<DialogueManager>().PlaySpeech("PLAYER_DANCE");
                SoundManager.GetComponent<AudioMan>().KeepHypeUp();
			}
		}
	}
}
