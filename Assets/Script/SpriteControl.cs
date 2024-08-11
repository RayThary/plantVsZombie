using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpriteControl : MonoBehaviour
{
    [SerializeField] private bool isHit = false;

    [SerializeField] private float ReturnTime = 0.5f;//���󺹱ͽð�

    [SerializeField] private Transform[] childList;// ����ڽĵ����
    private List<SpriteRenderer> m_spr = new List<SpriteRenderer>();//�������̷������ִ°͵����

    private List<Color> m_basicColor = new List<Color>();//�⺻����

    private bool isReturnCheck = false;
    private int isSprite;// ��������Ʈ����

    private Color chageColor;
    private BoxCollider2D box2d;
    void Start()
    {
        box2d = GetComponent<BoxCollider2D>();

        childList = transform.GetComponentsInChildren<Transform>();
        findSprite();

    }
    private void findSprite()
    {
        isSprite = 0;

        for (int i = 0; i < childList.Length; i++)
        {
            if (childList[i].GetComponent<SpriteRenderer>() != null)
            {

                m_spr.Add(childList[i].GetComponent<SpriteRenderer>());
                m_basicColor.Add(m_spr[isSprite].color);

                isSprite += 1;
            }
        }

    }

    void Update()
    {
        if (isHit)
        {
            isReturnCheck = true;
            colorControl();
            isHit = false;
        }
    }

    private void colorControl()
    {
        for (int i = 0; i < isSprite; i++)
        {
            m_spr[i].color = chageColor;
            Invoke("colorReturn", ReturnTime);
        }
    }

    private void colorReturn()
    {
        if (box2d.enabled == false)
        {
            return;
        }
        for (int i = 0; i < isSprite; i++)
        {
            m_spr[i].color = m_basicColor[i];
        }
        isReturnCheck = false;
    }

    public void SetReturnTime(float _time)
    {
        ReturnTime = _time;
    }

    public bool GetColorReturnCheck()
    {
        return isReturnCheck;
    }
    /// <summary>
    /// ��Ʈüũ
    /// </summary>
    /// <param name="_value">��Ʈ����</param>
    /// <param name="_color">��Ʈ�� ����ȯ</param>
    public void SetIsHit(bool _value, Color _color)
    {
        isHit = _value;
        chageColor = _color;
    }
}
