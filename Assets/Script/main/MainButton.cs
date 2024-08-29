using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainButton : MonoBehaviour
{
    private Button m_btnStart;
    private Button m_btnContinue;
    private Button m_btnEnd;

    private TextMeshProUGUI continueText;
    private int stage;
    void Start()
    {
        stage = PlayerPrefs.GetInt("Stage");

        m_btnStart = transform.GetChild(0).GetComponent<Button>();
        m_btnContinue = transform.GetChild(1).GetComponent<Button>();
        m_btnEnd = transform.GetChild(2).GetComponent<Button>();

        continueText = m_btnContinue.GetComponentInChildren<TextMeshProUGUI>();
        m_btnContinue.interactable = false;

        m_btnStart.onClick.AddListener(BtnStart);
        m_btnContinue.onClick.AddListener(BtnContinue);
        m_btnEnd.onClick.AddListener(BtnEnd);

        if (stage > 1)
        {
            m_btnContinue.interactable = true;
            continueText.text = stage + " Continue";
        }
        
    }

    private void BtnStart()
    {
        SceneManager.LoadSceneAsync(1);
    }

    private void BtnContinue()
    {

        SceneManager.LoadSceneAsync(stage);
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
