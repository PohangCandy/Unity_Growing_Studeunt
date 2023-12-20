using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainGameSetting : MonoBehaviour
{
    public static MainGameSetting instance;

    //�� �ؽ�Ʈ ����
    public TMP_Text moneytext;
    public TMP_Text statmoneytext;
    //��Ʈ�����ؽ�Ʈ
    public TMP_Text stresstext;
    public TMP_Text statstresstext;
    public Slider stressSlider;

    //�ð�, ��¥�ؽ�Ʈ
    public TMP_Text TimeText;
    public TMP_Text DateText;

    //�����͸� ���ϰ� ����ϱ� ���� �̱������� ����
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
        //���ΰ��� ���۽� ������ �ҷ���
        DataManager.instance.LoadData();
        GameSetting();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //���ӽ��۽� �⺻������ �ҷ��� �����͸� ������� ������ ����
    public void GameSetting()
	{
        moneytext.text = DataManager.instance.nowData.money.ToString() + "��";
        stresstext.text = DataManager.instance.nowData.stress.ToString();
        statmoneytext.text = DataManager.instance.nowData.money.ToString() + "��";
        statstresstext.text = DataManager.instance.nowData.stress.ToString();
        stressSlider.value = DataManager.instance.nowData.stress;

        //�ð��� ���ڸ����� �����ǰ� D2�� �Ͽ��� 0�� �տ�����
        TimeText.text = DataManager.instance.nowData.hour.ToString("D2") + ":"+DataManager.instance.nowData.minute.ToString("D2");
        DateText.text = DataManager.instance.nowData.day.ToString() + "����";

    }
}
