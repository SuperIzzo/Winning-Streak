using UnityEngine;
using System.Collections;

public abstract class Damageable : MonoBehaviour
{
	public abstract void OnDamage( Damager damager );
}
