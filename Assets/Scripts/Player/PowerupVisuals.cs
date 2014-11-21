using UnityEngine;
using System.Collections;

public class PowerupVisuals : MonoBehaviour {

	public GameObject lowBar, midBar, highBar;
	public GameObject player;

	void Update()
	{
		this.transform.position = player.transform.position + new Vector3(0,1.15f,-0.5f);
	}

	public void EnableLow()
	{
		lowBar.SetActive(true);
	}

	public void EnableMid()
	{
		midBar.SetActive(true);
	}

	public void EnableHigh()
	{
		highBar.SetActive(true);
	}

	public void Shutdown()
	{
		lowBar.SetActive(false);
		midBar.SetActive(false);
		highBar.SetActive(false);
	}
}
