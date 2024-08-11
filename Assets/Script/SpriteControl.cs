using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpriteControl : MonoBehaviour
{
    [SerializeField] private bool isHit = false;

    [SerializeField] private float ReturnTime = 0.5f;//원상복귀시간

    [SerializeField] private Transform[] childList;// 모든자식들모음
    private List<SpriteRenderer> m_spr = new List<SpriteRenderer>();//스프라이랜더러있는것들모음

    private List<Color> m_basicColor = new List<Color>();//기본색깔

    private bool isReturnCheck = false;
    private int isSprite;// 스프라이트개수

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
    /// 히트체크
    /// </summary>
    /// <param name="_value">히트유무</param>
    /// <param name="_color">히트시 색깔변환</param>
    public void SetIsHit(bool _value, Color _color)
    {
        isHit = _value;
        chageColor = _color;
    }
}
