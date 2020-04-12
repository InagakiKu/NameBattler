using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollController : MonoBehaviour
{
    public RectTransform prefab = null;
    public Text[] texts;
    public NodeButton button;
    public static int oid = 0; 

        // Start is called before the first frame update
    void Start()
    {
        // スクロールビューに表示するノードの数を調べる
        int count = 0; // 表示する行数

        // DB名を指定して接続
        SqliteDatabase sqlDB = new SqliteDatabase("namebattler.db");
        // SQL文の作成
        string query = "select count(*) as count from characters";
        // SQL文実行
        DataTable dataTable = sqlDB.ExecuteQuery(query);

        // 行数を求める
        foreach (DataRow dr in dataTable.Rows)
        {
            count = (int)dr["count"];
        }

        // 行数分のノードを作成する
        for(int i=0;i<count; i++)
        {
            // ノードの自動作成
            var character = GameObject.Instantiate(prefab) as RectTransform;
            character.SetParent(transform, false);

            // ノード内に表示するテキストを検索する
            query = string.Format("select * from characters limit {0} offset {1}", 1,i);
            dataTable = sqlDB.ExecuteQuery(query);

            string name = "";
            int job = 0;
            int hp = 0;
            int mp = 0;
            int str = 0;
            int def = 0;
            int agi = 0;
            int luck = 0;
            string create_at = null;

            foreach (DataRow dr in dataTable.Rows)
            {
                name = (string)dr["name"];
                job = (int)dr["job"];
                hp = (int)dr["hp"];
                mp = (int)dr["mp"];
                str = (int)dr["str"];
                def = (int)dr["def"];
                agi = (int)dr["agi"];
                luck = (int)dr["luck"];
                create_at = (string)dr["create_at"];

            }


            texts = character.GetComponentsInChildren<Text>();
            texts[0].text = name;

            if (job == 0)
            {
                texts[1].text = "戦士";

            }
            else if (job == 1)
            {
                texts[1].text = "魔法使い";
            }
            else if (job == 2)
            {
                texts[1].text = "僧侶";
            }
            else
            {
                texts[1].text = "勇者";
            }

            texts[2].text = string.Format("HP: {0} MP: {1} STR: {2} DEF: {3} AGI: {4}", hp, mp, str, def, agi);
        }
    }


}