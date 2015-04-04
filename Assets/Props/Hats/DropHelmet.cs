﻿using UnityEngine;
using System.Collections;

public class DropHelmet : MonoBehaviour
{
	public BaseCharacterController character;

	// Update is called once per frame
	void Update ()
	{
		if( character!=null && character.isKnockedDown )
		{
			GetComponent<Rigidbody>().isKinematic = false;
			transform.parent = null;
			character = null;
		}
	}
}
