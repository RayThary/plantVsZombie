using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailTrigger : MonoBehaviour
{
    private Transform lawnMower;
    private BoxCollider2D box2d;

    private bool lawnMowerMove = false;
    private bool lastChance = true;
    private bool failCheck = false;

    private float timer = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            if (lastChance)
            {
                lawnMowerMove = true;
                lawnMower.GetComponent<BoxCollider2D>().enabled = true;
                lastChance = false;
                SoundManager.instance.SFXCreate(SoundManager.Clips.lawnmower, 1, 0, transform);
                box2d.enabled = false;
            }
            else
            {
                SoundManager.instance.bgSoundPause();
                GameManager.instance.SetSoundOFF();

                SoundManager.instance.SFXCreate(SoundManager.Clips.losemusic, 1, 0, transform);
                failCheck = true;
            }
        }
    }
    
  
    private void Start()
    {
        box2d= GetComponent<BoxCollider2D>();
        lawnMower = transform.GetChild(0);
    }
    private void Update()
    {
        lawnMowerMoving();
        boxOnTimer();
        fail();
    }

    private void lawnMowerMoving()
    {
        if (lawnMowerMove)
        {
            lawnMower.transform.position += new Vector3(1, 0, 0) * Time.deltaTime * 10;
            if (lawnMower.position.x > 50)
            {
                Destroy(lawnMower.gameObject);
                lawnMowerMove = false;
            }
        }
    }

    private void boxOnTimer()
    {
        if (box2d.enabled == false)
        {
            timer += Time.deltaTime;
            if (timer > 1)
            {
                box2d.enabled = true;
            }
        }
    }

    private void fail()
    {
        if (failCheck)
        {
            StartCoroutine(failTrigger());
            failCheck = false;
        }
    }

    IEnumerator failTrigger()
    {
        yield return new WaitForSeconds(3);
        GameManager.instance.SetEnd();
        SoundManager.instance.SFXCreate(SoundManager.Clips.scream, 1, 0, transform);
        yield return new WaitForSeconds(3);

    }
}
