using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainGameSetting : MonoBehaviour
{
    //�� �ؽ�Ʈ ����
    public TMP_Text moneytext;

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
        moneytext.text = DataManager.instance.nowData.money;

    }
}
