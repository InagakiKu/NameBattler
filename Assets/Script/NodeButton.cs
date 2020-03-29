using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NodeButton : MonoBehaviour
{
    public string characterName;
    //ノードのボタンクリック時に起動する
    public void ButtonClick()
    {
        CharacterList.CharacterName = characterName;
        Debug.Log("「キャラクター詳細」に遷移");
        SceneManager.LoadScene("CharacterDetails");
    }
}
