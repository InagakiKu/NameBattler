using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	[HideInInspector] public Party enemyMembers;
	[HideInInspector] public Party partyMembers;
	private Button nextTurn;
	public Text text;
	public Text logtext;
	public Text strategytext;
	[HideInInspector] public string log;
	[HideInInspector] public int battleResult;
	private int turnNumber;
	private int phaseNumber;
	List<Player> aliveMembers;


	public void Start()
	{
		Debug.Log("GM Start");
		this.battleResult = 0;
		turnNumber = 0;
		// プレイヤーの準備
		enemyMembers = BattleStart.enemyMembers;
		partyMembers = BattleStart.partyMembers;

		aliveMembers = enemyMembers.GetMembers();
		aliveMembers.AddRange(partyMembers.GetMembers());

		enemyMembers.ChangeStrategy(new DefaultStrategy());
		partyMembers.ChangeStrategy(new DefaultStrategy());

		var content = GameObject.Find("StrategyName");
		this.strategytext = content.GetComponent<Text>();
		this.strategytext.text = "作戦　：　バランスよく";

		// 作戦の決定

		StatusReflection();

		string objectName = "Content";
		Debug.Log("検索するオブジェクト名" + objectName);
		content = GameObject.Find(objectName);
		logtext = content.GetComponentInChildren<Text>();
		// バトル開始の表示
		if (turnNumber == 0)
		{
			LogText.AddLog("=== バトル開始 ===");
		}
		turnNumber = 1;

		StatusPrint();
		logtext.text = LogText.GetLog();
	}



	/**
	 * パーティー内で一番早いプレイヤー同士を比較してより早いほうを返す
	*/
	public Player ComparateAGI(int index)
	{
		Debug.Log("GM ComparateAGI");
		List <Player> firstestEnemies = this.enemyMembers.FastestMembers();
		List<Player> firstestPlayers = this.partyMembers.FastestMembers();

		//firstestEnemiesが空だった
		if (!(firstestEnemies?.Count > 0))
		{
			return partyMembers.FastestMembers()[0];
		}

		if (!(firstestPlayers?.Count > 0))
		{
			return enemyMembers.FastestMembers()[0];
		}

		if (this.enemyMembers.FastestMembers()[0].GetAGI() > this.partyMembers.FastestMembers()[0].GetAGI())
		{
			Debug.Log(string.Format("早いのは{0}", this.enemyMembers.FastestMembers()[0].GetName()));
			return this.enemyMembers.FastestMembers()[0];
		}

		Debug.Log(this.partyMembers.FastestMembers()[0].GetName());
		return partyMembers.FastestMembers()[0];

	}

	public Party ContainsParty(Player attacker)
	{
		Debug.Log("GameManager ContainParty");
		if (enemyMembers.isExists(attacker))
		{
			Debug.Log("GameManager ContainParty partyMembers");

			foreach (Player p in partyMembers.GetMembers())
			{
				Debug.Log(p.GetName());

			}
			return partyMembers;
		}
		Debug.Log("GameManager ContainParty enemyMembers");
		foreach (Player p in enemyMembers.GetMembers())
		{
			Debug.Log(p.GetName());

		}
		return enemyMembers;
	}

	/**
	 * プレイヤー全員のステータスをパーティーごとに表示する
	 */
	public void StatusPrint()
	{
		Debug.Log("GM StatusPrint");
		LogText.AddLog("== 敵パーティーのステータス ==");
		// パーティー1のステータスの表示
		for (int i = 0; i < 3; i++)
		{
			//			if (enemyMembers.GetMembers().get(i).GetHP() > 0) {
			LogText.AddLog(enemyMembers.GetMembers()[i].PrintStatus());
			//			}
		}

		LogText.AddLog("== 自パーティーのステータス ==");
		// パーティー2のステータスの表示
		for (int i = 0; i < partyMembers.GetMembers().Count; i++)
		{
			//			if (enemyMembers.GetMembers().get(i).GetHP() > 0) {
			LogText.AddLog(partyMembers.GetMembers()[i].PrintStatus());
			//			}
		}
	}

	private void StatusReflection()
	{
		Debug.Log("GM StatusPrint");
		// エネミーパーティーステータスをTextに表示
		for (int i = 0; i < 3; i++)
		{
			string objectName = string.Format("Enemy{0}", i + 1);
			Debug.Log("検索するオブジェクト名" + objectName);
			var enemystatus = GameObject.Find(objectName);
			text = enemystatus.GetComponentInChildren<Text>();

			text.text = string.Format("{0}\nHP {1}/{2} \nMP {3}/{4} \n", enemyMembers.GetPlayer(i).GetName(), enemyMembers.GetPlayer(i).GetHP(), enemyMembers.GetPlayer(i).GetDefaultHP(), enemyMembers.GetPlayer(i).GetMP(), enemyMembers.GetPlayer(i).GetDefaultMP());
		}
		// プレイヤーパーティーステータスをTextに表示
		for (int i = 0; i < partyMembers.GetMembers().Count; i++)
		{
			string objectName = string.Format("Party{0}", i + 1);
			Debug.Log("検索するオブジェクト名" + objectName);
			var enemystatus = GameObject.Find(objectName);
			text = enemystatus.GetComponentInChildren<Text>();

			text.text = string.Format("{0}\nHP {1}/{2} \nMP {3}/{4} \n", partyMembers.GetPlayer(i).GetName(), partyMembers.GetPlayer(i).GetHP(), partyMembers.GetPlayer(i).GetDefaultHP(), partyMembers.GetPlayer(i).GetMP(), partyMembers.GetPlayer(i).GetDefaultMP());

		}

	}

	public void NextTurn()
	{
		Debug.Log("GameManager NextTurn ");

		// ==================================================
		// バトル処理
		// ==================================================

		LogText.AddLog(string.Format("- ターン{0} -{1}", turnNumber, System.Environment.NewLine));
		this.phaseNumber = 0;

		/*----------
		 * aliveMembers =
		 * 
		 ----------*/

		// 行動できる人間がいる間繰り返す
		while (0 < partyMembers.FastestMembers().Count + enemyMembers.FastestMembers().Count)
		{
			Debug.Log(string.Format("{0}={1}({2} + {3})", this.phaseNumber, partyMembers.FastestMembers().Count + enemyMembers.FastestMembers().Count, partyMembers.FastestMembers().Count, enemyMembers.FastestMembers().Count));

			// 未行動のプレイヤーで一番AGIが高いプレイヤーが攻撃する
			Player attacker = ComparateAGI(phaseNumber);
			
			// 攻撃するプレイヤーの攻撃できる対象を決める
			Party TargetParty = ContainsParty(attacker);

			Debug.Log(attacker.GetName());

			// paralyzeTurn 0 の時
			if (attacker.GetParalyzeTurn() == 0 && attacker.isParalyze())
			{
				Debug.Log("GameManager NextTurn 麻痺がとける時");
				LogText.AddLog(string.Format("{0} の麻痺がとれた！\n", attacker.GetName()));
				attacker.RecoveryParalyze();
			}

			LogText.AddLog(string.Format("▼ {0} の行動\n", attacker.GetName()));

			// 麻痺状態でない場合の処理
			if (!attacker.isParalyze())
			{
				Debug.Log("GameManager NextTurn 麻痺になってないとき");

				//どちらのパーティーに所属しているか
				if (enemyMembers.isExists(attacker))
				{
					Debug.Log(string.Format("{0} は敵パーティーに所属している", attacker.GetName()));
					attacker.Action(attacker, partyMembers);
				}
				else
				{
					Debug.Log(string.Format("{0} は味方パーティーに所属している", attacker.GetName()));
					attacker.Action(attacker, enemyMembers);
				}
			}
			else
			{
				Debug.Log("GameManager NextTurn 麻痺になっているとき");
				LogText.AddLog(string.Format("{0} は身体が麻痺して動けない！\n", attacker.GetName()));
			}

			if (TargetParty.isLose())
			{
				if (TargetParty == enemyMembers)
				{
					this.battleResult = 1;
				}
				else
				{
					this.battleResult = 2;
				}
				return;
			}

			if (attacker.isPoison())
			{
				Debug.Log("GameManager NextTurn 毒状態の時");
				attacker.ProcessPoison();
			}

			// 全滅してるかどうか
			//　してたらゲーム終了
			if (TargetParty.isLose())
			{
				if (TargetParty == enemyMembers)
                {
					this.battleResult = 0;

				}
                else
                {
					this.battleResult = 1;
				}

			}


			attacker.ChangeActive(false);
			Debug.Log(string.Format("GameManager NextTurn ChangeActive : {0}", attacker.isActive()));
			attacker.ChangeParalyzeTurn();
			Debug.Log("GameManager NextTurn ChangeParalyzeTurn");
			LogText.AddLog("--------------------------------");
			this.phaseNumber++;

		}

		LogText.AddLog("=================================");

		// ターン終了時の処理
		foreach (Player p in enemyMembers.AttackTarget())
		{
			p.ChangeActive(true);
		}

		foreach (Player p in partyMembers.AttackTarget())
		{
			p.ChangeActive(true);
		}

		StatusReflection();

		logtext.text = LogText.GetLog();
		turnNumber++;
	}

	public Strategy GetStrategy()
	{
		return partyMembers.GetStrategy();

	}



	public void ChangePartyStrategy(Strategy strategy, string strategyname)
    {
		Debug.Log("GM ChangePartyStrategy");
		partyMembers.ChangeStrategy(strategy);


		Debug.Log("検索するオブジェクト名 StrategyName");
		var content = GameObject.Find("StrategyName");
		this.strategytext = content.GetComponent<Text>();
		this.strategytext.text = string.Format("作戦　：　{0}", strategyname);
    }

	public int GetBattleResult() 
	{
		return this.battleResult;
			
	}

}
