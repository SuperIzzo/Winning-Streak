using UnityEngine;
using System.Collections;

public class GoalPostDance : MonoBehaviour {

	public GameObject soundManager;

	public float scoreInterval;

	float timer = 0;

	void Start()
	{
		soundManager = GameObject.FindGameObjectWithTag("SoundManager");
	}

	void OnTriggerStay(Collider other)
	{
		if(other.tag == "Player")
		{
			if(other.GetComponent<PlayerController>().dancing)
			{
				timer += Time.deltaTime;

				if(timer > scoreInterval)
				{
					ScoreManager.AddScore(1);
					timer = 0;
				}

				if(soundManager.GetComponent<AudioMan>().GetHype() > 0.8f)
				{
					if(Random.value < 0.01f)
					{
						soundManager.GetComponent<AudioMan>().PlayCelebrate();
						Debug.Log ("celebrate called");
					}
				}

				//soundManager.GetComponent<DialogueManager>().PlaySpeech("PLAYER_DANCE");
				soundManager.GetComponent<AudioMan>().KeepHypeUp();
			}
		}
	}
}
