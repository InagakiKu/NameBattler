using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterList : MonoBehaviour
{
    [HideInInspector]public static string CharacterName { set; get; }

    /// <summary>
    /// ボタンクリック時の処理
    /// </summary>
    public void ButtonClick()
    {
        switch (transform.name)
        {
            case "CreateButton":
                Debug.Log("「新しく作成する」を押した");
                SceneManager.LoadScene("CharacterCreate");
                break;
            case "BackButton":
                Debug.Log("「戻る」を押した");
                SceneManager.LoadScene("TopScreen");
                break;
            default:
                break;
        }

    }
}
