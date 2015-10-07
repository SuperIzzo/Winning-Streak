using UnityEngine;
using System.Collections;

public class FireworksActivator : MonoBehaviour
{
    [SerializeField]
    GameObject _fireWorks;

	protected void OnCollisionEnter(Collision col)
    {
        if( Random.value < 0.3f )
        {
            _fireWorks.SetActive( !_fireWorks.activeSelf );
        }
    }
}
