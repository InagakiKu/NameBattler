using System;
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
	public Wizard(string name) :base(name)
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
		// 魔術師のパラメータを名前から生成する
		this.defaultHP = (GetNumber(0, 10) + 5) * 10;
		this.defaultMP = GetNumber(1, 50) + 30;
		this.hp = defaultHP;
		this.mp = defaultMP;

		this.str = GetNumber(2, 50) + 1;
		this.def = GetNumber(3, 50) + 1;
		this.luck = GetNumber(4, 100);
		this.agi = GetNumber(5, 40) + 20;
		this.paralyze = false;
		this.poison = false;
		this.active = true;


	}

	// ↑を修正して魔法使い用のパラメーターに直す
	// ↓を修正して通常攻撃か魔法攻撃を行うようにする
	/**
	 * {@inheritDoc}<br>
	 * 攻撃側プレイヤー(atacker)のMPが魔法の消費MPに足りている場合は
	 * @param attacker {@inheritDoc}
	 * @param defender {@inheritDoc}
	 */
	public void Attack(Player activePlayer, List<Player> passiveMembers)
	{

		Player passivePlayer = passiveMembers[UnityEngine.Random.Range(0, passiveMembers.Count-1)];
		//使用する魔法を決定する
		Magic UseMagic = this.attackMagic[UnityEngine.Random.Range(0, attackMagic.Length)];


		if (UseMagic.GetUseMP() <= this.mp)
		{

			UseMagic.effect(activePlayer, passivePlayer);
			return;

		}
		// 与えるダメージを求める
		Console.WriteLine(activePlayer.GetName() + "　の　攻撃！");
		int damage = activePlayer.CalcDamage(passivePlayer);

		// 求めたダメージを対象プレイヤーに与える
		Console.WriteLine(passivePlayer.GetName() + "　に　" + damage + "　のダメージ！");
		passivePlayer.Damage(damage);

		passivePlayer.Down();

	}
}
