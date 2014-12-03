using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour
{
	public BaseCharacterController player;
	public GUIWindow endGameDialogue;
	public GUIWindow hud;

	private float endGameDialogCountdown = 0.0f;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if( endGameDialogCountdown> 0 )
		{
			endGameDialogCountdown -= Time.deltaTime;
			if( endGameDialogCountdown<=0 )
			{
				endGameDialogue.Show();
			}
		} else if( player.isKnockedDown )
		{
			hud.Hide();
			endGameDialogCountdown = 2.0f;
			//endGameDialogue.Show();
		}
	}
}
