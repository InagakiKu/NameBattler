using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleStart : MonoBehaviour
{
    public Player[] enemyMembers;
    public Player[] partyMembers;
    public Text[] texts;

    public void Start()
    {
        // エネミーの準備
        string[] enemyName= new string[3];
        int[] enemyJob = new int[3] { 0, 0, 0 };
        enemyMembers = new Player[3];

        // プレイヤーの準備
        string[] playerName = new string[3];
        int[] playerJob = new int[3] { 0, 0, 0 };
        partyMembers = new Player[3];


        // DB名を指定して接続
        SqliteDatabase sqlDB = new SqliteDatabase("master.db");


        // 名前と職業の決定
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
                    enemyMembers[i] = new Fighter(enemyName[i]);
                    Debug.Log("戦士を作成しました");
                   
                    break;
                case 1:
                    player = new Wizard(enemyName[i]);
                    Debug.Log("魔法使いを作成しました");
                    enemyMembers[i] = player;
                    break;
                case 2:
                    player = new Priest(enemyName[i]);
                    Debug.Log("僧侶を作成しました");
                    enemyMembers[i] = player;
                    break;
                case 3:
                    player = new Hero(enemyName[i]);
                    Debug.Log("勇者を作成しました");
                    enemyMembers[i] = player;
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

            texts[0].text = enemyMembers[i].GetName();
            
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

            texts[2].text = string.Format("HP: {0} MP: {1} STR: {2} DEF: {3} AGI: {4}", enemyMembers[i].GetHP(), enemyMembers[i].GetMP(), enemyMembers[i].GetSTR(), enemyMembers[i].GetDEF(), enemyMembers[i].GetAGI());
        }

        // DB名を指定して接続
        sqlDB = new SqliteDatabase("namebattler.db");

        // 名前と職業の決定
        for (int i = 0; i < 3; i++)
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

        for (int i = 0; i < 3; i++)
        {
            Player player = null;

            switch (playerJob[i])
            {
                case 0:
                    partyMembers[i] = new Fighter(playerName[i]);
                    Debug.Log("戦士を作成しました");

                    break;
                case 1:
                    player = new Wizard(playerName[i]);
                    Debug.Log("魔法使いを作成しました");
                    partyMembers[i] = player;
                    break;
                case 2:
                    player = new Priest(playerName[i]);
                    Debug.Log("僧侶を作成しました");
                    partyMembers[i] = player;
                    break;
                case 3:
                    player = new Hero(playerName[i]);
                    Debug.Log("勇者を作成しました");
                    partyMembers[i] = player;
                    break;
                default:
                    Debug.Log("デフォルト通過");
                    break;
            }
        }

        // パーティー情報の表示
        for (int i = 0; i < 3; i++)
        {
            string objectName = string.Format("Party{0}", i + 1);
            Debug.Log("検索するオブジェクト名" + objectName);
            var node = GameObject.Find(objectName);
            texts = node.GetComponentsInChildren<Text>();

            texts[0].text = partyMembers[i].GetName();

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

            texts[2].text = string.Format("HP: {0} MP: {1} STR: {2} DEF: {3} AGI: {4}", partyMembers[i].GetHP(), partyMembers[i].GetMP(), partyMembers[i].GetSTR(), partyMembers[i].GetDEF(), partyMembers[i].GetAGI());
        }
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
