using UnityEngine;
using System.Collections;

public class MousePicker : MonoBehaviour {

	//public GameObject play;

	public GameObject play;
	
	bool startedMatch = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () 
	{
		if(startedMatch) 
			return;



		if(Input.GetMouseButtonDown(0))
		{
			RaycastHit hit = new RaycastHit();
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if(Physics.Raycast(ray,out hit, 100.0f))
			{
				if(hit.collider.name == "play")
				{
					StartGameCommenter.announced = false;
					startedMatch = true;
					StartCoroutine("StartMatch");
				}

				if(hit.collider.name == "exit")
					Application.LoadLevel("credits");

			}
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
