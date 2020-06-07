using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public  class BattleMain : MonoBehaviour
{
    private GameManager gm;
    public Canvas canvasDialog;
    [HideInInspector] public Strategy strategy;
    private Toggle toggle;
    public ToggleGroup toggleGroup;
    public static int battleResult;


    public  void Start()
	{
		gm = new GameManager();
		gm.Start();
        this.canvasDialog.enabled = false;
    }

	public  Strategy GetStrategy()
	{
		return gm.GetStrategy();

	}

    public int GetBattleResult()
    {
        battleResult = gm.GetBattleResult();
        return battleResult;
    }

    public void ChangePartyStrategy(Strategy strategy, string strategyname)
    {
        gm.ChangePartyStrategy(strategy, strategyname);
    }

    public void NextTurn()
    {
        gm.NextTurn();
    }


    public void OpenCanvas()
    {

        strategy = GetStrategy();


        //取得した作戦と同じToggleをOnにする
        switch (strategy)
        {
            case DefaultStrategy ds:

                foreach (Toggle toggle in UnityEngine.Object.FindObjectsOfType(typeof(Toggle)))
                {
                    if (toggle.name == "DefaultStrategy")
                    {
                        this.toggle = toggle;
                        this.toggle.isOn = true;
                    }
                }

                break;
            case HealStrategy hs:
                foreach (Toggle toggle in UnityEngine.Object.FindObjectsOfType(typeof(Toggle)))
                {
                    if (toggle.name == "HealStrategy")
                    {
                        this.toggle = toggle;
                        this.toggle.isOn = true;
                    }
                }

                break;
            case BulleyStrategy bs:
                foreach (Toggle toggle in UnityEngine.Object.FindObjectsOfType(typeof(Toggle)))
                {
                    if (toggle.name == "BulleyStrategy")
                    {
                        this.toggle = toggle;
                        this.toggle.isOn = true;
                    }
                }

                break;
            case SavingStrategy ss:
                foreach (Toggle toggle in UnityEngine.Object.FindObjectsOfType(typeof(Toggle)))
                {
                    if (toggle.name == "SavingStrategy")
                    {
                        this.toggle = toggle;
                        this.toggle.isOn = true;
                    }
                }

                break;
            case AntiWizardStrategy aws:
                foreach (Toggle toggle in UnityEngine.Object.FindObjectsOfType(typeof(Toggle)))
                {
                    if (toggle.name == "AntiWizardStrategy")
                    {
                        this.toggle = toggle;
                        this.toggle.isOn = true;
                    }
                }

                break;
            default:
                break;
        }
    }

}