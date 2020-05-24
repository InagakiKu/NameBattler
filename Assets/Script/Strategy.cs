using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Strategy
{
	void StrategyAction(Player activePlayer, Party myParty, Party enemyParty);

}