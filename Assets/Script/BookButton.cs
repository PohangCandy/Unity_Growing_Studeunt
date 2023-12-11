using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookButton : MonoBehaviour
{
    public GameObject Image1;
    public GameObject Image2;
    public GameObject Image3;
    public GameObject Image4;
    public GameObject Image5;
    public GameObject Image6;
    public GameObject Image7;
    public GameObject Image8;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowImage1()
	{
        Image1.SetActive(true);
	}

    public void ShowImage2()
    {
        Image2.SetActive(true);
    }

    public void ShowImage3()
    {
        Image3.SetActive(true);
    }
    public void ShowImage4()
    {
        Image4.SetActive(true);
    }
    public void ShowImage5()
    {
        Image5.SetActive(true);
    }
    public void ShowImage6()
    {
        Image6.SetActive(true);
    }
    public void ShowImage7()
    {
        Image7.SetActive(true);
    }

    public void ShowImage8()
    {
        Image8.SetActive(true);
    }

}
