using UnityEngine;
using System.Collections;

public class TemporaryMenu : MonoBehaviour {

    public GameObject canvas;

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
                if (canvas.GetComponent<GUITransition>())
                {
                    canvas.GetComponent<GUITransition>().ExitScene("main-2");
                }
                toGame = true;
            }
        }
	}
}
