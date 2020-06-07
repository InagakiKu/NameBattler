using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralyze : Magic
{

	// =======================
	// フィールド変数
	// =======================
	// 魔法の名前
	protected string name = "パライズ";
	// 消費MP
	protected int usemp = 10;
	// =======================
	// コンストラクタ
	// =======================
	public Paralyze() : base()
	{

	}
	// =======================
	// Getter / Setter
	// =======================

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
	 * 防御側プレイヤーを確率で毒状態にする
	 * @param activePlayer : 魔法を行使するプレイヤー
	 * @param passivePlayer : 対象プレイヤー
	 */
	public override void effect(Player activePlayer, Player passivePlayer)
	{
		// MPが足りている場合の処理
		// 魔法を行使する側のMPを使用する

		LogText.AddLog(string.Format("{0} の {1}！", activePlayer.GetName(), this.name));
		activePlayer.UseMP(this.usemp);
		// 魔法の成功判定をする
		if (RandomGenerator.RandomJudge(20))
		{
			// 判定に成功した時
			passivePlayer.SetParalyze(true);
			LogText.AddLog(string.Format("{0} は 麻痺 にかかった", passivePlayer.GetName()));
		}
		else
		{
			// 判定に失敗した時
			LogText.AddLog(string.Format("{0} には何も起こらなかった", passivePlayer.GetName()));
		}
	}
}