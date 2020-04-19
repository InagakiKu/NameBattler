using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : Magic
	{

		// =======================
		// フィールド変数
		// =======================
		// 魔法の名前
		protected string name = "ポイズン";
		// 消費MP
		protected int usemp = 10;

		// =======================
		// コンストラクタ
		// =======================
		public Poison() : base()
		{

		}

		// =======================
		// Getter / Setter
		// =======================
		// 魔法のMPの取得
		public string GetUseName()
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
		 * 対称プレーヤーに毒状態を付与する<br>
		 * MPが足りない場合は、MPが足りないことを System.out で表示する<br>
		 * MPが足りている場合 かつ 毒状態になっていない場合、毒状態にする<br>
		 * MPが足りている場合 かつ 毒状態になっている場合、毒にかかっていることを System.out で表示する<br>
		 * @param activePlayer {@inheritDoc}
		 * @param passivePlayer {@inheritDoc}
		 */
		public void effect(Player activePlayer, Player passivePlayer)
		{

			// MPが足りている場合
			Console.WriteLine(activePlayer.GetName() + " の " + this.name);
			activePlayer.UseMP(this.usemp);

			// 毒にかかっているかの判定
			if (passivePlayer.isPoison() == false)
			{
				// 対称プレイヤーが毒にかかってない場合
				activePlayer.SetPoison(true);
				Console.WriteLine(passivePlayer.GetName() + " は 毒にかかった");

			}
			else
			{
				// 対称プレイヤーが毒にかかっている場合
				Console.WriteLine(passivePlayer.GetName() + " は すでに毒にかかっている");

			}
		}
}