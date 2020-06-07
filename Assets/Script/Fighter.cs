using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : Player
	{

		// =======================
		// フィールド変数
		// =======================
		// =======================
		// コンストラクタ
		// =======================
		public Fighter(string name) : base(name)
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
		protected override void MakeCharacter()
		{
			// 戦士のパラメータを名前から生成する
			this.defaultHP = (base.GetNumber(0, 30) + 10) * 10;
			this.defaultMP = base.GetNumber(1, 100) * 0;
			this.hp = base.defaultHP;
			this.mp = base.defaultMP;

			this.str = base.GetNumber(2, 100) + 30;
			this.def = base.GetNumber(3, 100) + 30;
			this.luck = base.GetNumber(4, 100) + 1;
			this.agi = base.GetNumber(5, 50) + 1;

			this.paralyze = false;
			this.poison = false;
			this.active = true;


		}


		// =======================
		// private メソッド
		// =======================

		// =======================
		// public メソッド
		// =======================

		/**
		 * {@inheritDoc}<br>
		 * @param attacker {@inheritDoc}
		 * @param defender {@inheritDoc}
		 */
		public override void Attack(Player activePlayer, List<Player> passiveMembers)
		{

		//攻撃対象の決定
		Player passivePlayer = passiveMembers[0];

		if (passiveMembers.Count > 1)
		{
			passivePlayer = passiveMembers[UnityEngine.Random.Range(0, passiveMembers.Count - 1)];

		}

		// 与えるダメージを求める
		LogText.AddLog(string.Format("{0} の攻撃！", activePlayer.GetName()));
			int damage = activePlayer.CalcDamage(passivePlayer);

		// 求めたダメージを対象プレイヤーに与える
			LogText.AddLog(string.Format("{0}に {1} のダメージ！", passivePlayer.GetName(), damage));
			passivePlayer.Damage(damage);

			passivePlayer.Down();
		}
	}