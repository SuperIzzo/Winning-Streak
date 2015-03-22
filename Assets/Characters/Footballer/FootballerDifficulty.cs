using UnityEngine;
using System.Collections;


public class FootballerDifficulty : MonoBehaviour
{
	private AIInput aiInput;
	private BaseCharacterController controller;

	private float baseMovementSpeed = 0.0f;
	private float basePlayerHate	= 0.0f;

	// Use this for initialization
	void Start ()
	{
		aiInput = GetComponent<AIInput>();
		controller = GetComponent<BaseCharacterController>();

		if( controller )
			baseMovementSpeed = controller.movementSpeed;

		if( aiInput )
			basePlayerHate = aiInput.playerHate;
	}
	
	// Update is called once per frame
	void Update ()
	{
		float difficultyLevel = GameSystem.difficulty.level;

		if( aiInput )
			aiInput.playerHate = basePlayerHate + (1-basePlayerHate)*difficultyLevel;

		if( controller )
			controller.movementSpeed = baseMovementSpeed + difficultyLevel*2;
	
	}
}
