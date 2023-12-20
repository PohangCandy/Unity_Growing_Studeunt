using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

//미니게임의 정보를 가질 클래스
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
    public TMP_Text counttext;

    //결과창 텍스트
    public GameObject resultPanal;
    public TMP_Text timetext;
    public TMP_Text scoretext;
    public TMP_Text ranktext;
    public TMP_Text stattext;

    //게임시작전 카운트다운
    public int countdownTime;
    public TMP_Text countdownDisplay;
    bool canPlay = false;

    public GameObject[] heartimage = new GameObject[3];

    // Start is called before the first frame update
    void Start()
    {
        //게임시작할때 스케일 1로 시작 ,코루틴시작해서 카운트다운이후 게임시작
        Time.timeScale = 1;
        StartCoroutine(CountdownToStart());

    }

    // Update is called once per frame
    void Update()
    {

        //게임이 실행되면 타이머 시작
        if (canPlay)
        {
            if (minigameTimer.value > 0.0f)
            {
                minigameTimer.value -= Time.deltaTime;

                //mathf.floor을 써서 소수점 날리고 구현
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
            if (checkSpace < 3 && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
            {
                //Debug.Log(slider.value);
                heartimage[checkSpace].SetActive(false);
                checkSpace++;
            }

            //스페이스바를 눌렀을경우 핸들이 오브젝트 안에 들어있다면 해당하는 오브젝트 삭제하고 정보초기화
            for (int i = 0; i < 3; i++)
            {
                if (slider.value >= minPos[i] && slider.value <= maxPos[i] && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
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

        //게임결과 현재 몇개 체크했는지에 따라 획득 랭크및 스탯증가량이 다름
        switch(checknum)
		{
            case 0:
                ranktext.text = "획득랭크 F";
                stattext.text = "10000원획득";
                DataManager.instance.nowData.money += 10000;
                break;
            case 1:
                ranktext.text = "획득랭크 C";
                stattext.text = "50000원획득\n자신감,체력 UP";
                DataManager.instance.nowData.money += 50000;
                DataManager.instance.nowData.conf += 1;
                DataManager.instance.nowData.hp += 1;
                break;
            case 2:
                ranktext.text = "획득랭크 B";
                stattext.text = "100000원획득\n자신감,체력 UP";
                DataManager.instance.nowData.money += 100000;
                DataManager.instance.nowData.conf += 2;
                DataManager.instance.nowData.hp += 3;
                break;
            default:
                ranktext.text = "획득랭크 A";
                stattext.text = "200000원획득\n자신감,체력 UP";
                DataManager.instance.nowData.money += 200000;
                DataManager.instance.nowData.conf += 3;
                DataManager.instance.nowData.hp += 5;
                break;
        }

        //공용으로 게임을 플레이하면 스트레스가 쌓인다.
        DataManager.instance.nowData.stress += 10;
        //시간을 추가한다.
        addhourTime(12);
        //데이터저장
        DataManager.instance.SaveData();
        Time.timeScale = 0;

    }
    
    //메인게임메뉴로 돌아가는 버튼
    public void GoMainGame()
    {
        DataManager.instance.SaveData();
        SceneManager.LoadScene("MainGame");
    }

    //게임을 하기전의 카운트다운
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
