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
        //시간이 9시 미만 즉 넘어가야할 당일 이라면 당일 9시까지 수면하기때문에
        //9시에서 현재시간을 뺀 시간에 기반하여 스트레스를 줄임
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
            Debug.Log(gettime + "시간만큼 스트레스가 줌");

        }
        //시간이 9시 이상이라면 다음날로 넘어가야하기때문에 다음날 9시 33시간 - 현재시간을 하여 남은시간을 계산하여 스트레스를 줄임
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
            Debug.Log(gettime + "시간만큼 스트레스가 줌");

        }
        DataManager.instance.SaveData();
        MainGameSetting.instance.GameSetting();
    }
}
