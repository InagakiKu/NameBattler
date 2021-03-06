﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Player
{

	// =======================
	// フィールド変数
	// =======================
	Magic[] attackMagic = { new Fire(), new Thunder() };
	// =======================
	// コンストラクタ
	// =======================
	public Wizard(string name) : base(name)
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
		// 魔術師のパラメータを名前から生成する
		this.defaultHP = (base.GetNumber(0, 10) + 5) * 10;
		this.defaultMP = base.GetNumber(1, 50) + 30;
		this.hp = base.defaultHP;
		this.mp = base.defaultMP;

		this.str = base.GetNumber(2, 50) + 1;
		this.def = base.GetNumber(3, 50) + 1;
		this.luck = base.GetNumber(4, 100) + 1;
		this.agi = base.GetNumber(5, 40) + 20;

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
	 * 攻撃側プレイヤー(atacker)のMPが魔法の消費MPに足りている場合は
	 * @param attacker {@inheritDoc}
	 * @param defender {@inheritDoc}
	 */
	public override void Attack(Player activePlayer, List<Player> passiveMembers)
	{
		Debug.Log("Wizard Attack");

		//攻撃対象の決定
		Player passivePlayer = passiveMembers[0];

		if (passiveMembers.Count > 1)
		{
			passivePlayer = passiveMembers[UnityEngine.Random.Range(0, passiveMembers.Count - 1)];

		}

		//使用する魔法を決定する
		Magic UseMagic = this.attackMagic[UnityEngine.Random.Range(0, attackMagic.Length)];

		Debug.Log(string.Format("Wizard Attack {0}", UseMagic.GetName()));
		if (UseMagic.GetUseMP() <= this.mp)
		{
			Debug.Log("Wizard Attack 魔法使えるとき");
			UseMagic.effect(activePlayer, passivePlayer);
			return;

		}
		// 与えるダメージを求める
		LogText.AddLog(string.Format("{0} の 攻撃！", activePlayer.GetName()));
		int damage = activePlayer.CalcDamage(passivePlayer);

		// 求めたダメージを対象プレイヤーに与える
		LogText.AddLog(string.Format("{0} に　{1｝ のダメージ！", passivePlayer.GetName(), damage));
		passivePlayer.Damage(damage);

		passivePlayer.Down();

	}
}
