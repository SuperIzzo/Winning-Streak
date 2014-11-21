using UnityEngine;
using System.Collections;

public class VanityPicker : MonoBehaviour {

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
				if(hit.collider.tag == "hat")
				{
					this.GetComponent<ItemControl>().AddHat(hit.collider.gameObject);
				}
				
				//if(hit.collider.name == "scarf")
				//	Application.Quit();
				
			}
		}
	}
}
