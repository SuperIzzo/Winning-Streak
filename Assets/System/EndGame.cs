using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour
{
	private BaseCharacterController player;
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
                hud.Hide();
                endGameDialogCountdown = 2.0f;
                //endGameDialogue.Show();
				ScoreManager score = GameSystem.score;
				Commentator commentator = GameSystem.commentator;

				commentator.Comment( CommentatorEvent.GAME_OVER );
				score.StopTimer();
            }
        }
        else
        {
			player = Player.characterController;
        }
	}
}
