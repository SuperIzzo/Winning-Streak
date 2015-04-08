using UnityEngine;
using System.Collections;

// TODO: This file reaks of smelly code, it needs urgent sanitation

public class EndGame : MonoBehaviour
{
	private BaseCharacterController player;
	public GUIWindow endGameDialogue;
	public GUIWindow hud;

	private bool announced = false;
	private float endGameDialogCountdown = 0.0f;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
        if (player)
        {
            if (endGameDialogCountdown > 0)
            {
                endGameDialogCountdown -= Time.deltaTime;
                if (endGameDialogCountdown <= 0)
                {
                    endGameDialogue.Show();
                }
            }
            else if (player.isKnockedDown)
            {
		if( !announced )
		{
			announced = true;

	                hud.Hide();
	                endGameDialogCountdown = 2.0f;
	                //endGameDialogue.Show();
			
			Score score = Player.p1.score;
			float highScore = Persistence.GetFloat("HighScore", 0);
			
			score.enabled = false;
			GameSession.current.enabled = false;

			if( score.total>highScore )
			{
				highScore = score.total;
				Persistence.SetFloat("HighScore", highScore); 
			}

			Commentator.Comment( CommentatorEvent.GAME_OVER );
		}
            }
	    else
	    {
		announced = false;
	    }
        }
        else
        {
			player = Player.p1.characterController;
        }
	}
}
