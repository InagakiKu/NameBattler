using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TopScreen : MonoBehaviour
{
    public void ButtonClick()
    {
        switch (transform.name)
        {
            case "ListButton":
                Debug.Log("「キャラ一覧」を押した");
                break;
            case "StartButton":
                Debug.Log("「バトル開始」を押した");
                SceneManager.LoadScene("PartyScreen");
                break;
            default:
                break;
        }

    }
}
