using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private Transform menuTrs;

    private Button menu;

    private Button reStart;
    private Button quitGame;
    private Button backToGame;
    
    void Start()
    {
        menuTrs = transform.GetChild(1);
        menu = transform.GetChild(0).GetComponent<Button>();

        reStart = menuTrs.GetChild(0).GetComponent<Button>();
        quitGame =menuTrs.GetChild(1).GetComponent<Button>();
        backToGame = menuTrs.GetChild(2).GetComponent<Button>();

        menu.onClick.AddListener(btnMenu);

        reStart.onClick.AddListener(btnReStart);
        quitGame.onClick.AddListener(btnQuitGame);
        backToGame.onClick.AddListener(btnBackToGame);
    }

    private void btnMenu()
    {
        Time.timeScale = 0;
        menuTrs.gameObject.SetActive(true);
    }

    private void btnReStart()
    {
        //����� ���ӸŴ������� ���°������������ üũ�� ���߿� �����־��ٰ�
        Debug.Log("���� ����������");
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(0);
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
