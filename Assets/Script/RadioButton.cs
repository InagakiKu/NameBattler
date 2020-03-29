using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RadioButton : MonoBehaviour
{
    // 連携するGameObject
    public ToggleGroup toggleGroup;

    public void SetButtons()
    {
        // アクティブになっているToggleのラベルを取得する
        string selectedLabel = toggleGroup.ActiveToggles()
            .First().GetComponentsInChildren<Text>()
            .First(toggleGroup => toggleGroup.name == "Label").text;
        switch (selectedLabel) 
        {
            case "HealStarategyToggle":
                Debug.Log("作戦：" + selectedLabel + " が選択されています");
                break;
            case "DefaultStrategyToggle":
                Debug.Log("作戦：" + selectedLabel + " が選択されています");
                break;
            case "SavingStrategyToggle":
                Debug.Log("作戦：" + selectedLabel + " が選択されています");
                break;
            case "BulleyStrategyToggle":
                Debug.Log("作戦：" + selectedLabel + " が選択されています");
                break;
            case "AntiWizardStrategyToggle":
                Debug.Log("作戦：" + selectedLabel + " が選択されています");
                break;
            default:
                break;
        }

        Debug.Log("作戦：" + selectedLabel + " が選択されています");
    }

}
