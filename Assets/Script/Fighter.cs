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
	public Fighter(string name) :base(name)
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
		this.defaultMP = GetNumber(1, 100) * 0;
		this.hp = defaultHP;
		this.mp = defaultMP;

		this.str = GetNumber(2, 100) + 30;
		this.def = GetNumber(3, 100) + 30;
		this.luck = GetNumber(4, 100) + 1;
		this.agi = GetNumber(5, 50) + 1;

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
	public void Attack(Player attacker, List<Player> passiveParty)
	{

		Player passivePlayer = passiveParty[UnityEngine.Random.Range(0, passiveParty.Count-1)];
		// 与えるダメージを求める
		Console.WriteLine(attacker.GetName() + "の攻撃！");
		int damage = attacker.CalcDamage(passivePlayer);

		// 求めたダメージを対象プレイヤーに与える
		Console.WriteLine(passivePlayer.GetName() + "に" + damage + "のダメージ！");
		passivePlayer.Damage(damage);

		passivePlayer.Down();
	}
}
