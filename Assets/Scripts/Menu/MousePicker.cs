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
					Application.LoadLevel("main-2");
				}

				if(hit.collider.name == "exit")
					Application.LoadLevel("credits");

			}
		}
	}
}
