using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SettingName : MonoBehaviour
{

    public  TMP_InputField setname;

    //������ �����Ұ�� �ʱ� ���� �����͸� �����Ѵ�.
    //�Ŀ� if���� ���� Īȣ�� ������������� ������ �ְ� �����ϰԲ� ��
    public void Finishname()
    {
        DataManager.instance.nowData.name = setname.text;
        DataManager.instance.nowData.money = 100000;
        DataManager.instance.nowData.flex = 5;
        DataManager.instance.nowData.conf = 5;
        DataManager.instance.nowData.social = 5;
        DataManager.instance.nowData.hp = 5;
        DataManager.instance.nowData.intel = 5;
        DataManager.instance.nowData.day = 1;
        DataManager.instance.nowData.hour = 9;
        DataManager.instance.nowData.minute = 00;
        DataManager.instance.nowData.stress = 1;
        DataManager.instance.SaveData();

        SceneManager.LoadScene("MainGame");
    }
}
