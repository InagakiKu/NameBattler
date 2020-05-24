using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BattleStart : MonoBehaviour
{
    [HideInInspector] public static Party enemyMembers;
    [HideInInspector] public static Party partyMembers;
    public Text[] texts;
    private int playerCount;

    public void Start()
    {
        playerCount = StartConfirm.toggleCount;

        // エネミーの準備
        string[] enemyName= new string[3];
        int[] enemyJob = new int[3];
        enemyMembers = new Party();

        // プレイヤーの準備
        string[] playerName = new string[playerCount];
        int[] playerJob = new int[playerCount] ;
        partyMembers = new Party();


        // DB名を指定して接続
        SqliteDatabase sqlDB = new SqliteDatabase("master.db");


        // 敵の名前と職業の決定
        for (int i = 0; i < 3; i++)
        {
            // SQL文の作成
            string query = string.Format("select name from enemyname where name_id = {0}", Random.Range(1, 80));
            Debug.Log(query);

            // SQL文実行
            DataTable dataTable = sqlDB.ExecuteQuery(query);

            // 名前を求める 
            foreach (DataRow dr in dataTable.Rows)
            {
                enemyName[i] = (string)dr["name"];
            }
            
            enemyJob[i] = Random.Range(0, 3);
            Debug.Log(string.Format("名前：{0} 職業：{1}",enemyName[i], enemyJob[i]));
        }

        for (int i = 0; i < 3; i++)
        {
            Player player = null;

            switch (enemyJob[i])
            {
                case 0:
                    enemyMembers.AppendPlayer(new Fighter(enemyName[i]));
                    Debug.Log("戦士を作成しました");
                    enemyMembers.GetPlayer(i).SetMyParty(enemyMembers);
                    break;
                case 1:
                    enemyMembers.AppendPlayer(new Wizard(enemyName[i]));
                    Debug.Log("魔法使いを作成しました ");
                    enemyMembers.GetPlayer(i).SetMyParty(enemyMembers);
                    break;
                case 2:
                    enemyMembers.AppendPlayer(new Priest(enemyName[i]));
                    Debug.Log("僧侶を作成しました");
                    enemyMembers.GetPlayer(i).SetMyParty(enemyMembers);
                    break;
                case 3:
                    enemyMembers.AppendPlayer(new Hero(enemyName[i]));
                    Debug.Log("勇者を作成しました");
                    enemyMembers.GetPlayer(i).SetMyParty(enemyMembers);
                    break;
                default:
                    Debug.Log("デフォルト通過");
                    break;
            }
        }


        // エネミー情報の表示
        for (int i = 0; i < 3; i++)
        {
            string objectName = string.Format("Enemy{0}", i+1);
            Debug.Log("検索するオブジェクト名" + objectName);
            var node = GameObject.Find(objectName);
            texts = node.GetComponentsInChildren<Text>();

            texts[0].text = enemyMembers.GetPlayer(i).GetName();
            
            if (enemyJob[i] == 0)
            {
                texts[1].text = "戦士";

            }
            else if (enemyJob[i] == 1)
            {
                texts[1].text = "魔法使い";
            }
            else if (enemyJob[i] == 2)
            {
                texts[1].text = "僧侶";
            }
            else
            {
                texts[1].text = "勇者";
            }

            texts[2].text = string.Format("HP: {0} MP: {1} STR: {2} DEF: {3} AGI: {4}", enemyMembers.GetPlayer(i).GetHP(), enemyMembers.GetPlayer(i).GetMP(), enemyMembers.GetPlayer(i).GetSTR(), enemyMembers.GetPlayer(i).GetDEF(), enemyMembers.GetPlayer(i).GetAGI());
        }

        // DB名を指定して接続
        sqlDB = new SqliteDatabase("namebattler.db");

        Debug.Log(playerCount);

        // 名前と職業の決定
        for (int i = 0; i < playerCount; i++)
        {
            // SQL文の作成
            string query = string.Format("select name,job from characters where name = '{0}'", StartConfirm.SelectCharacters[i]);
            Debug.Log(query);

            // SQL文実行
            DataTable dataTable = sqlDB.ExecuteQuery(query);

            // 名前を求める
            foreach (DataRow dr in dataTable.Rows)
            {
                playerName[i] = (string)dr["name"];
                playerJob[i] = (int)dr["job"];
            }
            Debug.Log(string.Format("作成したPlayerの名前：{0} 職業：{1}", playerName[i], playerJob[i]));
        }

        for (int i = 0; i < playerCount; i++)
        {
            Player player = null;

            switch (playerJob[i])
            {
                case 0:
                    partyMembers.AppendPlayer(new Fighter(playerName[i]));
                    Debug.Log("戦士を作成しました");
                    partyMembers.GetPlayer(i).SetMyParty(partyMembers);
                    break;
                case 1:
                    partyMembers.AppendPlayer(new Wizard(playerName[i]));
                    Debug.Log("魔法使いを作成しました");
                    partyMembers.GetPlayer(i).SetMyParty(partyMembers);
                    break;
                case 2:
                    partyMembers.AppendPlayer(new Priest(playerName[i]));
                    Debug.Log("僧侶を作成しました");
                    partyMembers.GetPlayer(i).SetMyParty(partyMembers);
                    break;
                case 3:
                    partyMembers.AppendPlayer(new Hero(playerName[i]));
                    Debug.Log("勇者を作成しました");
                    partyMembers.GetPlayer(i).SetMyParty(partyMembers);
                    break;
                default:
                    Debug.Log("デフォルト通過");
                    break;
            }
        }

        // パーティー情報の表示
        for (int i = 0; i < playerCount; i++)
        {
            string objectName = string.Format("Party{0}", i + 1);
            Debug.Log("検索するオブジェクト名" + objectName);
            var node = GameObject.Find(objectName);
            texts = node.GetComponentsInChildren<Text>();

            texts[0].text = partyMembers.GetPlayer(i).GetName();

            if (playerJob[i] == 0)
            {
                texts[1].text = "戦士";

            }
            else if (playerJob[i] == 1)
            {
                texts[1].text = "魔法使い";
            }
            else if (playerJob[i] == 2)
            {
                texts[1].text = "僧侶";
            }
            else
            {
                texts[1].text = "勇者";
            }

            texts[2].text = string.Format("HP: {0} MP: {1} STR: {2} DEF: {3} AGI: {4}", partyMembers.GetPlayer(i).GetHP(), partyMembers.GetPlayer(i).GetMP(), partyMembers.GetPlayer(i).GetSTR(), partyMembers.GetPlayer(i).GetDEF(), partyMembers.GetPlayer(i).GetAGI());
        }
        Debug.Log("表示終了");
    }

    public void ButtonClick()
    {
        switch (transform.name)
        {
            case "BackButton":
                Debug.Log("「戻る」を押した");
                SceneManager.LoadScene("PartyScreen");
                break;
            case "NextButton":
                Debug.Log("「この相手と戦う」を押した");
                SceneManager.LoadScene("BattleMain");
                break;
            case "AgainButton":
                Debug.Log("「相手を選びなおす」を押した");
                // 現在のシーン名を取得
                Scene loadScene = SceneManager.GetActiveScene();
                // シーンの再読み込み
                SceneManager.LoadScene(loadScene.name);
                break;
            default:
                break;
        }
    }
}
