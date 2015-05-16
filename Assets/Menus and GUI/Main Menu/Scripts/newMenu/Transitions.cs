/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                        Transitions.cs                       </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Jake Thorne                                            </author> * 
 * <date>    07-Dec-2014                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Transitions : MonoBehaviour {

    public GameObject Jumbotron;

    public List<GameObject> mainMenuObjects = new List<GameObject>();
    public List<GameObject> customiseObjects = new List<GameObject>();
    public List<GameObject> scoreObjects = new List<GameObject>();

    //original positions to switch back to that menu
    List<Vector3> mainMenuOriginalPositions = new List<Vector3>();
    List<Vector3> customiseOriginalPositions = new List<Vector3>();
    List<Vector3> scoreOriginalPositions = new List<Vector3>();

    [HideInInspector]
    public bool inTransistion = false;
    int offset = 1400;
    float speed = 12;

	// Use this for initialization
	void Start () 
    {
        foreach (GameObject go in mainMenuObjects)
        {
            //mainMenuOriginalPositions.Add(go.transform.position);
        }

        foreach (GameObject go in customiseObjects)
        {
            //customiseOriginalPositions.Add(go.transform.position);
        }

        foreach (GameObject go in scoreObjects)
        {
           // scoreOriginalPositions.Add(go.transform.position);
        }
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ToMain()
    {
        if (!inTransistion)
        {
            StartCoroutine("TransistionToMain");
        }
    }

    public void ToCustomisation()
    {
        Debug.Log("In ToCust");
        if (!inTransistion)
        {
            for (int i = 0; i < mainMenuObjects.Count; i++)
            {
                if (mainMenuObjects[i].name == "MM_AcceptButton")
                {
                    mainMenuObjects[i].SetActive(false);
                }
            }

            StartCoroutine("TransistionToCustomisation");
        }
    }

    public void ToScore()
    {
        if (!inTransistion)
        {
            for (int i = 0; i < mainMenuObjects.Count; i++)
            {
                if (mainMenuObjects[i].name == "MM_AcceptButton")
                {
                    mainMenuObjects[i].SetActive(false);
                }
            }

            StartCoroutine("TransistionToScore");
        }
    }

    IEnumerator TransistionToMain()
    {
        inTransistion = true;

        float increment = Jumbotron.transform.rotation.eulerAngles.y < 0 ? 2 : -2;

        float countDown = 90;

        Quaternion rot = Jumbotron.transform.rotation;

        //coming from customisation
        if (increment == -2)
        {
            while (countDown > 0)
            {
                rot.eulerAngles += new Vector3(0, increment, 0);
                countDown += increment;
                Jumbotron.transform.rotation = rot;

                yield return null;
            }
        }
        else //coming from scoreboard
        {

        }

        //for (int i = 0; i < mainMenuObjects.Count; i++)
        //{
        //    if (mainMenuObjects[i].name == "MM_AcceptButton")
        //    {
        //        mainMenuObjects[i].SetActive(true);
        //    }
        //}

        this.GetComponent<MenuControls>().SetMenu(1);
        inTransistion = false;
    }

    IEnumerator TransistionToCustomisation()
    {
        Debug.Log("In ToCust IEnum");
        inTransistion = true;
        this.GetComponent<MenuControls>().SetMenu(2);

        float increment = 2;

        float countDown = 90;

        Quaternion rot = Jumbotron.transform.rotation;

        while (countDown > 0)
        {
            
            rot.eulerAngles += new Vector3(0, increment, 0);
            Debug.Log(rot.eulerAngles);
            countDown -= increment;
            Jumbotron.transform.rotation = rot;
            //Quaternion.Slerp(Jumbotron.transform.rotation, rot, increment);

            //for (int i = 0; i < mainMenuObjects.Count; i++)
            //{
            //    mainMenuObjects[i].transform.position = Vector3.Lerp(mainMenuObjects[i].transform.position,
            //                                                         mainMenuOriginalPositions[i] - new Vector3(offset, 0, 0),
            //                                                         Time.deltaTime * speed);
            //}

            //for (int i = 0; i < customiseObjects.Count; i++)
            //{
            //    customiseObjects[i].transform.position = Vector3.Lerp(customiseObjects[i].transform.position,
            //                                                          customiseOriginalPositions[i] - new Vector3(offset, 0, 0),
            //                                                          Time.deltaTime * speed);
            //}

            //for (int i = 0; i < scoreObjects.Count; i++)
            //{
            //    scoreObjects[i].transform.position = Vector3.Lerp(scoreObjects[i].transform.position,
            //                                                      scoreOriginalPositions[i] - new Vector3(offset, 0, 0),
            //                                                      Time.deltaTime * speed);
            //}


            //inTransistion = customiseObjects[0].transform.localPosition.x > 1 ? true : false;

            yield return null;
        }

        Debug.Log("Done ToCust IEnum");

        inTransistion = false;
    }

    IEnumerator TransistionToScore()
    {
        inTransistion = true;
        this.GetComponent<MenuControls>().SetMenu(0);

        while (inTransistion)
        {
            for (int i = 0; i < mainMenuObjects.Count; i++)
            {
                mainMenuObjects[i].transform.position = Vector3.Lerp(mainMenuObjects[i].transform.position,
                                                                     mainMenuOriginalPositions[i] + new Vector3(offset, 0, 0),
                                                                     Time.deltaTime * speed);
            }

            for (int i = 0; i < customiseObjects.Count; i++)
            {
                customiseObjects[i].transform.position = Vector3.Lerp(customiseObjects[i].transform.position,
                                                                      customiseOriginalPositions[i] + new Vector3(offset, 0, 0),
                                                                      Time.deltaTime * speed);
            }

            for (int i = 0; i < scoreObjects.Count; i++)
            {
                scoreObjects[i].transform.position = Vector3.Lerp(scoreObjects[i].transform.position,
                                                                  scoreOriginalPositions[i] + new Vector3(offset, 0, 0),
                                                                  Time.deltaTime * speed);
            }

            inTransistion = scoreObjects[0].transform.localPosition.x < -1 ? true : false;

            yield return null;
        }

        inTransistion = false;
    }
}
