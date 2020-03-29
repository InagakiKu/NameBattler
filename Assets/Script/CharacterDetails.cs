using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterDetails : MonoBehaviour
{

    public Text textName;
    public Text textJob;
    public Text textStatus;
    private string name;
    
    // シーン開始後に呼び出される
    void Start()
    {
        Debug.Log("「キャラクター詳細画面」に遷移した");

        this.name = CharacterList.CharacterName;

        // DBからデータを取得する
        SqliteDatabase sqlDB = new SqliteDatabase("namebattler.db");
        string query = string.Format("select * from characters where name ='{0}'", this.name);
        DataTable dataTable = sqlDB.ExecuteQuery(query);

        //変数の準備
        var name = "";
        int job = 0;
        int hp = 0;
        int mp = 0;
        int str = 0;
        int def = 0;
        int agi = 0;
        int luck = 0;
        string create_at = null;

        // DBのデータを変数に格納
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

        // 名前の表示用
        textName.text = name;

        //職業の表示用
        if (job == 0)
        {
            textJob.text = "戦士";
        }
        else if (job == 1)
        {
            textJob.text = "魔法使い";
        }
        else if (job == 2)
        {
            textJob.text = "僧侶";
        }
        else
        {
            textJob.text = "勇者";
        }

        textStatus.text = string.Format("\n{0}\n{1}\n{2}\n{3}\n{4}\n{5}",hp, mp, str, def, agi, luck);
    }

    /// <summary>
    /// ボタンクリック時の処理
    /// </summary>
    public void ButtonClick()
    {
        switch (transform.name){
            case "BackButton":
                Debug.Log("「戻る」を押した");
                SceneManager.LoadScene("CharacterList");
                break;
            default:
                break;
        }

    }
}
