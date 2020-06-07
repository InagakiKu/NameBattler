using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class BattleResult : MonoBehaviour
{

    [HideInInspector] public int battleResult;
    private Text resultText;

    public void Start()
    {
        GetResult();

        var content = GameObject.Find("BattleResult");
        this.resultText = content.GetComponent<Text>();
        switch (battleResult)
        {
            case 1:
                this.resultText.text = string.Format("YOU{0}WIN", System.Environment.NewLine);
                break;
            case 2:
                this.resultText.text = string.Format("YOU{0}LOSE", System.Environment.NewLine);
                break;
            default:
                break;
        }
    }

    //ボタン押下時の処理
    public void ButtonClick()
    {
        switch (transform.name)
        {
            case "RestartButton":
                Debug.Log("「再挑戦」を押した");
                // パーティーを引き継ぐ

                // バトルメイン画面に遷移
                SceneManager.LoadScene("BattleMain");
                break;
            case "NextButton":
                Debug.Log("「次の対戦」を押した");
                SceneManager.LoadScene("BattleStart");
                break;
            case "CloseButton":
                Debug.Log("「対戦を終了する」を押した");
                SceneManager.LoadScene("TopScreen");
                break;
            default:
                break;

        }

    }

    void GetResult()
    {
        this.battleResult = BattleMain.battleResult;
    }

}
