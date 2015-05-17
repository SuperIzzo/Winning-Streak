/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                         Controls.cs                         </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Hristoz Stefanov                                       </author> * 
 * <date>    27-Nov-2014                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour
{

	//public GameObject player;


	public AudioClip toHats;
	public AudioClip fromHats;
	AudioSource sound;
	
	public static int selection = 0;
	bool transitioning = false;

	// Use this for initialization
	void Start ()
	{
		sound = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		float x = Input.GetAxis("Horizontal");


		if(!transitioning)
		{
			if( x>0.2f && selection == 0 )
			{
				sound.PlayOneShot(toHats,0.5f);
				selection = 1;
				transitioning = true;

				return;
			}

			if( x<-0.2f && selection == 1 )
			{
				sound.PlayOneShot(fromHats,0.5f);
				selection = 0;
				transitioning = true;

				return;
			}

			if( x>0.2f && selection == 1 )
			{
				sound.PlayOneShot(toHats,0.5f);
				selection = 2;
				transitioning = true;

				return;
			}

			if( x<-0.2f && selection == 2 )
			{
				sound.PlayOneShot(fromHats,0.5f);
				selection = 1;
				transitioning = true;

				return;
			}
		}
		else
		{	
			//going to hatPicker
			if(selection == 2)
			{
				Quaternion toRot = Quaternion.identity;

				toRot.eulerAngles = new Vector3(0,177,0);

				this.transform.rotation = Quaternion.Lerp (this.transform.rotation,toRot,Time.deltaTime * 10);

				if(this.transform.eulerAngles.y > 175)
				{
					transitioning = false;
				}
			}

			//going to tutorial
			if(selection == 0)
			{
				Quaternion toRot = Quaternion.identity;
				
				toRot.eulerAngles = new Vector3(0,0,0);
				
				this.transform.rotation = Quaternion.Lerp (this.transform.rotation,toRot,Time.deltaTime * 10);
				
				if(this.transform.eulerAngles.y < 1)
				{
					transitioning = false;
				}
			}

			//going to mainmenu
			if(selection == 1)
			{
				Quaternion toRot = Quaternion.identity;
				
				toRot.eulerAngles = new Vector3(0,73,0);
				
				this.transform.rotation = Quaternion.Lerp (this.transform.rotation,toRot,Time.deltaTime * 10);
				
				if(this.transform.eulerAngles.y > 71 && this.transform.eulerAngles.y < 75)
				{
					transitioning = false;
				}
			}
		}
	
	}
}
