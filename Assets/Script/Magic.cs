using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
	{

		// =======================
		// フィールド変数
		// =======================
		// 魔法の名前
		protected string name;
		// 消費MP
		protected int usemp;


		// =======================
		// コンストラクタ
		// =======================
		public Magic()
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
		 *魔法を行使する
		 * @param activePlayer : 行為プレイヤー
		 * @param passivePlayer : 対象プレイヤー
		 */
		public void effect(Player activePlayer, Player passivePlayer)
		{
			// 魔法ごとに処理をオーバーライドして記述する
		}


	}