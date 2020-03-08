﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Player
{

	// =======================
	// フィールド変数
	// =======================
	Magic[] healMagic = { new Heal() };
	// =======================
	// コンストラクタ
	// =======================
	public Hero(string name):base(name)
	{

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
		this.defaultHP = (GetNumber(0, 30) + 10) * 10;
		this.defaultMP = GetNumber(1, 30) + 20;
		this.hp = defaultHP;
		this.mp = defaultMP;

		this.str = GetNumber(2, 70) + 30;
		this.def = GetNumber(3, 70) + 30;
		this.luck = GetNumber(4, 100) + 1;
		this.agi = GetNumber(5, 50);
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
	 * 自身のHPが減っている場合は 回復魔法 を使用し、
	 * 自身のHPが減っていない場合は 対象プレイヤー に攻撃を行う
	 * @param attacker {@inheritDoc}
	 * @param defender {@inheritDoc}
	 */
	public void Attack(Player activePlayer, List<Player> passiveMembers)
	{

		Player passivePlayer = passiveMembers[UnityEngine.Random.Range(0, passiveMembers.Count-1)];

		// 与えるダメージを求める
		Console.WriteLine(activePlayer.GetName() + "　の　攻撃！");
		int damage = activePlayer.CalcDamage(passivePlayer);

		// 求めたダメージを対象プレイヤーに与える
		Console.WriteLine(passivePlayer.GetName() + "　に　" + damage + "　のダメージ！");
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
		Magic UseMagic = this.healMagic[UnityEngine.Random.Range(0, healMagic.Length-1)];

		if (UseMagic.GetUseMP() > this.mp)
		{

			return false;
		}

		UseMagic.effect(activePlayer, passivePlayer);

		return true;
	}

}
