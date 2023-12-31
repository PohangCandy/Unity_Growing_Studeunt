using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TilteBtnMove : MonoBehaviour
{
    public void GoBooks()
	{
        SceneManager.LoadScene("Books");
	}

    public void NewGame()
	{
        SceneManager.LoadScene("NewGame");
	}

    public void Continue()
	{
        SceneManager.LoadScene("MainGame");
	}

    public void ContinueGame()
    {
        //불러올 데이터가 있는지 체크하기
        if (!DataManager.instance.DataCheck())
        {
            return;
        }
        SceneManager.LoadScene("MainGame");
        DataManager.instance.LoadData();
    }

    public void GoTitle()
	{
        SceneManager.LoadScene("Title");
	}

    public void GoStore()
    {
        SceneManager.LoadScene("Store");
    }

    public void GoGameScene()
	{
        SceneManager.LoadScene("GameScene");
    }

    public void ExitGame()
	{
        DataManager.instance.SaveData();
        Application.Quit();
        Debug.Log("EXIT GAME");
	}
}
