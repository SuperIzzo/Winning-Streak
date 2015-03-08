using UnityEngine;
using System.Collections;

public class TemporaryMenu : MonoBehaviour {

    public Transition transition;

    bool toGame = false;
    float timer = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (!toGame)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                transition.MenuToGame();
                toGame = true;
            }
        }
	}
}
