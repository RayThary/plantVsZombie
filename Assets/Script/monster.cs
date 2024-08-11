using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class monster : MonoBehaviour
{
    [SerializeField] private float hp = 10;
    [SerializeField] private float moveSpeed = 4;
    private float beforeMoveSpeed;

    private bool isMoving = true;

    private Animator anim;
    private BoxCollider2D box2d;

    private Plant plant;
    private SpriteControl sprControl;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            plantBullet plant = collision.GetComponent<plantBullet>();
            float damage = plant.GetBulletDamage;
            if(plant.GetBulletType == plantBullet.bulletType.ice)
            {
                moveSpeed = moveSpeed / 2;
                sprControl.SetReturnTime(1f);
                sprControl.SetIsHit(true, Color.blue);
                Invoke("returnSpeed", 1f);
                
            }
            hp -= damage;
        }
        
        if(collision.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            sprControl.SetReturnTime(99);
            sprControl.SetIsHit(true, Color.black);
            box2d.enabled = false;
            hp -= 99;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("LawnMower"))
        {
            box2d.enabled = false;
            hp -= 99;
        }

    }


    void Start()
    {
        anim = GetComponent<Animator>();
        box2d = GetComponent<BoxCollider2D>();
        sprControl = GetComponent<SpriteControl>();
        beforeMoveSpeed = moveSpeed;
    }

    void Update()
    {
        if (hp <= 0)
        {
            box2d.enabled = false;
            anim.SetTrigger("Death");
            return;
        }
        move();
        attack();
    }

    private void move()
    {
        if (isMoving)
        {
            transform.position -= new Vector3(moveSpeed, 0, 0) * Time.deltaTime;
        }

    
    }

    private void returnSpeed()
    {
        moveSpeed = beforeMoveSpeed;
    }

    private void attack()
    {
        Vector2 origin = transform.position;
        origin.y += 0.5f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(-1, 0), 1f, LayerMask.GetMask("Plant"));

        if (hit.collider != null)
        {
            isMoving = false;
            plant = hit.transform.GetComponent<Plant>();
            anim.SetBool("Attack", true);
        }
        else
        {
            isMoving = true;
            anim.SetBool("Attack", false);
        }
    }

    private void HitPlant()
    {
        if (plant != null)
        {
            plant.SetHitCheck(1);
        }
    }

    private void deathAinm()
    {
        Destroy(gameObject);
    }
}
