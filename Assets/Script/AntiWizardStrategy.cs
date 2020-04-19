using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NameButtlerApplication
{

	public class AntiWizardStrategy : Strategy
	{

		public void StartegyAction(Player activePlayer, Party myParty, Party enemyParty)
		{

			// 能動側が戦士の時
			if (activePlayer is Fighter)
			{
				activePlayer.Attack(activePlayer, enemyParty.WizardTarget());
				return;
			}

			// 能動側が魔術師の時
			if (activePlayer is Wizard)
			{
				//攻撃
				activePlayer.Attack(activePlayer, enemyParty.WizardTarget());
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
				if (myParty.HealMembers(50).Count > 0)
				{
					// 回復魔法を書けることができたとき
					if (activePlayer.HealHP(activePlayer, myParty.HealMembers(50)))
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



				activePlayer.Attack(activePlayer, enemyParty.WizardTarget());
				return;

			}

			// 能動側が勇者の時
			if (activePlayer is Hero)
			{
				if (myParty.HealMembers(50).Count > 0)
				{

					if (activePlayer.HealHP(activePlayer, myParty.HealMembers(50)))
					{
						// 回復魔法をかける
						return;
					}

				}

				activePlayer.Attack(activePlayer, enemyParty.WizardTarget());
				return;
			}
		}

	}
}