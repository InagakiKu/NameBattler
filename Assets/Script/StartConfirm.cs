using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartConfirm : MonoBehaviour
{
    private Toggle[] toggles;
    [SerializeField] Canvas canvasDialog; // ダイアログのキャンバス
    private Text[] texts;
    [HideInInspector] public static string[] SelectCharacters { set;  get; }

    // Start is called before the first frame update
    void Start()
    {
        canvasDialog.enabled = false;
    }

    public void ButtonClick()
    {
        int toggleCount = 0;

        switch (transform.name)
        {
            case "StartButton":
                Debug.Log("「このパーティーで開始する」を押した");
                // ONになっているToggleの数を調べる

                foreach (Toggle toggle in UnityEngine.Object.FindObjectsOfType(typeof(Toggle)))
                {
                   if(toggle.isOn){toggleCount += 1;}
                }

                // 選択されている
                if (toggleCount <= 3 && toggleCount > 0)
                {

                    SelectCharacters = new string[toggleCount];
                    int selectedCharacters = 2;
                    foreach (Toggle toggle in UnityEngine.Object.FindObjectsOfType(typeof(Toggle)))
                    {
                        if (toggle.isOn) {
                            texts = toggle.GetComponentsInChildren<Text>();
                            SelectCharacters[selectedCharacters] = texts[0].text;
                            Debug.Log(SelectCharacters[selectedCharacters]);
                            selectedCharacters--;
                        }
                    }

                    Debug.Log("「バトル開始画面」に遷移する");
                    // シーン遷移
                    SceneManager.LoadScene("BattleStart");
                }
                else
                {
                    Debug.Log(toggleCount);
                    // ダイアログ表示
                    canvasDialog.enabled = true;

                }
                break;
            case "OKButton":
                canvasDialog.enabled = false;
                break;
            default:
                break;
        }
    }

}
