using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StretchHack : MonoBehaviour
{

    private Vector3 startPosition;

    public List<GameObject> limbList = new List<GameObject>();
    private List<Vector3> positionList = new List<Vector3>();

    private float hackTimer = 0;
    public float hackDuration = 1.5f;

    void Start()
    {
        hackTimer = 0;

        foreach (GameObject go in limbList)
        {
            positionList.Add(go.transform.localPosition);
        }
    }

    void LateUpdate()
    {
        //If the model has gone ragdoll, implement the hack for a certain amount of time
        if (this.GetComponent<BaseCharacterController>().isKnockedDown && hackTimer < hackDuration)
        {
            hackTimer += Time.unscaledDeltaTime;

            for (int i = 0; i < limbList.Count; i++)
            {
                limbList[i].transform.localPosition = positionList[i];
                //limbList[i].rigidbody.Sleep();
            }
        }
    }
}



