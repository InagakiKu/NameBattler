using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Magic
{

	// =======================
	// フィールド変数
	// =======================
	// 魔法の名前
	protected string name = "ヒール";
	// 消費MP
	protected int usemp = 20;
	// 回復量
	protected int recoverhp = 50;

	// =======================
	// コンストラクタ
	// =======================
	public Heal() : base()
	{

	}

	// =======================
	// Getter / Setter
	// =======================

	// 回復するHPの取得
	public int GetRecoverHP()
	{
		return this.recoverhp;
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
	 * 対称プレーヤーの体力を回復する
	 * @param activePlayer {@inheritDoc}
	 * @param passivePlayer {@inheritDoc}
	 */
	public override void effect(Player activePlayer, Player passivePlayer)
	{
		// HPを 50 回復する

		activePlayer.UseMP(this.usemp);

		LogText.AddLog(string.Format("{0} の {1}！", activePlayer.GetName(), this.name));
		LogText.AddLog(string.Format("{0} の HP が  {1}　回復した！", passivePlayer.GetName(), this.recoverhp));

		passivePlayer.RecoverHP(this.recoverhp);


	}

}