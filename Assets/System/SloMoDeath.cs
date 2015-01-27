using UnityEngine;
using System.Collections;

public class SloMoDeath : MonoBehaviour
{
	BaseCharacterController character;

	// Use this for initialization
	void Start ()
	{
		character = GetComponent<BaseCharacterController>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if( character.isKnockedDown )
		{
		}
	}
}
