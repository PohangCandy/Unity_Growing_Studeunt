using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Sleep : MonoBehaviour
{
    public GameObject sleepPanel;

    public void sleepbuttonclick()
	{
        sleepPanel.SetActive(true);
    }

    public void sleepyes()
    {
        setSleepTime();
        sleepPanel.SetActive(false);
    }
    public void sleepno()
    {
        sleepPanel.SetActive(false);
    }

    void setSleepTime()
    {
        //�ð��� 9�� �̸� �� �Ѿ���� ���� �̶�� ���� 9�ñ��� �����ϱ⶧����
        //9�ÿ��� ����ð��� �� �ð��� ����Ͽ� ��Ʈ������ ����
        int gettime;
        if (DataManager.instance.nowData.hour < 9)
        {
            gettime = 9 - DataManager.instance.nowData.hour;
            DataManager.instance.nowData.hour = 9;
            for (int i = gettime; i > 1; i--)
            {
                if (DataManager.instance.nowData.stress <= 0)
                    break;
                DataManager.instance.nowData.stress -= 1;
            }
            Debug.Log(gettime + "�ð���ŭ ��Ʈ������ ��");

        }
        //�ð��� 9�� �̻��̶�� �������� �Ѿ���ϱ⶧���� ������ 9�� 33�ð� - ����ð��� �Ͽ� �����ð��� ����Ͽ� ��Ʈ������ ����
        else
        {
            gettime = 33 - DataManager.instance.nowData.hour;
            DataManager.instance.nowData.day++;
            DataManager.instance.nowData.hour = 9;
            for (int i = gettime; i > 1; i--)
            {
                if (DataManager.instance.nowData.stress <= 0)
                    break;
                DataManager.instance.nowData.stress -= 1;
            }
            Debug.Log(gettime + "�ð���ŭ ��Ʈ������ ��");

        }
        DataManager.instance.SaveData();
        MainGameSetting.instance.GameSetting();
    }
}
