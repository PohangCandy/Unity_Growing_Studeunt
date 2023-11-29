using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TilteBtnMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

    public void GoTitle()
	{
        SceneManager.LoadScene("Title");
	}

    public void ExitGame()
	{
        Application.Quit();
        Debug.Log("EXIT GAME");
	}
}
