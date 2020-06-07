using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultStrategy : Strategy
{

	public DefaultStrategy():base()
	{

	}
	public void StrategyAction(Player activePlayer, Party myParty, Party enemyParty)
	{
		Debug.Log("DefaultStrategy StrategyAction");
		// 能動側が戦士の時
		if (activePlayer is Fighter)
		{
			activePlayer.Attack(activePlayer, enemyParty.AttackTarget());
			Debug.Log("DefaultStrategy Fighter Attack");
			return;
		}

		// 能動側が魔術師の時
		if (activePlayer is Wizard)
		{
			//攻撃
			activePlayer.Attack(activePlayer, enemyParty.AttackTarget());
			Debug.Log("DefaultStrategy Wizard");
			return;
		}

		// 能動側が僧侶の時
		if (activePlayer is Priest)
		{
			Debug.Log("DefaultStrategy Priest");
			if (myParty.DebuffMembers().Count > 0)
			{
				Debug.Log("DefaultStrategy 自身のPartyに状態異常のPlayerがいる");
				if (activePlayer.HealDebuff(activePlayer, myParty.DebuffMembers()[0]))
				{
					Debug.Log("DefaultStrategy Priest HealDebuff");
					return;
				}
			}

			//　回復する味方がいるとき
			if (myParty.HealMembers(50).Count > 0)
			{
				Debug.Log("DefaultStrategy 回復魔法を書けることができたとき");
				// 回復魔法を書けることができたとき
				if (activePlayer.HealHP(activePlayer, myParty.HealMembers(50)))
				{
					Debug.Log("DefaultStrategy Priest HealHP");
					return;
				}

			}

			if (myParty.DebuffMembers().Count > 0)
			{
				Debug.Log("DefaultStrategy 状態異常魔法をかけられる敵がいるとき");
				//　状態異常魔法をかけられる敵がいるとき
				Player passivePlayer = myParty.DebuffMembers()[UnityEngine.Random.Range(0, myParty.DebuffMembers().Count - 1)];
				if (activePlayer.Debuff(activePlayer, passivePlayer))
				{
					Debug.Log("DefaultStrategy Priest Debuff");
					return;
				}
			}

			Debug.Log("DefaultStrategy Priest Attack");
			activePlayer.Attack(activePlayer, enemyParty.AttackTarget());

			return;
		}

		// 能動側が勇者の時
		if (activePlayer is Hero)
		{
			Debug.Log("DefaultStrategy Hero");
			if (myParty.HealMembers(50).Count > 0)
			{
				if (activePlayer.HealHP(activePlayer, myParty.HealMembers(50)))
				{
					// 回復魔法をかける
					Debug.Log("DefaultStrategy Hero HealHP");
					return;

				}

			}
			activePlayer.Attack(activePlayer, enemyParty.AttackTarget());
			Debug.Log("DefaultStrategy Hero Attack");
			return;
		}
	}
}