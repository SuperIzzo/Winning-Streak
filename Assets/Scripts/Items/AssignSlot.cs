using UnityEngine;
using System.Collections;

public class AssignSlot : MonoBehaviour {

	bool equipped = false;
	GameObject slot;

	public float slowTimer = 0;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		slowTimer += Time.deltaTime;

		if(slowTimer > 3)
		{
			if(this.rigidbody.velocity.x < 0.001f && this.rigidbody.velocity.y < 0.001f && this.rigidbody.velocity.z < 0.001f)
				this.rigidbody.Sleep();
		}

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

		this.rigidbody.isKinematic = true;

		if(GetComponent<BoxCollider>())
			this.GetComponent<BoxCollider>().enabled = false;
		
		if(GetComponent<MeshCollider>())
			this.GetComponent<MeshCollider>().enabled = false;
		
		if(GetComponent<CapsuleCollider>())
			this.GetComponent<CapsuleCollider>().enabled = false;

		this.transform.position = slot.transform.position;
		this.transform.rotation = slot.transform.rotation;
		this.transform.parent = slot.transform;
	}

	public void Unequip()
	{
		SetColliderOff();

		slowTimer = 0;
		this.rigidbody.isKinematic = false;
		this.transform.parent = null;

		if(this.tag == "hat")
		{
			this.rigidbody.AddForce(this.transform.up * 900);
			this.rigidbody.AddTorque(new Vector3(1000,1000,1000));
		}
		equipped = false;
	}

	public void SetColliderOff()
	{
		StartCoroutine("ColliderOff");
	}

	IEnumerator ColliderOff()
	{
		float timer = 0;
		slowTimer = 0;

		if(GetComponent<BoxCollider>())
			this.GetComponent<BoxCollider>().enabled = false;

		if(GetComponent<MeshCollider>())
			this.GetComponent<MeshCollider>().enabled = false;

		if(GetComponent<CapsuleCollider>())
			this.GetComponent<CapsuleCollider>().enabled = false;

		while(timer < 0.1f)
		{
			timer += Time.deltaTime;
			yield return null;
		}

		if(GetComponent<BoxCollider>())
			this.GetComponent<BoxCollider>().enabled = true;
		
		if(GetComponent<MeshCollider>())
			this.GetComponent<MeshCollider>().enabled = true;
		
		if(GetComponent<CapsuleCollider>())
			this.GetComponent<CapsuleCollider>().enabled = true;
	}

}





