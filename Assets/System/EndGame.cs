using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour
{
	public BaseCharacterController player;
	public GUIWindow endGameDialogue;
	public GUIWindow hud;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if( player.isKnockedDown )
		{
			hud.Hide();
			endGameDialogue.Show();
		}
	}
}
