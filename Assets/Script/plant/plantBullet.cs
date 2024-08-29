using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plantBullet : MonoBehaviour
{
    public enum bulletType
    {
        basic,
        ice,
        cherryBomb,
    }
    [SerializeField] private bulletType bullet;
    public bulletType GetBulletType { get { return bullet; } }
    [SerializeField] private float bulletSpeed = 2;
    [SerializeField] private float bulletDamage = 1;
    public float GetBulletDamage { get { return bulletDamage; } }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (bullet == bulletType.basic|| bullet == bulletType.ice)
        {
            transform.position += new Vector3(bulletSpeed, 0, 0) * Time.deltaTime;
        }
        else if( bullet == bulletType.cherryBomb)
        {
            StartCoroutine(cherryBombDestroy());
        }
    }

    IEnumerator cherryBombDestroy()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
