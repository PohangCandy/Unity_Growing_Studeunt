using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainGameSetting : MonoBehaviour
{
    //돈 텍스트 변경
    public TMP_Text moneytext;

    // Start is called before the first frame update
    void Start()
    {
        //메인게임 시작시 데이터 불러옴
        DataManager.instance.LoadData();
        GameSetting();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //게임시작시 기본적으로 불러온 데이터를 기반으로 데이터 변경
    public void GameSetting()
	{
        moneytext.text = DataManager.instance.nowData.money;

    }
}
