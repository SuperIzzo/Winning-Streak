﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUITransition : MonoBehaviour {

    public enum TransitionState { ENTER, EXIT };
    public TransitionState tranState = TransitionState.ENTER;

    public GameObject camera;

    public List<GameObject> guiText;
    public GameObject guiBackdrop;

    public float transitionSpeed = 1;
    public float delay = 1;


    private float originalScale;

    private string nextScene = "";

    float timer = 0;
    bool transitioned = false;
    bool disableText = false;

	void Start () 
    {
        originalScale = guiBackdrop.transform.localScale.x;
        ResetGUI();
	}

    public void ExitScene(string newScene)
    {
        nextScene = newScene;
        tranState = TransitionState.EXIT;
        ResetGUI();
    }

    void ResetGUI()
    {
        if (tranState == TransitionState.ENTER)
        {
            //reset scale
            Vector3 scale = guiBackdrop.transform.localScale;
            scale.x = 0;
            guiBackdrop.transform.localScale = scale;

            foreach (GameObject go in guiText)
            {
                go.SetActive(false);
            }
        }
        else if (tranState == TransitionState.EXIT)
        {
            //reset scale
            Vector3 scale = guiBackdrop.transform.localScale;
            scale.x = originalScale;

            guiBackdrop.transform.localScale = scale;
        }

        disableText = false;
        transitioned = false;
    }

	void Update () 
    {
        if (timer > delay)
        {
            if (!transitioned)
            {
                if (tranState == TransitionState.ENTER)
                {
                    Vector3 scale = guiBackdrop.transform.localScale;
                    scale.x += transitionSpeed * Time.deltaTime;
                    guiBackdrop.transform.localScale = scale;

                    if (guiBackdrop.transform.localScale.x > originalScale)
                    {
                        Vector3 scale0 = guiBackdrop.transform.localScale;
                        scale0.x = originalScale;
                        guiBackdrop.transform.localScale = scale0;
                        transitioned = true;

                        foreach (GameObject go in guiText)
                        {
                            go.SetActive(true);
                        }
                    }
                }
                else if (tranState == TransitionState.EXIT)
                {
                    if (!disableText)
                    {
                        foreach (GameObject go in guiText)
                        {
                            go.SetActive(false);
                        }

                        disableText = true;
                    }
                    Vector3 scale = guiBackdrop.transform.localScale;
                    scale.x -= transitionSpeed * Time.deltaTime;
                    guiBackdrop.transform.localScale = scale;

                    if (guiBackdrop.transform.localScale.x < 0)
                    {
                        Vector3 scale0 = guiBackdrop.transform.localScale;
                        scale0.x = 0;
                        guiBackdrop.transform.localScale = scale0;

                        Application.LoadLevel(nextScene);
                        transitioned = true;
                    }
                }
            }
        }
        else timer += Time.deltaTime;
	}
}
