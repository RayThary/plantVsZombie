using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sun : MonoBehaviour
{
    [SerializeField] private bool isPlant = false;
    [SerializeField] private float downSpeed = 0.5f;
    [SerializeField] private float isPlantSpeed = 0.5f;

    private bool downCheck = false;
    private Vector2 targetVec;

    private bool noMove = false;

    private float endPosY;
    [SerializeField] private float rotateSpeed = 1;

    private float timer = 0;
    private Color sprAlpha;
    private bool checkAlpha;
    private SpriteRenderer spr;
    private Transform sprTrs;
    void Start()
    {
        spr = GetComponentInChildren<SpriteRenderer>();
        sprTrs = spr.GetComponent<Transform>();
        sprAlpha = spr.color;
    }

    // Update is called once per frame
    void Update()
    {
        sunRotation();
        move();
        removeSun();
    }

    private void sunRotation()
    {
        sprTrs.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * rotateSpeed);
    }
    private void move()
    {
        if (noMove)
        {
            return;
        }
        if (isPlant)
        {
            if (downCheck == false)
            {
                transform.position += new Vector3(0, 1, 0) * Time.deltaTime * isPlantSpeed;
                if (transform.position.y >= endPosY)
                {
                    float x = Random.Range(transform.position.x - 0.5f, transform.position.x + 0.5f);
                    float y = Random.Range(transform.position.y - 0.6f, transform.position.y - 1);
                    targetVec = new Vector2(x, y);
                    downCheck = true;
                }
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, targetVec, Time.deltaTime * isPlantSpeed);
                if (transform.position == new Vector3(targetVec.x, targetVec.y, 0))
                {
                    noMove = true;
                }
            }
        }
        else
        {
            if (transform.position.y >= endPosY)
            {
                transform.position -= new Vector3(0, -1, 0) * downSpeed * Time.deltaTime;
            }
            else
            {
                noMove = true;
            }

        }
    }

    private void removeSun()
    {
        if (noMove)
        {
            timer += Time.deltaTime;
            if (timer > 8)
            {
                sunAlpha();
            }
            else if(timer > 13)
            {
                Destroy(gameObject);
            }
        }
    }

    private void sunAlpha()
    {
        if (sprAlpha.a >= 1)
        {
            checkAlpha = false;
        }
        else if( sprAlpha.a<0.6f)
        {
            checkAlpha = true;
        }

        if (checkAlpha)
        {
        sprAlpha.a += Time.deltaTime;
        }
        else
        {
            sprAlpha.a -= Time.deltaTime;
        }
        spr.color = sprAlpha;
    }
    private void OnMouseDown()
    {
        SoundManager.instance.SFXCreate(SoundManager.Clips.Sun, 1, 0, GameManager.instance.GetPlantBulletParent);
        GameManager.instance.AddPoint(25);
        Destroy(gameObject);
    }
    public void SetEndPosY(float _y)
    {
        endPosY = _y;
    }
    public void SetIsPlant(bool _value)
    {
        isPlant = _value;
    }
}
