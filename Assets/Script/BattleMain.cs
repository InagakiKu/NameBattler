using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleMain : MonoBehaviour
{
    public void ButtonClick()
    {
        switch (transform.name)
        {
            case "ChangeButton":
                Debug.Log("「変更」を押した");
                SceneManager.LoadScene("ChangeStrategy");
                break;
            case "NextButton":
                Debug.Log("「次のターン」を押した");
                break;
            default:
                break;
        }
    }
}
