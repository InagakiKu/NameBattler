using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleResult : MonoBehaviour
{

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
}
