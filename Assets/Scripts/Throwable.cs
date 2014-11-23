using UnityEngine;
using System.Collections;

public class Throwable : MonoBehaviour
{
	public Damager damager;
	int framesTillDisabled = 0;


	void Start()
	{
		if( !damager )
			damager = GetComponent<Damager>();

		if( !damager )
		{
			damager = gameObject.AddComponent<Damager>();
		}

		damager.enabled = false;
	}

	// Use this for initialization
	void OnCollisionEnter()
	{
		framesTillDisabled = 2;
	}


	public void Throw()
	{
		damager.enabled = true;
	}

	void Update()
	{
		if( framesTillDisabled>0 )
		{
			framesTillDisabled -= 1;

			if( framesTillDisabled<=0 )
			{
				damager.enabled = false;
			}
		}
	}
}
