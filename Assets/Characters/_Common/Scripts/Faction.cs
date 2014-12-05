using UnityEngine;
using System.Collections;

public enum FactionID
{
	STREAKER,
	TEAM_A,
	TEAM_B
}

public class Faction : MonoBehaviour
{
	public FactionID faction;


	public bool IsAlly( FactionID otherFaction )
	{
		return this.faction == otherFaction;
	}

	public bool IsAlly( Faction faction )
	{
		if( faction )
			return IsAlly( faction.faction );
		else
			return false;
	}

	public bool IsEnemy( FactionID otherFaction )
	{
		return !IsAlly(otherFaction);
	}
	
	public bool IsEnemy( Faction faction )
	{
		if( faction )
			return IsEnemy( faction.faction );
		else
			return true;
	}
}
