using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleStart : MonoBehaviour
{
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
