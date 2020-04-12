using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PartyScreen : MonoBehaviour
{
    [HideInInspector] public static Toggle[] toggle  { set; get; }

    /// <summary>
    /// ボタンクリック時の処理
    /// </summary>
    public void ButtonClick()
    {
        int toggleCount = 0;    
        switch (transform.name)
        {
            case "BackButton":
                Debug.Log("「戻る」を押した");
                SceneManager.LoadScene("TopScreen");
                break;

            default:
                break;
        }

    }
    
}
