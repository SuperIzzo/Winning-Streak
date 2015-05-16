/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                       MenuControls.cs                       </file> * 
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
using UnityEngine.UI;

public class MenuControls : MonoBehaviour {

    public GameObject gamePadArrow1, gamePadArrow2;

    public List<GameObject> mainButtonList = new List<GameObject>();
    public List<GameObject> mouseButtons = new List<GameObject>();

    Vector3 arrowPos1;
    Vector3 arrowPos2;

    bool verticalUpPressed = false;
    bool verticalDownPressed = false;

    bool horizontalLeftPressed = false;
    bool horizontalRightPressed = false;

    //0 to 4
    int selection = 0;

    //customisation menu variables
    public GameObject PlayerType, HatType;
    public GameObject UpDownIcon, LeftRightIcon;
    public GameObject ExitSave, ExitDiscard;
    int typeSelection = 0;
    int playerTypeSelection = 0;
    int playerHatSelection = 0;

    enum CurrentMenu
    {
        SCORE,
        MAIN,
        CUSTOMISATION,
        HELP
    };

    CurrentMenu menu = CurrentMenu.MAIN;

    public void SetMenu(int m)
    {
        switch (m)
        {
            case 0:
                menu = CurrentMenu.SCORE;
                break;
            case 1:
                menu = CurrentMenu.MAIN;
                break;
            case 2:
                menu = CurrentMenu.CUSTOMISATION;
                break;
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetJoystickNames().Length > 0)
        {
            foreach (GameObject go in mouseButtons)
            {
                if (go.activeSelf)
                {
                    go.SetActive(false);
                }
            }

            if (!UpDownIcon.activeSelf)
                UpDownIcon.SetActive(true);

            if (!LeftRightIcon.activeSelf)
                LeftRightIcon.SetActive(true);

            if (menu == CurrentMenu.MAIN)
            {
                if (!gamePadArrow1.activeSelf)
                    gamePadArrow1.SetActive(true);

                if (!gamePadArrow2.activeSelf)
                    gamePadArrow2.SetActive(true);
            }

            if (!ExitSave.activeSelf)
                ExitSave.SetActive(true);

            if (!ExitDiscard.activeSelf)
                ExitDiscard.SetActive(true);
        }
        else
        {
            foreach (GameObject go in mouseButtons)
            {
                if (!go.activeSelf)
                {
                    go.SetActive(true);
                }
            }

            //if (ExitSave.activeSelf)
            //    ExitSave.SetActive(false);

            //if (ExitDiscard.activeSelf)
            //    ExitDiscard.SetActive(false);

            //if (UpDownIcon.activeSelf)
            //    UpDownIcon.SetActive(false);

            //if (LeftRightIcon.activeSelf)
            //    LeftRightIcon.SetActive(false);

            //if (gamePadArrow1.activeSelf)
            //    gamePadArrow1.SetActive(false);

            //if (gamePadArrow2.activeSelf)
            //    gamePadArrow2.SetActive(false);
            

            return;
        }

        if (menu == CurrentMenu.MAIN)
        {
            
            //up
            if (Input.GetAxis("Vertical") > 0)
            {
                if (selection != 0 && !verticalUpPressed)
                {
                    selection--;
                    verticalUpPressed = true;
                }
            }
            else verticalUpPressed = false;

            //down
            if (Input.GetAxis("Vertical") < 0)
            {
                if (selection != 4 && !verticalDownPressed)
                {
                    selection++;
                    verticalDownPressed = true;
                }
            }
            else verticalDownPressed = false;

            UpdateArrowPosition();

            if (Input.GetButtonDown("Dash") && menu == CurrentMenu.MAIN)
            {
                switch (selection)
                {
                    case 0:
                        StartGame();
                        break;
                    case 1:
                        this.GetComponent<Transitions>().ToScore();
                        break;
                    case 2:
                        this.GetComponent<Transitions>().ToCustomisation();
                        break;
                    case 3:

                        break;
                    case 4:
                        QuitGame();
                        break;
                }
            }
        }
        else if (menu == CurrentMenu.CUSTOMISATION)
        {
            //up
            if (Input.GetAxis("Vertical") > 0.5f)
            {
                if (!verticalUpPressed)
                {
                    CustomisationUp();
                }
            }
            else verticalUpPressed = false;

            //down
            if (Input.GetAxis("Vertical") < -0.5f)
            {
                if (!verticalDownPressed)
                {
                    CustomisationDown();
                }
            }
            else verticalDownPressed = false;

            //sideways type selection
            if (Input.GetAxis("Horizontal") > 0.25f || Input.GetAxis("Horizontal") < -0.25f)
            {
                if (Input.GetAxis("Horizontal") > 0.5f)
                {
                    if (!horizontalRightPressed)
                        CustomisationRight();
                }
                else horizontalRightPressed = false;

                if (Input.GetAxis("Horizontal") < -0.5f)
                {
                    if (!horizontalLeftPressed)
                        CustomisationLeft();
                }
                else horizontalLeftPressed = false;
            }

            //icon positioning
            if(typeSelection == 1)
                UpDownIcon.transform.localPosition = new Vector3(9, -18, 0);
            else
                UpDownIcon.transform.localPosition = new Vector3(-20.3f, -18, 0);

            


            //main menu buttons
            if (Input.GetButtonDown("Dash")) //save and exit
            {
                SaveCustomisation();
                this.GetComponent<Transitions>().ToMain();
            }
            else if (Input.GetButtonDown("Grab")) //discard and exit
            {
                DiscardCustomisation();
                this.GetComponent<Transitions>().ToMain();
            }  
        }
        else if (menu == CurrentMenu.SCORE)
        {
            if (Input.GetButtonDown("Grab"))
            {
                this.GetComponent<Transitions>().ToMain();
            }  
        }
	}

    public void CustomisationUp(int mouseButton = -1)
    {
        if (mouseButton == 0)
        {
            playerTypeSelection--;
            UpdatePlayerTypeText();
            return;
        }
        else if (mouseButton == 1)
        {
            playerHatSelection--;
            UpdatePlayerHatText();
            return;
        }

        if (typeSelection == 0 && playerTypeSelection != 0)
        {
            playerTypeSelection--;
            UpdatePlayerTypeText();

            verticalUpPressed = true;
        }
        else if (typeSelection == 1 && playerHatSelection != 0)
        {
            playerHatSelection--;
            UpdatePlayerHatText();

            verticalUpPressed = true;
        }
    }

    public void CustomisationDown(int mouseButton = -1)
    {
        if (mouseButton == 0)
        {
            playerTypeSelection++;
            UpdatePlayerTypeText();
            return;
        }
        else if (mouseButton == 1)
        {
            playerHatSelection++;
            UpdatePlayerHatText();
            return;
        }

        if (typeSelection == 0 && playerTypeSelection != 3)
        {
            playerTypeSelection++;
            UpdatePlayerTypeText();

            verticalDownPressed = true;
        }
        else if (typeSelection == 1 && playerHatSelection != 8)
        {
            playerHatSelection++;
            UpdatePlayerHatText();

            verticalDownPressed = true;
        }
    }

    public void CustomisationLeft()
    {
        typeSelection = 0;
        horizontalLeftPressed = true;
    }

    public void CustomisationRight()
    {
        typeSelection = 1;
        horizontalRightPressed = true;
    }

    public void UpdatePlayerTypeText()
    {
        switch (playerTypeSelection)
        {
            case 0:
                PlayerType.GetComponent<Text>().text = "FAT GUY";
                break;
            case 1:
                PlayerType.GetComponent<Text>().text = "FAT GIRL";
                break;
            case 2:
                PlayerType.GetComponent<Text>().text = "FIT GUY";
                break;
            case 3:
                PlayerType.GetComponent<Text>().text = "FIT GIRL";
                break;
        }
    }

    public void UpdatePlayerHatText()
    {
        switch (playerHatSelection)
        {
            case 0:
                HatType.GetComponent<Text>().text = "A MULLET.";
                break;
            case 1:
                HatType.GetComponent<Text>().text = "A HELMET.";
                break;
            case 2:
                HatType.GetComponent<Text>().text = "BANGS.";
                break;
            case 3:
                HatType.GetComponent<Text>().text = "A TOPHAT.";
                break;
            case 4:
                HatType.GetComponent<Text>().text = "A BUZZCUT.";
                break;
            case 5:
                HatType.GetComponent<Text>().text = "AN AFRO.";
                break;
            case 6:
                HatType.GetComponent<Text>().text = "BRAIN DAMAGE.";
                break;
            case 7:
                HatType.GetComponent<Text>().text = "AN OLD TIME-Y CAP.";
                break;
            case 8:
                HatType.GetComponent<Text>().text = "GOOD OLD BOLDNESS.";
                break;
        }
    }

    public void StartGame()
    {
        Application.LoadLevel("main-2");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SaveCustomisation()
    {

    }

    public void DiscardCustomisation()
    {

    }

    void UpdateArrowPosition()
    {
        if (menu == CurrentMenu.MAIN)
        {
            arrowPos2.x = 300 - (15 * selection);
            arrowPos1.x = -arrowPos2.x;

            arrowPos1.y = arrowPos2.y = 60 - (100 * selection);

            gamePadArrow1.transform.localPosition = arrowPos1;
            gamePadArrow2.transform.localPosition = arrowPos2;
        }
    }
}
