using UnityEngine;
using System.Collections;

public class ArrowSelectOption : MonoBehaviour {

	public Vector3 playPoint, exitPoint;
	int selection = 0;

	public GameObject play;

	bool startedMatch = false;

	void Start () 
	{
		this.transform.position = playPoint;
	}

	void Update () 
	{
		if(Controls.selection != 0)
			return;

		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");

		if(selection == 0)
		{
			this.transform.position = playPoint;
		}
		else if(selection == 1)
		{
			this.transform.position = exitPoint;
		}

		if(selection == 0)
		{
			if(y < -0.5f)
				selection = 1;
		}

		if(selection == 1)
		{
			if(y > 0.5f)
				selection = 0;
		}

		if(Input.GetButtonDown("Dash") && !startedMatch)
		{
			if(selection == 0)
			{
				StartGameCommenter.announced = false;
				startedMatch = true;
				StartCoroutine("StartMatch");
			}
			else if(selection == 1)
				Application.LoadLevel("credits");
		}
	}

	IEnumerator StartMatch()
	{
		float timer = 0;

		while (timer < 18)
		{
			timer += Time.deltaTime;

			play.GetComponent<TextMesh>().text = "" +  (18 - (int)timer);

			if(Input.GetButtonDown("Dash") && timer > 2.5f)
				timer = 18;

			yield return null;
		}

		Application.LoadLevel("main-2");
	}

}






