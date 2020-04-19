using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogText : MonoBehaviour
{
    //　文字列を格納する変数
    public string log = "";

    // 文字列を追加する
    public void AddText(string text)
    {
        this.log = string.Concat(this.log, text);
        Debug.Log(this.log);
    }


    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Text>().text = log;
    }
}
