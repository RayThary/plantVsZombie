using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainButton : MonoBehaviour
{
    private Button m_btnStart;
    private Button m_btnEnd;
    void Start()
    {
        m_btnStart = transform.GetChild(0).GetComponent<Button>();

        m_btnEnd = transform.GetChild(2).GetComponent<Button>();

        m_btnStart.onClick.AddListener(BtnStart);

        m_btnEnd.onClick.AddListener(BtnEnd);
    }

    private void BtnStart()
    {
        SceneManager.LoadSceneAsync(1);
    }

    private void BtnEnd()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
