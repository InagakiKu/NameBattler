using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleMain : MonoBehaviour
{
	[HideInInspector] public Party enemyMembers;
	[HideInInspector] public Party partyMembers;
	[HideInInspector] public LogText logtext= new LogText();
	[SerializeField] private Button nextTurn;
	public Text text;

	public void Start()
    {
        // プレイヤーの準備
        enemyMembers = BattleStart.enemyMembers;
        partyMembers = BattleStart.partyMembers;

        // 作戦の決定
        enemyMembers.ChangeStrategy(new DefaultStrategy());
        partyMembers.ChangeStrategy(new DefaultStrategy());

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
		for (int i = 0; i < 3; i++)
		{
			string objectName = string.Format("Party{0}", i + 1);
			Debug.Log("検索するオブジェクト名" + objectName);
			var enemystatus = GameObject.Find(objectName);
			text = enemystatus.GetComponentInChildren<Text>();

			text.text = string.Format("{0}\nHP {1}/{2} \nMP {3}/{4} \n", partyMembers.GetPlayer(i).GetName(), partyMembers.GetPlayer(i).GetHP(), partyMembers.GetPlayer(i).GetDefaultHP(), partyMembers.GetPlayer(i).GetMP(), partyMembers.GetPlayer(i).GetDefaultMP());
		}

		// エネミーステータスをTextに表示

		// ==================================================
		// バトル処理
		// ==================================================
		// ステータス表示
		StatusPrint();

		// バトル開始の表示
		logtext.AddText("=== バトル開始 ===\n");

		int turnNumber = 1;
	// 最大でも20ターンまで
	lose: while (true)
		{
			logtext.AddText(string.Format("- ターン{0} -\n\n", turnNumber));

			// 行動できる人間がiいる間繰り返す
			while (enemyMembers.existsActivePlayer() && partyMembers.existsActivePlayer())
			{
				// 未行動のプレイヤーで一番AGIが高いプレイヤーが攻撃する
				Player attacker = ComparateAGI();
				// 攻撃するプレイヤーの攻撃できる対象を決める
				Party TargetParty = ContainsParty(attacker);

				// paralyzeTurn 0 の時
				if (attacker.GetParalyzeTurn() == 0 && attacker.isParalyze())
				{
					logtext.AddText(attacker.GetName() + " の麻痺がとれた！\n");
					attacker.RecoveryParalyze();
				}


				logtext.AddText("▼ " + attacker.GetName() + " の行動\n");

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
					logtext.AddText(attacker.GetName() + " は身体が麻痺して動けない！\n");

				}

				if (TargetParty.isLose()) goto EXITLOOP;
				

				if (attacker.isPoison())
				{
					attacker.ProcessPoison();
				}
				// 全滅してるかどうか
				//　してたらゲーム終了
				if (TargetParty.isLose()) goto EXITLOOP;

				attacker.ChangeActive(false);
				attacker.ChangeParalyzeTurn();
				logtext.AddText("--------------------------------\n");
			}

			logtext.AddText("--------------------------------\n");
			StatusPrint();
			logtext.AddText("--------------------------------\n");




			// ターン終了時の処理
			StartCoroutine(WaitButtonClick(nextTurn));
			foreach (Player p in enemyMembers.AttackTarget())
			{
				p.ChangeActive(true);
			}

			foreach (Player p in partyMembers.AttackTarget())
			{
				p.ChangeActive(true);
			}
			turnNumber++;

		}
		
		EXITLOOP: ;
		logtext.AddText("\n");

		// 結果表示
		if (enemyMembers.isLose())
		{
			logtext.AddText("パーティー１にはもう戦える者がいない！\n");
			logtext.AddText("\n");
			logtext.AddText("================================\n");
			logtext.AddText("　　　パーティー２の勝利！　　　\n");
			logtext.AddText("================================\n");
		}
		else
		{
			logtext.AddText("パーティー２にはもう戦える者がいない！\n");
			logtext.AddText("\n");
			logtext.AddText("================================\n");
			logtext.AddText("　　　パーティー１の勝利！　　　\n");
			logtext.AddText("================================\n");

		}
		/*
				logtext.AddText("================================");
				StatusPrint();
		*/
	}

	IEnumerator WaitButtonClick(Button button)
    {
		yield return new WaitUntil(() => true);	
		Debug.LogFormat("{0} がクリックされました", button.name);
	}

	/**
	 * パーティー内で一番早いプレイヤー同士を比較してより早いほうを返す
	*/
	public Player ComparateAGI()
	{
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
		logtext.AddText("== パーティー1のステータス ==\n");
		// パーティー1のステータスの表示
		for (int i = 0; i < enemyMembers.GetMembers().Count; i++)
		{
			//			if (enemyMembers.GetMembers().get(i).GetHP() > 0) {
			enemyMembers.GetMembers()[i].PrintStatus();
			//			}
		}
		logtext.AddText("\n");
		logtext.AddText("== パーティー2のステータス ==\n");
		// パーティー2のステータスの表示
		for (int i = 0; i < partyMembers.GetMembers().Count; i++)
		{
			//			if (enemyMembers.GetMembers().get(i).GetHP() > 0) {
			partyMembers.GetMembers()[i].PrintStatus();
			//			}
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
            case "NextButton":
                Debug.Log("「次のターン」を押した");
                break;
            default:
                break;
        }
    }
}
