using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NodeButton : MonoBehaviour
{
    public Text[] texts;

    /// <summary>
    /// ボタンクリック時の処理
    /// </summary>
    public void ButtonClick()
    {
        GameObject parentObject = transform.parent.gameObject;
        texts = parentObject.GetComponentsInChildren<Text>();
        CharacterList.CharacterName = texts[0].text;

        Debug.Log("「キャラクター詳細」に遷移");
        SceneManager.LoadScene("CharacterDetails");
    }
}
