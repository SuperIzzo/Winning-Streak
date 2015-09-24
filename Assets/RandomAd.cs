using UnityEngine;
using System.Collections.Generic;

public class RandomAd : MonoBehaviour
{
    [SerializeField]
    List<Mesh> _meshes;

    [SerializeField]
    List<Material> _materials; 


	// Use this for initialization
	void Start ()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
	
        if( meshFilter )
        {
            meshFilter.mesh = _meshes[Random.Range( 0, _meshes.Count )];
        }
	}
}
