using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SplashScreen : MonoBehaviour {

    public List<GameObject> TransitionObjects;

    public float splashLength = 6;
    public float fadeInTime = 1;

    public float fadeInSpeed = 1;
    public float fadeOutSpeed = 0.5f;

    float timer = 0;
	
	void Start () 
    {
        foreach (GameObject go in TransitionObjects)
        {
            go.transform.localScale = Vector3.zero;
            go.GetComponent<Renderer>().material.color = new Color(Random.Range(0.0f, 1.0f),
                                                   Random.Range(0.0f, 1.0f),
                                                   Random.Range(0.0f, 1.0f));
        }
	}
	
	
	void Update () 
    {
        timer += Time.deltaTime;

        //if (timer > splashLength - fadeInTime)
        //{
        //    CrowdManager.MasterVolume -= fadeOutSpeed * Time.deltaTime;
        //}

        if (timer > splashLength - 1)
        {
            foreach (GameObject go in TransitionObjects)
            {
                go.transform.localScale += new Vector3(2* Time.deltaTime,2* Time.deltaTime,2* Time.deltaTime);
            }
        }

        if (timer > splashLength)
        {
            //CrowdManager.MasterVolume = 1;
            Application.LoadLevel("menu-3");
        }
	}
}
