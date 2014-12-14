using UnityEngine;
using System.Collections;

public class CensorBarLookAt : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (ReferenceManager.GetCamera())
        {
            Quaternion rot = this.transform.rotation;
            Transform lookat = this.transform;

            lookat.LookAt(ReferenceManager.GetCamera().transform);

            rot.y = lookat.rotation.y;

            this.transform.rotation = rot;
        }
	}
}
