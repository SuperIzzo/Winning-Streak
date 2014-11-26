using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemControl : MonoBehaviour {

	public GameObject powerupBars;
	public GameObject weaponSlot, hatSlot, scarfSlot;

	public GameObject hatFollow;
	
	public List<GameObject> weaponList;
	GameObject equippedWeapon;
	
	GameObject equippedHat;
	
	bool weaponEquipped, hatEquipped, scarfEquipped;
	
	float pickupRadius = 1;

	public float throwForce = 500;
	bool byKeyboard = false;

	public Animator animator;

	bool inThrowPowerup = false;
	float powerupTimer = 1;
	const float POWERUP_MAX = 3;

	void Start () 
	{
		weaponEquipped = false;
		hatEquipped = false;
		scarfEquipped = false;

		string hatToAdd = PlayerPrefs.GetString("ActiveHat");

		AddHat(GameObject.Find(hatToAdd));
	}

	void OnApplicationQuit()
	{
		PlayerPrefs.DeleteKey("ActiveHat");
	}
	
	void Update () 
	{
		if(Application.loadedLevelName == "menu")
			return;

		//pick up
		if( Input.GetButtonDown( "Grab" ))
		{
			if(!weaponEquipped)
			{
				foreach(GameObject wep in weaponList)
				{
					if(Vector3.Distance(wep.transform.position, this.transform.position) < pickupRadius)
					{
						equippedWeapon = wep;
						equippedWeapon.GetComponent<AssignSlot>().Equip(weaponSlot);

						ScoreManager.AddMultPoint(1);

						// Commentate the successful aquisition of a ball
						if( wep.gameObject.CompareTag("ball") )
						{
							Commentator commentator = Commentator.instance;
							commentator.Comment( CommentatorEvent.PICKED_BALL );
						}

						weaponEquipped = true;

						return;
					}
				}	
			}
			else
			{
				//throw
				inThrowPowerup = true;
				equippedWeapon.GetComponent<AssignSlot>().slowTimer = 0;
				powerupBars.GetComponent<PowerupVisuals>().EnableLow();
				
				if( animator )
				{
					animator.SetBool( "charge_throw", true );
				}
			}
		}

		// TODO: Expected fail! inThrowPowerup == true
		if(inThrowPowerup)
		{
			powerupTimer += Time.deltaTime;

			if(powerupTimer > 2)
				powerupBars.GetComponent<PowerupVisuals>().EnableMid();

			if(powerupTimer > 2.8f)
				powerupBars.GetComponent<PowerupVisuals>().EnableHigh();

			if( (!Input.GetButton("Grab") || powerupTimer > POWERUP_MAX))
			{
				equippedWeapon.GetComponent<AssignSlot>().Unequip();
				
				equippedWeapon.rigidbody.AddForce(this.transform.forward * throwForce * 2 / equippedWeapon.GetComponent<ItemStats>().weight);
				equippedWeapon.rigidbody.AddForce((this.transform.up * throwForce / equippedWeapon.GetComponent<ItemStats>().weight / 2) * powerupTimer);
				equippedWeapon.GetComponent<AssignSlot>().SetColliderOff();

				Throwable throwable = equippedWeapon.GetComponent<Throwable>();
				if( throwable )
				{
					throwable.Throw();
				}

				powerupBars.GetComponent<PowerupVisuals>().Shutdown();
				
				weaponEquipped = false;
				
				powerupTimer = 1;
				inThrowPowerup = false;

				if( animator )
				{
					animator.SetBool( "charge_throw", false );
				}
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
			hat.GetComponent<BoxCollider>().isTrigger = true;
			hat.GetComponent<Rigidbody>().useGravity = false;

			equippedHat = hat;

			if(equippedHat.name == "jseHat")
			{
				hatSlot.transform.position = new Vector3(-0.001532465f, 0.003497288f, -0.03810024f);
				Quaternion rot1 = hatSlot.transform.rotation;
				rot1.eulerAngles = new Vector3(275.2638f,343.5084f,197.6758f);
				hatSlot.transform.rotation = rot1;
				hatEquipped = true;

				return;
			}

			if(Application.loadedLevelName == "main-2")
				hatSlot.transform.position = hatFollow.transform.position + equippedHat.GetComponent<ItemStats>().hatSlotPos;
			else 
				hatSlot.transform.position = hatSlot.transform.position + equippedHat.GetComponent<ItemStats>().hatSlotPos;
				
			equippedHat.GetComponent<AssignSlot>().Equip(hatSlot);

			Quaternion rot = Quaternion.identity;
			rot.eulerAngles += equippedHat.GetComponent<ItemStats>().hatSlotRot;

				hatSlot.transform.rotation = rot;

			hatEquipped = true;

			return;
		}
		else
		{
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
