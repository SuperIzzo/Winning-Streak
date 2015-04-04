using UnityEngine;
using System.Collections;

public class CameraSlowRotate : MonoBehaviour {
    public float rotateSpeed = 1;

    private Vector3 rotateDirection;

    void Start()
    {
        rotateDirection = new Vector3(0, Random.RandomRange(0, 1), 0);

        if (rotateDirection.y == 0)
            rotateDirection.y = -1;
    }

	void Update () 
    {
        this.gameObject.transform.Rotate(rotateDirection, rotateSpeed * Time.deltaTime);
	}
}
