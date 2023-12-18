using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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



    public GameObject resultPanal;
    public TMP_Text timetext;
    public TMP_Text scoretext;
    public TMP_Text ranktext;

    public int countdownTime;
    public TMP_Text countdownDisplay;
    bool canPlay = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountdownToStart());

    }

    // Update is called once per frame
    void Update()
    {

        if (canPlay)
        {
            if (minigameTimer.value > 0.0f)
            {
                minigameTimer.value -= Time.deltaTime;
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

        for(int i = 0; i < 3; i++)
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
            if (checkSpace < 3 && Input.GetKeyDown(KeyCode.Space))
            {
                //Debug.Log(slider.value);
                checkSpace++;
            }

            //�����̽��ٸ� ��������� �ڵ��� ������Ʈ �ȿ� ����ִٸ� �ش��ϴ� ������Ʈ �����ϰ� �����ʱ�ȭ
            for (int i = 0; i < 3; i++)
            {
                if (slider.value >= minPos[i] && slider.value <= maxPos[i] && Input.GetKeyDown(KeyCode.Space))
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

        switch(checknum)
		{
            case 0:
                ranktext.text = "ȹ�淩ũ F";
                break;
            case 1:
                ranktext.text = "ȹ�淩ũ C";
                break;
            case 2:
                ranktext.text = "ȹ�淩ũ B";
                break;
            default:
                ranktext.text = "ȹ�淩ũ A";
                break;
        }

		Time.timeScale = 0;

    }

    public void GoMainGame()
    {
        SceneManager.LoadScene("MainGame");
    }

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

}
