using UnityEngine;
using System.Collections;

public class AssignSlot : MonoBehaviour {

	bool equipped = false;
	GameObject slot;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(equipped)
		{
			if(this.tag == "weapon")
			{
				this.rigidbody.useGravity = false;
				this.transform.position = slot.transform.position;
				this.transform.rotation = slot.transform.rotation;
			}
			else
			{
				this.transform.position = slot.transform.position;
			}
		}
		else
		{
			if(this.tag == "weapon")
			{
				this.rigidbody.useGravity = true;
			}
		}
	}

	public void Equip(GameObject newSlot)
	{
		slot = newSlot;
		equipped = true;

		this.transform.position = slot.transform.position;
		this.transform.rotation = slot.transform.rotation;
		this.transform.parent = slot.transform;
	}

	public void Unequip()
	{
		this.transform.parent = null;
		equipped = false;
	}
}
