using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

//�̴ϰ����� ������ ���� Ŭ����
public class Game1 : MonoBehaviour
{
    //�����̴�����
    public Slider slider;
    //�����̴��� �ڵ��� �����̴� �ӵ�
    public int speed;

    //�����̴����� ���ӿ�����Ʈ ��ġ ����
    public float[] minPos = new float[3];
    public float[] maxPos = new float[3];
    public RectTransform[] good = new RectTransform[3];

    //������Ʈ ����(����Ű���)
    public GameObject[] box = new GameObject[3];

    //�����̴� �ڵ��� ����
    private bool checkdir = true;
    
    //��� �ν����� üũ
    public int checknum = 0;
    //�����̽��� ��� �������� üũ
    public int checkSpace = 0;

    //Ÿ�̸� ������Ʈ
    public Slider minigameTimer;
    private bool setgameover = false;
    public TMP_Text counttext;

    //���â �ؽ�Ʈ
    public GameObject resultPanal;
    public TMP_Text timetext;
    public TMP_Text scoretext;
    public TMP_Text ranktext;
    public TMP_Text stattext;

    //���ӽ����� ī��Ʈ�ٿ�
    public int countdownTime;
    public TMP_Text countdownDisplay;
    bool canPlay = false;

    public GameObject[] heartimage = new GameObject[3];

    // Start is called before the first frame update
    void Start()
    {
        //���ӽ����Ҷ� ������ 1�� ���� ,�ڷ�ƾ�����ؼ� ī��Ʈ�ٿ����� ���ӽ���
        Time.timeScale = 1;
        StartCoroutine(CountdownToStart());

    }

    // Update is called once per frame
    void Update()
    {

        //������ ����Ǹ� Ÿ�̸� ����
        if (canPlay)
        {
            if (minigameTimer.value > 0.0f)
            {
                minigameTimer.value -= Time.deltaTime;

                //mathf.floor�� �Ἥ �Ҽ��� ������ ����
                counttext.text = Mathf.Floor(minigameTimer.value).ToString();

            }

            else
            {
                setgameover = true;
            }
        }

    }

    public void ResetGame()
	{
        checkdir = true;
        setgameover = false;
        slider.value = 0;
        for (int i = 0; i < 3; i++)
		{
            minPos[i] = good[i].anchoredPosition.x;
            maxPos[i] = good[i].sizeDelta.x + minPos[i];
		}

        StartCoroutine(CheckBox());
	}


    //�ڷ�ƾ���� ����
    IEnumerator CheckBox()
    {
        yield return null;

        //�����̽��ٸ� 3�� ������������ ��� ����
        while ((checkSpace < 3) && !setgameover)
        {
            //��������
            if(checkdir)
                slider.value += Time.deltaTime * speed;
            else if(!checkdir)
                slider.value -= Time.deltaTime * speed;

            if (slider.value <= slider.minValue)
                checkdir = true;
            else if (slider.value >= slider.maxValue)
                checkdir = false;

            //�����̽��� üũ
            if (checkSpace < 3 && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
            {
                //Debug.Log(slider.value);
                heartimage[checkSpace].SetActive(false);
                checkSpace++;
            }

            //�����̽��ٸ� ��������� �ڵ��� ������Ʈ �ȿ� ����ִٸ� �ش��ϴ� ������Ʈ �����ϰ� �����ʱ�ȭ
            for (int i = 0; i < 3; i++)
            {
                if (slider.value >= minPos[i] && slider.value <= maxPos[i] && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
                {
                    checknum++;
                    box[i].SetActive(false);
                    minPos[i] = 0;
                    maxPos[i] = 0;
                    Debug.Log(i + "��°���ںμ���");
                }
            }

            yield return null;
            
		}
        resultPanal.SetActive(true);
        timetext.text = "�����ð� " + (int)minigameTimer.value +"��";
		scoretext.text = "ȹ�� ����Ʈ " + checknum + "/3��";

        //���Ӱ�� ���� � üũ�ߴ����� ���� ȹ�� ��ũ�� ������������ �ٸ�
        switch(checknum)
		{
            case 0:
                ranktext.text = "ȹ�淩ũ F";
                stattext.text = "10000��ȹ��";
                DataManager.instance.nowData.money += 10000;
                break;
            case 1:
                ranktext.text = "ȹ�淩ũ C";
                stattext.text = "50000��ȹ��\n�ڽŰ�,ü�� UP";
                DataManager.instance.nowData.money += 50000;
                DataManager.instance.nowData.conf += 1;
                DataManager.instance.nowData.hp += 1;
                break;
            case 2:
                ranktext.text = "ȹ�淩ũ B";
                stattext.text = "100000��ȹ��\n�ڽŰ�,ü�� UP";
                DataManager.instance.nowData.money += 100000;
                DataManager.instance.nowData.conf += 2;
                DataManager.instance.nowData.hp += 3;
                break;
            default:
                ranktext.text = "ȹ�淩ũ A";
                stattext.text = "200000��ȹ��\n�ڽŰ�,ü�� UP";
                DataManager.instance.nowData.money += 200000;
                DataManager.instance.nowData.conf += 3;
                DataManager.instance.nowData.hp += 5;
                break;
        }

        //�������� ������ �÷����ϸ� ��Ʈ������ ���δ�.
        DataManager.instance.nowData.stress += 10;
        //�ð��� �߰��Ѵ�.
        addhourTime(12);
        //����������
        DataManager.instance.SaveData();
        Time.timeScale = 0;

    }
    
    //���ΰ��Ӹ޴��� ���ư��� ��ư
    public void GoMainGame()
    {
        DataManager.instance.SaveData();
        SceneManager.LoadScene("MainGame");
    }

    //������ �ϱ����� ī��Ʈ�ٿ�
    IEnumerator CountdownToStart()
	{
        while(countdownTime > 0)
		{
            countdownDisplay.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);

            countdownTime--;
		}

        countdownDisplay.text = "GameStart!";

        canPlay = true;
        ResetGame();
        
        yield return new WaitForSeconds(1f);
        countdownDisplay.gameObject.SetActive(false);
	}

    void addhourTime(int hour)
	{
        if (DataManager.instance.nowData.hour + hour > 24)
		{
            DataManager.instance.nowData.hour = DataManager.instance.nowData.hour - hour;
            DataManager.instance.nowData.day++;

        }
        else
		{
            DataManager.instance.nowData.hour += hour;
        }
	}

}
