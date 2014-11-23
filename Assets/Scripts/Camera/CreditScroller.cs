using UnityEngine;
using System.Collections;

public class CreditScroller : MonoBehaviour {

	public float scrollSpeed = 6;
	void Start () 
	{
		GameObject go = GameObject.Find("_GameManager");
		
		if(go)
		{
			Destroy (go);
		}

		StartCoroutine("Scroll");
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	IEnumerator Scroll()
	{
		while(this.transform.position.x < 30)
	 	{
			Vector3 newPos = this.transform.position;
			newPos.x += Time.deltaTime * scrollSpeed;

			if(Input.anyKeyDown || Input.GetButton("Dash") || Input.GetButton("Grab") || Input.GetButton("Wiggle"))
			{
				Application.Quit ();
			}
				
			if(this.transform.position.x > 29)
				Application.LoadLevel("menu");

			this.transform.position = Vector3.Lerp(this.transform.position, newPos,Time.deltaTime);

			yield return null;
		}

		Application.Quit ();
	}
}
