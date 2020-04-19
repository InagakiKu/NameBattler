using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : Magic
{

		// =======================
		// フィールド変数
		// =======================
		// 魔法の名前
		protected string name = "ファイヤー";
		// 消費MP
		protected int usemp = 10;

		// =======================
		// コンストラクタ
		// =======================
		public Fire()
		{

		}

		// =======================
		// Getter / Setter
		// =======================
		public string GetName()
		{
			return this.name;
		}

		public int GetUseMP()
		{
			return this.usemp;
		}

		// =======================
		// protected メソッド
		// =======================

		// =======================
		// private メソッド
		// =======================

		// =======================
		// public メソッド
		// =======================
		/**
		 * 対称プレーヤーに炎撃を加える
		 * @param activePlayer {@inheritDoc}
		 * @param passivePlayer {@inheritDoc}
		 */
		public void effect(Player activePlayer, Player passivePlayer)
		{

			// MPが足りている場合の処理

			int damage = UnityEngine.Random.Range(10, 30);

			activePlayer.UseMP(this.usemp);

			Console.WriteLine(activePlayer.GetName() + " の " + this.name + "！");
			Console.WriteLine(passivePlayer.GetName() + " に " + damage + " のダメージ！");

			passivePlayer.Damage(damage);
		}
	}