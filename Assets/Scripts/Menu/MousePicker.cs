using UnityEngine;
using System.Collections;

public class MousePicker : MonoBehaviour {

	//public GameObject play;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetMouseButtonDown(0))
		{
			RaycastHit hit = new RaycastHit();
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if(Physics.Raycast(ray,out hit, 100.0f))
			{
				if(hit.collider.name == "play")
					Application.LoadLevel("main-2");

				if(hit.collider.name == "exit")
					Application.Quit();

			}
		}
	}
}
