using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Game1 : MonoBehaviour
{
    //슬라이더정보
    public Slider slider;
    //슬라이더의 핸들이 움직이는 속도
    public int speed;

    //슬라이더안의 게임오브젝트 위치 정보
    public float[] minPos = new float[3];
    public float[] maxPos = new float[3];
    public RectTransform[] good = new RectTransform[3];

    //오브젝트 정보(껐다키기용)
    public GameObject[] box = new GameObject[3];

    //슬라이더 핸들의 방향
    private bool checkdir = true;
    
    //몇개나 부쉈는지 체크
    public int checknum = 0;
    //스페이스바 몇번 눌렀는지 체크
    public int checkSpace = 0;

    //타이머 오브젝트
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


    //코루틴으로 실행
    IEnumerator CheckBox()
    {
        yield return null;

        //스페이스바를 3번 누르기전까지 계속 실행
        while ((checkSpace < 3) && !setgameover)
        {
            //방향조절
            if(checkdir)
                slider.value += Time.deltaTime * speed;
            else if(!checkdir)
                slider.value -= Time.deltaTime * speed;

            if (slider.value <= slider.minValue)
                checkdir = true;
            else if (slider.value >= slider.maxValue)
                checkdir = false;

            //스페이스바 체크
            if (checkSpace < 3 && Input.GetKeyDown(KeyCode.Space))
            {
                //Debug.Log(slider.value);
                checkSpace++;
            }

            //스페이스바를 눌렀을경우 핸들이 오브젝트 안에 들어있다면 해당하는 오브젝트 삭제하고 정보초기화
            for (int i = 0; i < 3; i++)
            {
                if (slider.value >= minPos[i] && slider.value <= maxPos[i] && Input.GetKeyDown(KeyCode.Space))
                {
                    checknum++;
                    box[i].SetActive(false);
                    minPos[i] = 0;
                    maxPos[i] = 0;
                    Debug.Log(i + "번째상자부서짐");
                }
            }

            yield return null;
            
		}
        resultPanal.SetActive(true);
        timetext.text = "남은시간 " + (int)minigameTimer.value +"초";
		scoretext.text = "획득 포인트 " + checknum + "/3점";

        switch(checknum)
		{
            case 0:
                ranktext.text = "획득랭크 F";
                break;
            case 1:
                ranktext.text = "획득랭크 C";
                break;
            case 2:
                ranktext.text = "획득랭크 B";
                break;
            default:
                ranktext.text = "획득랭크 A";
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
