using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemControl : MonoBehaviour {
	
	GameObject player;
	public GameObject powerupBars;
	public GameObject weaponSlot, hatSlot, scarfSlot;
	
	public List<GameObject> weaponList;
	GameObject equippedWeapon;
	
	GameObject equippedHat;
	
	bool weaponEquipped, hatEquipped, scarfEquipped;
	
	float pickupRadius = 1;

	public float throwForce = 500;
	bool byKeyboard = false;

	bool inThrowPowerup = false;
	float powerupTimer = 1;
	const float POWERUP_MAX = 3;

	void Start () 
	{
		player = gameObject;
		
		weaponEquipped = false;
		hatEquipped = false;
		scarfEquipped = false;

		string hatToAdd = PlayerPrefs.GetString("ActiveHat");
		Debug.Log (hatToAdd);

		AddHat(GameObject.Find(hatToAdd));
	}

	void OnApplicationQuit()
	{
		PlayerPrefs.DeleteKey("ActiveHat");
	}
	
	void Update () 
	{
		//pick up
		if(player.GetComponent<PlayerController>().TestButton("Y") || Input.GetKeyDown(KeyCode.O))
		{
			if(!weaponEquipped)
			{
				foreach(GameObject wep in weaponList)
				{
					if(Vector3.Distance(wep.transform.position, player.transform.position) < pickupRadius)
					{
						equippedWeapon = wep;
						equippedWeapon.GetComponent<AssignSlot>().Equip(weaponSlot);
						
						weaponEquipped = true;

						return;
					}
				}	
			}
			else
			{
				equippedWeapon.GetComponent<AssignSlot>().Unequip();
				weaponEquipped = false;
			}
		}

		//throw
		if((player.GetComponent<PlayerController>().TestButton("B") || Input.GetKeyDown(KeyCode.P))
		   && weaponEquipped)
		{
			if(Input.GetKey(KeyCode.P))
				byKeyboard = true;
			else byKeyboard = false;

			inThrowPowerup = true;
			powerupBars.GetComponent<PowerupVisuals>().EnableLow();

			this.GetComponentInChildren<AnimationController>().SetAnimationOn("charge_throw");
		}

		if(inThrowPowerup)
		{
			powerupTimer += Time.deltaTime;

			if(powerupTimer > 2)
				powerupBars.GetComponent<PowerupVisuals>().EnableMid();

			if(powerupTimer > 2.8f)
				powerupBars.GetComponent<PowerupVisuals>().EnableHigh();

			if(byKeyboard && (!Input.GetKey(KeyCode.P) || powerupTimer > POWERUP_MAX))
			{
				equippedWeapon.GetComponent<AssignSlot>().Unequip();
				
				equippedWeapon.rigidbody.AddForce(this.transform.forward * throwForce / equippedWeapon.GetComponent<ItemStats>().weight);
				equippedWeapon.rigidbody.AddForce((this.transform.up * throwForce / 3) * powerupTimer);
				equippedWeapon.GetComponent<AssignSlot>().SetColliderOff();
				
				powerupBars.GetComponent<PowerupVisuals>().Shutdown();
				
				weaponEquipped = false;
				
				powerupTimer = 1;
				inThrowPowerup = false;
			}

			if(!player.GetComponent<PlayerController>().TestButtonDown("B") || powerupTimer > POWERUP_MAX)
			{
				equippedWeapon.GetComponent<AssignSlot>().Unequip();
				
				equippedWeapon.rigidbody.AddForce(this.transform.forward * throwForce / equippedWeapon.GetComponent<ItemStats>().weight);
				equippedWeapon.rigidbody.AddForce((this.transform.up * throwForce / 3) * powerupTimer);
				equippedWeapon.GetComponent<AssignSlot>().SetColliderOff();

				powerupBars.GetComponent<PowerupVisuals>().Shutdown();
				this.GetComponentInChildren<AnimationController>().SetAnimationOff("charge_throw");

				weaponEquipped = false;

				powerupTimer = 1;
				inThrowPowerup = false;
			}
		}
	}

	public void AddHat(GameObject hat)
	{
		if(hat == equippedHat)
			return;

		if(Application.loadedLevelName == "menu")
		{
			Debug.Log("saved hat");
			PlayerPrefs.SetString("ActiveHat", hat.name);
		}

		hat.rigidbody.Sleep();

		if(equippedHat == null)
		{
			Debug.Log("equip hat");

			hat.GetComponent<BoxCollider>().isTrigger = true;
			hat.GetComponent<Rigidbody>().useGravity = false;

			equippedHat = hat;
			equippedHat.GetComponent<AssignSlot>().Equip(hatSlot);
			
			hatEquipped = true;

			return;
		}
		else
		{
			Debug.Log("unequip hat");

			equippedHat.rigidbody.AddForce(this.transform.up * throwForce);
			equippedHat.rigidbody.AddTorque(new Vector3(100,100,100));

			equippedHat.GetComponent<BoxCollider>().isTrigger = false;
			equippedHat.GetComponent<Rigidbody>().useGravity = true;
			equippedHat.GetComponent<AssignSlot>().Unequip();

			equippedHat = hat;
			equippedHat.GetComponent<BoxCollider>().isTrigger = true;
			equippedHat.GetComponent<Rigidbody>().useGravity = false;
			equippedHat.GetComponent<AssignSlot>().Equip(hatSlot);
			
			hatEquipped = true;

			return;
		}
	}
}
