using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanelConfirm : MonoBehaviour
{
    /// <summary>
    ///  ダイアログのキャンバス
    /// </summary>
    [SerializeField] Canvas canvasDialog;
    

    // Start is called before the first frame update
    void Start()
    {
        canvasDialog.enabled = false;
    }

    public void ButtonClick()
    {
        switch (transform.name)
        {
            case "DeleteButton":
                Debug.Log("「削除する」を押した");
                // 削除ダイアログの表示
                canvasDialog.enabled = true;
                break;
            case "YesButton":
                Debug.Log("「はい」を押した");
                // DELETE処理
                ProcessingSQL deleteSQL = new ProcessingSQL();
                deleteSQL.DeleteSQL(CharacterList.CharacterName);

                // 削除ダイアログの消去
                canvasDialog.enabled = false;

                SceneManager.LoadScene("CharacterList");
                break;
            case "NoButton":
                Debug.Log("「いいえ」を押した");
                // 削除ダイアログの消去
                canvasDialog.enabled = false;
                break;
            default:
                break;
        }
    }

}
