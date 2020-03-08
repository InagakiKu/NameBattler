using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Linq;

public class ChangeStrategy : MonoBehaviour
{
    // 連携するGameObject
    public ToggleGroup toggleGroup;

    public void ButtonClick()
    {
                Debug.Log("「戻る」を押した");

        // アクティブになっているToggleのラベルを取得する
        string selectedLabel = toggleGroup.ActiveToggles()
            .First().GetComponentsInChildren<Text>()
            .First(t => t.name == "Label").text;
        Debug.Log(selectedLabel);
        SceneManager.LoadScene("BattleMain");

    }
}
