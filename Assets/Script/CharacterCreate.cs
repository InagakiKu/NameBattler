using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.UI;
using System;

public class CharacterCreate : MonoBehaviour
{
    public InputField inputField;
    public ToggleGroup toggleGroup;
    private DateTime TodayNow;

    public void ButtonClick()
    {
        switch (transform.name)
        {
            case "CreateButton":
                Debug.Log("「作成する」を押した");

                Player player = null;
                string name = "";
                int job = 0;
                int hp = 0;
                int mp = 0;
                int str = 0;
                int def = 0;
                int agi = 0;
                int luck = 0;
                string create_at = "";

                // DB名を指定して接続
                SqliteDatabase sqlDB = new SqliteDatabase("namebattler.db");

                //  入力されたテキストの取得
                this.inputField = inputField.GetComponent<InputField>();
                name = inputField.text;

                // 選択された職業の取得
                string selectedLabel = toggleGroup.ActiveToggles().First().GetComponentsInChildren<Text>()
                    .First(t => t.name == "Label").text;
                Debug.Log(selectedLabel + "が選択された");

                switch (selectedLabel) {
                    case "戦士":
                        job = 0;
                        player = new Fighter(name);
                        hp = player.GetHP();
                        mp = player.GetMP();
                        str = player.GetSTR();
                        def = player.GetDEF();
                        agi = player.GetAGI();
                        luck = player.GetLUCK();
                        Debug.Log("戦士を作成しました");
                        break;
                    case "魔法使い":
                        job = 1;
                        player = new Wizard(name);
                        hp = player.GetHP();
                        mp = player.GetMP();
                        str = player.GetSTR();
                        def = player.GetDEF();
                        agi = player.GetAGI();
                        luck = player.GetLUCK();
                        Debug.Log("魔法使いを作成しました");
                        break;
                    case "僧侶":
                        job = 2;
                        player = new Priest(name);
                        hp = player.GetHP();
                        mp = player.GetMP();
                        str = player.GetSTR();
                        def = player.GetDEF();
                        agi = player.GetAGI();
                        luck = player.GetLUCK();
                        Debug.Log("僧侶を作成しました");
                        break;
                    case "勇者":
                        job = 3;
                        player = new Hero(name);
                        hp = player.GetHP();
                        mp = player.GetMP();
                        str = player.GetSTR();
                        def = player.GetDEF();
                        agi = player.GetAGI();
                        luck = player.GetLUCK();
                        Debug.Log("勇者を作成しました");
                        break;
                    default:
                        Debug.Log("デフォルト通過");
                        break;
                }
                
                // 作成時間の取得
                TodayNow = DateTime.Now;

                create_at = string.Format("{0}年{1}月{2}日 {3}:{4}:{5}",TodayNow.Year,TodayNow.Month,TodayNow.Day,TodayNow.Hour,TodayNow.Minute, TodayNow.Second);
                Debug.Log(create_at);

                // SQL文の作成
                string query = string.Format("insert into characters values('{0}',{1},{2},{3},{4},{5},{6},{7},'{8}')", name, job, hp, mp, str, def, agi, luck, create_at);
                Debug.Log(query);
                // SQL文実行
                DataTable dataTable = sqlDB.ExecuteQuery(query);

                // 
                CharacterList.CharacterName = name;
                Debug.Log(CharacterList.CharacterName);

                Debug.Log("キャラクター作成終了");
                // キャラクター作成完了画面に遷移
                SceneManager.LoadScene("CreateResult");
                break;

            case "BackButton":
                Debug.Log("「戻る」を押した");
                SceneManager.LoadScene("CharacterList");
                break;
            default:
                break;
        }

    }
}
