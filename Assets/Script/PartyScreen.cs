using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PartyScreen : MonoBehaviour
{
    public void ButtonClick()
    {
        switch (transform.name)
        {
            case "BackButton":
                Debug.Log("「戻る」を押した");
                SceneManager.LoadScene("TopScreen");
                break;
            case "StartButton":
                Debug.Log("「このパーティーで開始」を押した");
                SceneManager.LoadScene("BattleStart");
                break;
            default:
                break;
        }

    }

}
