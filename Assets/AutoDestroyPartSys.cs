using UnityEngine;

public class AutoDestroyPartSys : MonoBehaviour
{
    private ParticleSystem ps;


    public void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    public void Update()
    {
        if( ps )
        {
            if( !ps.IsAlive() )
            {
                Destroy( gameObject );
            }
        }
    }
}