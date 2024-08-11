using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndWall : MonoBehaviour
{
    [SerializeField]private Transform lawnMower;

    private bool lawnMowerMove = false;
    private bool lastChance = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            if (lastChance)
            {
                lawnMowerMove = true;
                lawnMower.GetComponent<BoxCollider2D>().enabled = true;
                lastChance = false;
            }
            else
            {
                GameManager.instance.SetEnd();
                Time.timeScale = 0;
            }
        }
    }
    private void Start()
    {
        lawnMower = transform.GetChild(0);
    }
    private void Update()
    {
        if (lawnMowerMove)
        {
            lawnMower.transform.position += new Vector3(1, 0, 0) * Time.deltaTime * 10;
        }
    }

}
