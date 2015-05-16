/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                      SplashMenuSound.cs                     </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Jake Thorne                                            </author> * 
 * <date>    06-Mar-2015                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
using UnityEngine;
using System.Collections;

public class SplashMenuSound : MonoBehaviour {

     public static SplashMenuSound i;
     
     void Awake () 
     {
         if(!i) 
         {
             if (Application.loadedLevelName == "main-2")
             {
                 Destroy(gameObject);
                 Debug.Break();
                 return;
             }

             i = this;
             DontDestroyOnLoad(gameObject);
         }
         else Destroy(gameObject);
     }
}
