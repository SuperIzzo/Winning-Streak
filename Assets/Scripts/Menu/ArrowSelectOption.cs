using UnityEngine;
using System.Collections;

public class ArrowSelectOption : MonoBehaviour {

	public Vector3 playPoint, exitPoint;
	int selection = 0;

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

		if(Input.GetButtonDown("Dash"))
		{
			if(selection == 0)
				Application.LoadLevel("main-2");
			else if(selection == 1)
				Application.LoadLevel("credits");
		}
	}
}
