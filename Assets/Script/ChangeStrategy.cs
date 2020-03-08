using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeStrategy : MonoBehaviour
{
    public void ButtonClick()
    {
                Debug.Log("「戻る」を押した");
                SceneManager.LoadScene("BattleMain");

    }
}
