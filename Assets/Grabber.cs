namespace RoaringSnail.WinningStreak
{
    using Characters;
    using UnityEngine;

    public class Grabber : MonoBehaviour
    {
        IGrabbingCharacter _grabber;
        
        protected void Awake()
        {
            _grabber = GetComponentInParent<IGrabbingCharacter>();
        }

        void OnTriggerEnter( Collider col )
        {            
            if( col.gameObject == _grabber.grabTarget )
            {
                _grabber.Grab();
            }
        }
    }
}