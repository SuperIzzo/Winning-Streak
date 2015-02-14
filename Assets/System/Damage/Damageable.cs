using UnityEngine;
using System.Collections;

//--------------------------------------------------------------
/// <summary> An abstract damageable class. </summary>
//--------------------------------------
public abstract class Damageable : MonoBehaviour
{
	//--------------------------------------------------------------
	/// <summary> Callback that raises the damage event. </summary>
	/// <param name="damager">The <see cref="Damager"/> 
	/// that cause the damage.</param>
	//--------------------------------------
	public abstract void OnDamage( Damager damager );
}
