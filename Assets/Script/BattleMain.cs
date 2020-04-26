using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BattleMain : MonoBehaviour
{
	[HideInInspector] public Party enemyMembers;
	[HideInInspector] public Party partyMembers;
	private Button nextTurn;
	public Text text;
	public Text logtext;
	private string log;
	private int turnNumber;

	public void Start()
	{
		// プレイヤーの準備
		enemyMembers = BattleStart.enemyMembers;
		partyMembers = BattleStart.partyMembers;

		// 作戦の決定
		enemyMembers.ChangeStrategy(new DefaultStrategy());
		partyMembers.ChangeStrategy(new DefaultStrategy());


		StatusReflection();

		string objectName = "Content";
		Debug.Log("検索するオブジェクト名" + objectName);
		var content = GameObject.Find(objectName);
		logtext = content.GetComponentInChildren<Text>();

		// バトル開始の表示
		log = "=== バトル開始 ===\n";
		logtext.text = log;

		turnNumber = 1;
		// 最大でも20ターンまで

		StatusPrint();

	}



	/**
	 * パーティー内で一番早いプレイヤー同士を比較してより早いほうを返す
	*/
	public Player ComparateAGI()
	{
		Debug.Log(string.Format("enemy{0}:{1}、Party{2}:{3}", this.enemyMembers.FastestPlayer().GetName(), this.enemyMembers.FastestPlayer().GetAGI(), this.partyMembers.FastestPlayer().GetName(), this.partyMembers.FastestPlayer().GetAGI()));
		if (this.enemyMembers.FastestPlayer().GetAGI() > this.partyMembers.FastestPlayer().GetAGI())
		{
			return this.enemyMembers.FastestPlayer();
		}
		return partyMembers.FastestPlayer();

	}

	public Party ContainsParty(Player attacker)
	{

		if (enemyMembers.isExists(attacker))
		{
			return partyMembers;
		}

		return enemyMembers;
	}

	/**
	 * プレイヤー全員のステータスをパーティーごとに表示する
	 */
	public void StatusPrint()
	{
		log = log + "== パーティー1のステータス ==\n";
		// パーティー1のステータスの表示
		for (int i = 0; i < 3; i++)
		{
			//			if (enemyMembers.GetMembers().get(i).GetHP() > 0) {
			log = log + enemyMembers.GetMembers()[i].PrintStatus();
			//			}
		}
		log = log + "\n";
		log = log + "== パーティー2のステータス ==\n";
		// パーティー2のステータスの表示
		for (int i = 0; i < partyMembers.GetMembers().Count; i++)
		{
			//			if (enemyMembers.GetMembers().get(i).GetHP() > 0) {
			log = log + partyMembers.GetMembers()[i].PrintStatus();
			//			}
			logtext.text = log;
		}
	}
	public void ButtonClick()
    {
        switch (transform.name)
        {
            case "ChangeButton":
                Debug.Log("「変更」を押した");
                SceneManager.LoadScene("ChangeStrategy");
                break;
            case "NextTurn":
                Debug.Log("「次のターン」を押した");
				NextTurn();
                break;
            default:
                break;
        }
    }

	private  void StatusReflection()
    {
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

	private void NextTurn()
	{


		// ==================================================
		// バトル処理
		// ==================================================

		log = log + string.Format("- ターン{0} -\n\n", turnNumber);
		int reiterationCount = 0;
		// 行動できる人間がiいる間繰り返す
		while (enemyMembers.existsActivePlayer() && partyMembers.existsActivePlayer() && reiterationCount <6)
		{
			// 未行動のプレイヤーで一番AGIが高いプレイヤーが攻撃する
			Player attacker = ComparateAGI();
			// 攻撃するプレイヤーの攻撃できる対象を決める
			Party TargetParty = ContainsParty(attacker);
				// paralyzeTurn 0 の時
			if (attacker.GetParalyzeTurn() == 0 && attacker.isParalyze())
			{
				log = log + string.Format("{0} の麻痺がとれた！\n", attacker.GetName());
				attacker.RecoveryParalyze();
			}

			log = log + string.Format("▼ {0} の行動\n", attacker.GetName());
				// 麻痺状態でない場合の処理
			if (!attacker.isParalyze())
			{
				//どちらのパーティーに所属しているか
				if (attacker.GetMyParty()==enemyMembers)
				{
					attacker.Action(attacker, partyMembers);
				}
				else
				{
					attacker.Action(attacker, enemyMembers);
				}
			}
			else
			{
				log  = log + string.Format ("{0} は身体が麻痺して動けない！\n", attacker.GetName());
			}
		
			if (TargetParty.isLose())
            {
				SceneManager.LoadScene("ResultScreen");
			}
			
			if (attacker.isPoison())
			{
				attacker.ProcessPoison();
			}
			
			// 全滅してるかどうか
			//　してたらゲーム終了
			if (TargetParty.isLose())
			{
				SceneManager.LoadScene("ResultScreen");
			}


			attacker.ChangeActive(false);
			attacker.ChangeParalyzeTurn();
			log = log + "--------------------------------\n";
			reiterationCount++;
		}
		

		log = log + "--------------------------------\n";
		log = log + "--------------------------------\n";

		StatusPrint();

		// ターン終了時の処理
		foreach (Player p in enemyMembers.AttackTarget())
		{
			p.ChangeActive(true);
		}
		
		foreach (Player p in partyMembers.AttackTarget())
		{
			p.ChangeActive(true);
		}

		logtext.text = log;
		turnNumber++;
	}

}

