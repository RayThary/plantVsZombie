using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FailMenu : MonoBehaviour
{
    private GameObject failMenu;
    private Button quitButton;
    private bool oneCheck = false;

    void Start()
    {
        failMenu = transform.GetChild(0).gameObject;
        quitButton = failMenu.GetComponentInChildren<Button>();
        quitButton.onClick.AddListener(quit);
    }

    private void quit()
    {
        SceneManager.LoadSceneAsync(0);
    }

    void Update()
    {
        if (transform.localScale.x <= 1f)
        {
            transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * 0.5f;
        }
        else
        {
            if (oneCheck == false)
            {
                Invoke("failMenuON", 1);
                oneCheck = true;
            }
        }
    }

    private void failMenuON()
    {
        Image img = GetComponent<Image>();
        img.enabled = false;
        failMenu.SetActive(true);
    }
}