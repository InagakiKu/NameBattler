using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogText
{
    //　文字列を格納する変数
    private static List<string> logMsg = new List<string>();
    // 文字列を追加する
    public static void AddLog(string text)
    {
        logMsg.Add(text);
    }

    public static string GetLog()
    {
        string outMessage = "";

        foreach (string msg in logMsg)
        {
            outMessage += msg + System.Environment.NewLine;
        }

        return outMessage;
    }
}
