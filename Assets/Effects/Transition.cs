using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Transition : MonoBehaviour
{
    //TODO: UNHACK


    public List<GameObject> TransitionObjects;
    public List<GameObject> TextInScene;

    public bool isTimed = true;
    public float splashLength = 6;
    public float fadeTime = 0.5f;
    float fadeInSpeed = 2.5f;
    float fadeOutSpeed = 4.5f;

    float timer = 0;

    public void MenuToGame() { StartCoroutine("MenuPlay"); }

    void Start()
    {
        CrowdManager.MasterVolume = 1;

   

        foreach (GameObject go in TransitionObjects)
        {
            if (!go.activeSelf)
                go.SetActive(true);

            go.transform.localScale = new Vector3(2, 2, 2);
            go.GetComponent<Renderer>().material.color = new Color(Random.Range(0.0f, 1.0f),
                                                   Random.Range(0.0f, 1.0f),
                                                   Random.Range(0.0f, 1.0f));
        }

        foreach (GameObject go in TextInScene)
        {
            go.SetActive(false);
        }
    }

    IEnumerator MenuPlay()
    {
        float l_timer = 0;
        foreach (GameObject go1 in TextInScene)
        {
            if (go1.activeSelf)
                go1.SetActive(false);
        }

        while (l_timer < 0.5f)
        {
            l_timer += Time.deltaTime;
            foreach (GameObject go in TransitionObjects)
            {
                if (go.transform.localScale.x < 2)
                    go.transform.localScale += new Vector3(fadeOutSpeed * Time.deltaTime, fadeOutSpeed * Time.deltaTime, fadeOutSpeed * Time.deltaTime);
                
            }

            yield return null;
        }

        

        Application.LoadLevel("main-2");
    }


    void Update()
    {
        if (isTimed)
        {
            timer += Time.deltaTime;

            //going out
            if (timer > splashLength - fadeTime / 2)
            {
                if (Application.loadedLevelName == "splash")
                {
                    foreach (GameObject go in TransitionObjects)
                    {
                        if (go.transform.localScale.x < 2)
                        go.transform.localScale += new Vector3(fadeOutSpeed * Time.deltaTime, fadeOutSpeed * Time.deltaTime, fadeOutSpeed * Time.deltaTime);
                    }
                }
            }

            if (timer > splashLength)
            {
                //CrowdManager.MasterVolume = 1;
                if(Application.loadedLevelName == "splash")
                    Application.LoadLevel("menu-3");

                
            }

            if (timer < fadeTime * 4)
            {
                foreach (GameObject go in TransitionObjects)
                {
                    if (go.transform.localScale.x > 0)
                        go.transform.localScale -= new Vector3(fadeInSpeed * Time.deltaTime, fadeInSpeed * Time.deltaTime, fadeInSpeed * Time.deltaTime);
                    else if (go.transform.localScale.x < 0)
                    {
                        go.transform.localScale = Vector3.zero;

                        foreach (GameObject go1 in TextInScene)
                        {
                            if (!go1.activeSelf)
                                go1.SetActive(true);
                        }
                    }
                }
            }
        }
    }
}
