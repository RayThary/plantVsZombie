using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private Transform menuTrs;

    private Button menu;

    private Button SFX;
    private Button BGM;

    private Image SFXImage;
    private Image BGMImage;

    private Button reStart;
    private Button quitGame;
    private Button backToGame;

    void Start()
    {
        menuTrs = transform.GetChild(1);
        menu = transform.GetChild(0).GetComponent<Button>();

        SFX = menuTrs.GetChild(0).GetComponent<Button>();
        BGM = menuTrs.GetChild(1).GetComponent<Button>();
        SFXImage = SFX.transform.GetChild(0).GetComponent<Image>();
        BGMImage = BGM.transform.GetChild(0).GetComponent<Image>();

        reStart = menuTrs.GetChild(2).GetComponent<Button>();
        quitGame = menuTrs.GetChild(3).GetComponent<Button>();
        backToGame = menuTrs.GetChild(4).GetComponent<Button>();

        menu.onClick.AddListener(btnMenu);

        SFX.onClick.AddListener(btnSFX);
        BGM.onClick.AddListener(btnBGM);

        reStart.onClick.AddListener(btnReStart);
        quitGame.onClick.AddListener(btnQuitGame);
        backToGame.onClick.AddListener(btnBackToGame);
    }

    private void btnSFX()
    {
        if (SFXImage.enabled == false)
        {
            SFXImage.enabled = true;
            SoundManager.instance.SetSFXSound(false);
        }
        else
        {
            SFXImage.enabled = false;
            SoundManager.instance.SetSFXSound(true);
        }
    }

    private void btnBGM()
    {

        if(BGMImage.enabled == false)
        {
            BGMImage.enabled = true;
            SoundManager.instance.SetBGMSound(false);
        }
        else
        {
            BGMImage.enabled = false;
            SoundManager.instance.SetBGMSound(true);
        }
    }

    private void btnMenu()
    {
        Time.timeScale = 0;
        menuTrs.gameObject.SetActive(true);
    }

    private void btnReStart()
    {   
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(1);
    }

    private void btnQuitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(0);
    }

    private void btnBackToGame()
    {
        menuTrs.gameObject.SetActive(false);
        Time.timeScale = 1;
    }


}
