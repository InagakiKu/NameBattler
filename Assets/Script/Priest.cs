using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Priest : Player
	{

		// =======================
		// フィールド変数
		// =======================
		Magic[] healMagic = { new Heal(), new Recovery() };
		Magic[] debuffMagic = { new Paralyze(), new Poison() };

		// =======================
		// コンストラクタ
		// =======================
		public Priest(String name) : base(name)
		{
			this.name = name;
			MakeCharacter();
		}

		// =======================
		// Getter / Setter
		// =======================

		// =======================
		// protected メソッド
		// =======================
		/**
		 * 名前(name)からキャラクターに必要なパラメータを生成する
		 */
		protected void MakeCharacter()
		{
			// 戦士のパラメータを名前から生成する
			this.defaultHP = (base.GetNumber(0, 12) + 8) * 10;
			this.defaultMP = base.GetNumber(1, 50) + 30;
			this.str = (base.GetNumber(2, 50) + 1);
			this.def = base.GetNumber(3, 50) + 1;
			this.luck = base.GetNumber(4, 100);
			this.agi = base.GetNumber(5, 40) + 20;
			this.hp = defaultHP;
			this.mp = defaultMP;

			this.paralyze = false;
			this.poison = false;
			this.active = true;


		}


		/**
		 * {@inheritDoc}<br>
		 */
		public void Attack(Player activePlayer, List<Player> passiveMembers)
		{

			Player passivePlayer = passiveMembers[UnityEngine.Random.Range(0, passiveMembers.Count - 1)];

		// 与えるダメージを求める
			LogText.AddLog(string.Format("{0} の 攻撃！", activePlayer.GetName()));
			int damage = activePlayer.CalcDamage(passivePlayer);

			// 求めたダメージを対象プレイヤーに与える
			LogText.AddLog(string.Format("{0} に　{1｝ のダメージ！", passivePlayer.GetName(), damage));
			passivePlayer.Damage(damage);

			passivePlayer.Down();

		}

		/**
		 * {@inheritDoc}<br>
		 */
		public bool HealHP(Player activePlayer, List<Player> passiveMembers)
		{

			Player passivePlayer = passiveMembers[0];
			//使用する魔法を決定する
			Magic UseMagic = this.healMagic[0];

			if (UseMagic.GetUseMP() > this.mp)
			{

				return false;
			}

			UseMagic.effect(activePlayer, passivePlayer);

			return true;
		}

		public bool HealDebuff(Player activePlayer, List<Player> passiveMembers)
		{
			Player passivePlayer = passiveMembers[0];
			//使用する魔法を決定する
			Magic UseMagic = this.healMagic[1];

			if (UseMagic.GetUseMP() > this.mp)
			{

				return false;
			}

			UseMagic.effect(activePlayer, passivePlayer);

			return true;

		}

		/**
		 * {@inheritDoc}<br>
		 */

		public bool Debuff(Player activePlayer, Player passivePlayer)
		{

			if (!passivePlayer.isParalyze())
			{
				if (this.debuffMagic[0].GetUseMP() > this.mp)
				{
					return false;
				}
				this.debuffMagic[0].effect(activePlayer, passivePlayer);
				return true;
			}

			if (!passivePlayer.isPoison())
			{
				if (this.debuffMagic[1].GetUseMP() > this.mp)
				{
					return false;
				}
				this.debuffMagic[1].effect(activePlayer, passivePlayer);
				return true;
			}

			return false;
		}



		// =======================
		// private メソッド
		// =======================

		// =======================
		// public メソッド
		// =======================

	}
