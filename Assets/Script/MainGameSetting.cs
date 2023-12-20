using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainGameSetting : MonoBehaviour
{
    public static MainGameSetting instance;

    //돈 텍스트 변경
    public TMP_Text moneytext;
    public TMP_Text statmoneytext;
    //스트레스텍스트
    public TMP_Text stresstext;
    public TMP_Text statstresstext;
    public Slider stressSlider;

    //시간, 날짜텍스트
    public TMP_Text TimeText;
    public TMP_Text DateText;

    //데이터를 편하게 사용하기 위해 싱글톤으로 저장
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
    }

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
        moneytext.text = DataManager.instance.nowData.money.ToString() + "원";
        stresstext.text = DataManager.instance.nowData.stress.ToString();
        statmoneytext.text = DataManager.instance.nowData.money.ToString() + "원";
        statstresstext.text = DataManager.instance.nowData.stress.ToString();
        stressSlider.value = DataManager.instance.nowData.stress;

        //시간이 두자리수가 유지되게 D2를 하여서 0을 앞에붙임
        TimeText.text = DataManager.instance.nowData.hour.ToString("D2") + ":"+DataManager.instance.nowData.minute.ToString("D2");
        DateText.text = DataManager.instance.nowData.day.ToString() + "일차";

    }
}
