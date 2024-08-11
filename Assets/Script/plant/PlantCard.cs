using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlantCard : MonoBehaviour
{
    [SerializeField] private GameObject m_plant;
    [SerializeField] private Sprite m_plantSpr;
    [SerializeField] private float m_price = 10;

    private Button m_button;
    private TextMeshProUGUI m_text;//첫번째자식으로만들어줄것
    [SerializeField]private Image m_image;//마지막자식으로 넣어줄것
    [SerializeField]private float buyCoolTime = 3;
    void Start()
    {

        m_button = GetComponent<Button>();
        m_button.onClick.AddListener(buyPlant);
        m_text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        m_text.text = m_price.ToString();
    }

    private void Update()
    {
        if (m_button.enabled == false)
        {
            m_image.fillAmount -= Time.deltaTime/ buyCoolTime;
            if(m_image.fillAmount <= 0)
            {
                m_button.enabled = true;
            }
        }
    }

    private void buyPlant()
    {
        if (GameManager.instance.GetPoint >= m_price)
        {
            GameManager.instance.MinusPoint(m_price);
            GameManager.instance.buyPlant(m_plant, m_plantSpr);
            m_button.enabled = false;
            m_image.fillAmount = 1;
        }
    }


}
