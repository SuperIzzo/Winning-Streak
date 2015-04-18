using UnityEngine;
using System.Collections;

public class CameraSlowRotate : MonoBehaviour {
    public float rotateSpeed = 1;

    private Vector3 rotateDirection;

    private float delay = 1, timer = 0;
    bool rotate = false;

    void Start()
    {
        rotateDirection = new Vector3(0, Random.RandomRange(0, 1), 0);

        if (rotateDirection.y == 0)
            rotateDirection.y = -1;
    }

	void Update () 
    {
        if (rotate)
            this.gameObject.transform.Rotate(rotateDirection, rotateSpeed * Time.deltaTime);
        else
        {
            timer += Time.deltaTime;

            if (timer > delay)
                rotate = true;
        }
	}
}
