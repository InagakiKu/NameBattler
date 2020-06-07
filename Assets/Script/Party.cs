using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Party : MonoBehaviour
{
	// =======================
	// フィールド変数
	// =======================
	// パーティーの情報が入ったリスト
	private List<Player> members;
	private Strategy myStrategy;

	// =======================
	// コンストラクタ
	// =======================
	public Party()
	{
		this.members = new List<Player>();
	}

	// =======================
	// Getter / Setter
	// =======================
	/**
	 * パーティーメンバーを List<Player> で取得する
	 */
	public List<Player> GetMembers()
	{

		return this.members;

	}

	/// <summary>
	/// 指定したインデックスのプレイヤーを返す
	/// </summary>
	/// <param name="index">int</param>
	/// <returns></returns>
	public Player GetPlayer(int index)
	{
		return members[index];
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
	 * パーティーにプレイヤーを追加する
	 *
	 * @param player
	 *            : 追加するプレイヤー
	 */
	public void AppendPlayer(Player player)
	{

		this.members.Add(player);

	}

	/**
	 * パーティーからプレイヤーを離脱させる
	 *
	 * @param player
	 *            : 離脱させるプレイヤー
	 */
	public void RemovePlayer(Player player)
	{

		this.members.Remove(player);
	}

	/**
	 * パーティー内のHPのあるプレイヤーをList<Player>で返す
	 *
	 * @return　：攻撃可能なプレイヤーのList
	 */
	public List<Player> AttackTarget()
	{
		var result = from x in members
					 where x.GetHP() > 0
					 select x;

		return result.Distinct().ToList();
	}

	/**
	 * パーティー内にHP回復が必要なメンバーのリストを返す
	 *
	 * @return targetList : HP回復が必要なメンバのList
	 */
	public List<Player> HealMembers(int percent)
	{
		Debug.Log("Party HealMembers");

		var result = from x in members
					 where x.GetHP() < (x.GetDefaultHP() * percent / 100)
					 select x;

		return result.Distinct().ToList();
	}

	/**
	 * 未行動のPlayerの中で一番早いPlayerを返す
	 * @return
	 */
	public List<Player> FastestMembers()
	{
		Debug.Log("Party FastestMembers");
		var result = from x in members
					 where x.GetHP() >0
					 select x;
		
		result = from x in result
				 where x.GetActive()
				 select x;

		result = from a in result
					 orderby a.GetAGI() descending
					 select a;

		return result.Distinct().ToList();
	}

	/**
	 * 未行動のPlayerの中で一番体力の低いPlayerを返す
	 * @return　体力の低い順に並んだリスト
	 */
	public List<Player> WeakMembers()
	{
		var result = from x in members
					 where x.GetActive()
					 select x;

		result = from a in result
				 orderby a.GetHP()
				 select a;

		return result.Distinct().ToList();
	}

	/**
	 * パーティーに魔法使いがいる場合、魔法使いのみのリストを返す
	 * パーティーに魔法使いがいない場合、攻撃可能なプレーヤーのリストを返す
	 * @return
	 */
	public List<Player> WizardTarget()
	{
		var result = from x in members
					 where x is Wizard
					 select x;

		result = from a in members
				 orderby a.GetHP() > 0
				 select a;

		result = result.Distinct().ToList();

		if (result.Count() < 0)
		{

			result = AttackTarget();

			return result.Distinct().ToList();

		}

		return result.Distinct().ToList();
	}



	/**
	 *
	 * @param player
	 * @return
	 */
	public bool isExists(Player player)
	{

		return members.Contains(player);
	}

	/**
	 * 行動できる人間がいるかどうか
	 */
	public bool existsActivePlayer()
	{
		var result = from x in members
					 where x.GetActive()
					 select x;
		result = result.Distinct().ToList();

		if (result.Count() >0)
		{
			Debug.Log(string.Format("行動できる人間は {0} 人",result.Count()));
			return true;
		}

		return false;

	}

	/**
	 * パーティー内のプレイヤーが全滅しているかどうか判定する
	 *
	 * @return ： パーティーが全滅しているかどうか
	 */
	public bool isLose()
	{
		var result = from x in members
					 where x.GetHP() > 0
					 select x;
		result = result.Distinct().ToList();

		if (result.Count() >= 1)
		{
			return false;
		}

		return true;
	}


	/**
	 * 作戦をパーティーメンバーに伝える
	 */
	public void ChangeStrategy(Strategy strategy)
	{
		foreach (Player p in this.members)
		{
			p.SetStrategy(strategy);
		}
		this.myStrategy = strategy;
	}

	public List<Player> DebuffMembers()
	{
		var result = from x in members
					 where !(x.isParalyze()) | !(x.isPoison())
					 select x;

		return result.Distinct().ToList();
	}

	public Strategy GetStrategy()
    {
		return this.myStrategy; 
    }


}