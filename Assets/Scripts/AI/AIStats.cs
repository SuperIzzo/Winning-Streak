using UnityEngine;
using System.Collections;

public class AIStats : MonoBehaviour {

	//running speed for running after the player
	public float speed = 0;

	//willingness to chase the player 0 - 100 %
	public float chaseChance = 0;

	//recovery time to be normal after an attack
	public float recoveryTime = 0;
}
