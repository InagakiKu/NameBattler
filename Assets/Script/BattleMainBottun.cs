using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class BattleMainBottun : MonoBehaviour
{
    public ToggleGroup toggleGroup;
    public Canvas canvasDialog;
    GameObject BMObject;
    BattleMain script;

    void Start()
    {
        BMObject = GameObject.Find("BattleMain");
        script = BMObject.GetComponent<BattleMain>();
    }

    public void ButtonClick()
    {
        switch (transform.name)
        {
            case "BackBottun":
                // 選択された職業の取得
                string selectedLabel = toggleGroup.ActiveToggles().First().GetComponentsInChildren<Text>()
                    .First(t => t.name == "Label").text;
                Debug.Log(selectedLabel + "が選択された");
                switch (selectedLabel)
                {
                    case "DefaultStrategy":

                        script.ChangePartyStrategy(new DefaultStrategy(), selectedLabel);


                        break;
                    case "HealStrategy":
                        script.ChangePartyStrategy(new HealStrategy(), selectedLabel);


                        break;
                    case "BulleyStrategy":
                        script.ChangePartyStrategy(new BulleyStrategy(), selectedLabel);


                        break;
                    case "SavingStrategy":
                        script.ChangePartyStrategy(new SavingStrategy(), selectedLabel);


                        break;
                    case "AntiWizardStrategy":
                        script.ChangePartyStrategy(new AntiWizardStrategy(), selectedLabel);


                        break;
                    default:
                        break;
                }
                Canvas canvasDialog = GameObject.Find("ChangeStrategy").GetComponent<Canvas>();
                canvasDialog.enabled = false;
                break;
            case "ChangeButton":
                Debug.Log("「変更」を押した");

                script.OpenCanvas();
                this.canvasDialog.enabled = true;
                break;
            case "NextTurn":
                Debug.Log("「次のターン」を押した");

                //戦闘が終了しているか確認
                if (script.GetBattleResult() != 0)
                {
                    SceneManager.LoadScene("ResultScreen");
                    return;
                }
                script.NextTurn();


                break;

            default:
                break;
        }
    }


}
