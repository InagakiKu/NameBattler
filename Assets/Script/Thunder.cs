using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : Magic
{

	// =======================
	// フィールド変数
	// =======================
	// 魔法の名前
	protected string name = "サンダー";
	// 消費MP
	protected int usemp = 20;

	// =======================
	// コンストラクタ
	// =======================
	public Thunder()
	{

	}

	// =======================
	// Getter / Setter
	// =======================
	// 魔法のMPの取得
	public string GetName()
	{
		return this.name;
	}

	public  int GetUseMP()
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
	 * 対称プレーヤーに電撃を加える
	 * @param activePlayer {@inheritDoc}
	 * @param passivePlayer {@inheritDoc}
	 */
	public override void effect(Player activePlayer, Player passivePlayer)
	{

		// MPが足りている場合の処理
		int damage = UnityEngine.Random.Range(10, 30);

		activePlayer.UseMP(this.usemp);

		LogText.AddLog(string.Format("{0} の {1}！", activePlayer.GetName(), this.name));
		LogText.AddLog($"{passivePlayer.GetName()} に {damage} のダメージ！");

		passivePlayer.Damage(damage);
	}
}