using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PartyNode : MonoBehaviour
{
    private Toggle toggle;
    public Text text;

    void Start()
    {
        toggle = GetComponent<Toggle>();
    }
    public void SwitchToggle()
    {
        int toggleCount = 0;

        foreach (Toggle toggle in UnityEngine.Object.FindObjectsOfType(typeof(Toggle)))
        {
            if (toggle.isOn) { toggleCount += 1; }
        }

        text = GameObject.Find("StartButton").GetComponentInChildren<Text>();
        text.text = string.Format("このパーティーで開始する({0}/3)", toggleCount);

        Debug.Log("チェックされました。" );
    }
}
