using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SettingName : MonoBehaviour
{

    public  TMP_InputField setname;

    public void Finishname()
    {
        DataManager.instance.nowData.name = setname.text;
        DataManager.instance.nowData.money = 100000 + "¿ø";
        DataManager.instance.SaveData();

        SceneManager.LoadScene("MainGame");
    }
}
