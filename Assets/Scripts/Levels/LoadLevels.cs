using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevels : MonoBehaviour
{
    //Levels gombok + Level Scene betöltések 

    public Button[] buttons;
    public Sprite spriteRenderer1; // interactable = false
    public Sprite spriteRenderer2; // last interactable
    public Sprite spriteRenderer3; // interactable = true, kivéve last


    private void Awake()
    {
        if (buttons.Length > 0)
        {
            int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].interactable = false;
                Image buttonImage = buttons[i].GetComponent<Image>();
                if (buttonImage != null)
                {
                    buttonImage.sprite = spriteRenderer1;
                }
            }
            for (int i = 0; i < unlockedLevel; i++)
            {
                buttons[i].interactable = true;
                Image buttonImage = buttons[i].GetComponent<Image>();
                if (buttonImage != null)
                {
                    if (i == unlockedLevel - 1)
                    {
                        buttonImage.sprite = spriteRenderer2;
                    }
                    else
                    {
                        buttonImage.sprite = spriteRenderer3;
                    }
                }
            }
        }
    }
    public void LoadLevel1()
    {
        SceneManager.LoadScene(5);
    }
    public void LoadLevel2()
    {
        SceneManager.LoadScene(6);
    }
    public void LoadLevel3()
    {
        SceneManager.LoadScene(7);
    }
    public void LoadLevel4()
    {
        SceneManager.LoadScene(8);
    }
    public void LoadLevel5()
    {
        SceneManager.LoadScene(9);
    }
    public void LoadLevel6()
    {
        SceneManager.LoadScene(10);
    }
    public void LoadLevel7()
    {
        SceneManager.LoadScene(11);
    }
    public void LoadLevel8()
    {
        SceneManager.LoadScene(12);
    }
    public void LoadLevel9()
    {
        SceneManager.LoadScene(13);
    }
    public void LoadLevel10()
    {
        SceneManager.LoadScene(14);
    }
    public void LoadLevel11()
    {
        SceneManager.LoadScene(15);
    }
    public void LoadLevel12()
    {
        SceneManager.LoadScene(16);
    }
    public void LoadLevel13()
    {
        SceneManager.LoadScene(17);
    }
    public void LoadLevel14()
    {
        SceneManager.LoadScene(18);
    }
    public void LoadLevel15()
    {
        SceneManager.LoadScene(19);
    }
    public void LoadLevel16()
    {
        SceneManager.LoadScene(20);
    }
    public void LoadLevel17()
    {
        SceneManager.LoadScene(21);
    }
    public void LoadLevel18()
    {
        SceneManager.LoadScene(22);
    }
    public void LoadLevel19()
    {
        SceneManager.LoadScene(23);
    }
    public void LoadLevel20()
    {
        SceneManager.LoadScene(24);
    }
    public void LoadLevel21()
    {
        SceneManager.LoadScene(25);
    }
    public void LoadLevel22()
    {
        SceneManager.LoadScene(26);
    }
    public void LoadLevel23()
    {
        SceneManager.LoadScene(27);
    }
    public void LoadLevel24()
    {
        SceneManager.LoadScene(28);
    }
    public void LoadLevel25()
    {
        SceneManager.LoadScene(29);
    }
    public void LoadLevel26()
    {
        SceneManager.LoadScene(30);
    }
    public void LoadLevel27()
    {
        SceneManager.LoadScene(31);
    }
    public void LoadLevel28()
    {
        SceneManager.LoadScene(32);
    }
    public void LoadLevel29()
    {
        SceneManager.LoadScene(33);
    }
    public void LoadLevel30()
    {
        SceneManager.LoadScene(34);
    }



}
