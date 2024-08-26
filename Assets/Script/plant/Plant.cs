using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public enum PlantType
    {
        basic,
        wall,
        ice,
        sun,
        cherryBomb,
    }
    [SerializeField] private PlantType plantType;
    [SerializeField] private float hp = 5;
    [SerializeField] private float attackDealy = 2;
    private float timer = 0;
    private Transform shotTrs;
    private Animator anim;
    [SerializeField] private GameObject bullet;
    private tiles tile;
    void Start()
    {
        if (plantType == PlantType.basic || plantType == PlantType.ice)
        {
            shotTrs = transform.GetChild(0);
        }
        else if (plantType == PlantType.cherryBomb)
        {
            anim = GetComponent<Animator>();
        }


    }


    void Update()
    {
        patten();
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void patten()
    {
        if (plantType == PlantType.basic || plantType == PlantType.ice)
        {
            timer += Time.deltaTime;
            if (timer > attackDealy)
            {
                int random = Random.Range(0, 2);
                if(random == 0)
                {
                    SoundManager.instance.SFXCreate(SoundManager.Clips.plantShot1, 0.5f, 0, transform);
                }
                else
                {
                    SoundManager.instance.SFXCreate(SoundManager.Clips.plantShot2, 0.5f, 0, transform);
                }

                GameObject bulletObj = Instantiate(bullet);
                bulletObj.transform.parent = GameManager.instance.GetPlantBulletParent;
                bulletObj.transform.position = shotTrs.position;
                timer = 0;
            }
        }
        else if (plantType == PlantType.sun)
        {
            timer += Time.deltaTime;
            if (timer > attackDealy)
            {
                float endPos = transform.position.y + 0.5f;
                GameObject sunObj = Instantiate(bullet);
                sunObj.GetComponent<sun>().SetIsPlant(true);
                sunObj.GetComponent<sun>().SetEndPosY(endPos);
                sunObj.transform.parent = GameManager.instance.GetPlantBulletParent;
                sunObj.transform.position = transform.position;
                timer = 0;
            }
        }
        else if (plantType == PlantType.cherryBomb)
        {
            if (transform.localScale.x <= 1.5f)
            {
                transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * 0.2f;
            }
            else
            {
                anim.SetTrigger("bomb");
            }
        }

    }

    public void SetHitCheck(float _damage)
    {
        hp -= _damage;
    }

    private void OnDestroy()
    {
        tile.SetUseTileCheck = false;
    }

    public void SetTile(RaycastHit2D _hit)
    {
        tile = _hit.transform.GetComponent<tiles>();
    }

    //眉府气藕局聪皋捞记侩
    private void CherryBomb()
    {
        GameObject cherryBombBox = Instantiate(bullet);
        cherryBombBox.transform.position = transform.position;
    }

    //眉府气藕局聪皋捞记侩
    private void CherryBombEnd()
    {
        Destroy(gameObject);
    }
}
