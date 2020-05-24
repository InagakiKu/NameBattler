using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealStrategy : Strategy
{
	public HealStrategy() : base()
	{

	}
	public void StrategyAction(Player activePlayer, Party myParty, Party enemyParty)
	{

		// 能動側が戦士の時
		if (activePlayer is Fighter)
		{
			activePlayer.Attack(activePlayer, enemyParty.AttackTarget());
			return;
		}

		// 能動側が魔術師の時
		if (activePlayer is Wizard)
		{
			//攻撃
			activePlayer.Attack(activePlayer, enemyParty.AttackTarget());
			return;

		}

		if (activePlayer is Priest)
		{
			// 能動側が僧侶の時

			if (myParty.DebuffMembers().Count > 0)
			{
				if (activePlayer.HealDebuff(activePlayer, myParty.DebuffMembers()[0]))
				{
					return;
				}
			}
			//　回復する味方がいるとき
			if (myParty.HealMembers(90).Count > 0)
			{
				// 回復魔法を書けることができたとき
				if (activePlayer.HealHP(activePlayer, myParty.HealMembers(90)))
				{
					return;
				}
			}

			if (myParty.DebuffMembers().Count > 0)
			{
				//　状態異常魔法をかけられる敵がいるとき
				Player passivePlayer = myParty.DebuffMembers()[UnityEngine.Random.Range(0, myParty.DebuffMembers().Count - 1)];

				if (activePlayer.Debuff(activePlayer, passivePlayer))
				{
					return;
				}
			}



			activePlayer.Attack(activePlayer, enemyParty.AttackTarget());
			return;

		}

		// 能動側が勇者の時
		if (activePlayer is Hero)
		{
			if (myParty.HealMembers(90).Count > 0)
			{

				if (activePlayer.HealHP(activePlayer, myParty.HealMembers(90)))
				{
					// 回復魔法をかける
					return;
				}

			}

			activePlayer.Attack(activePlayer, enemyParty.AttackTarget());
			return;
		}
	}

}
