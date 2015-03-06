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
