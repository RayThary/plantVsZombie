using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]private float usingPoint = 0;
    public float GetPoint { get { return usingPoint; } }


    private GameObject currentPlant;//��ưŬ���� �̰��� ���������ʿ�����
    private Sprite currentPlantSpr;

    private Transform plantParent;
    private Transform plantBulletParent;
    public Transform GetPlantBulletParent { get { return plantBulletParent; } }

    private Transform monsterParent;
    public Transform GetMonsterParent { get { return monsterParent; } }
    [SerializeField] private Slider slider;

    [SerializeField] private int monsterMax = 20;
    private float monsterCount = 0;
    [SerializeField] private List<Transform> spawnTrs = new List<Transform>();
    [SerializeField] private List<GameObject> monster = new List<GameObject>();

    [SerializeField] private Transform tiles;
    [SerializeField] private LayerMask tileMask;

    [SerializeField] private GameObject EndImage;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        plantParent = transform.GetChild(0);
        plantBulletParent = transform.GetChild(1);
        monsterParent = transform.GetChild(2);


    }
    void Start()
    {
        //InvokeRepeating("zombieSwpan", 2, 3);
        StartCoroutine(zombieSwpan());
        monsterCount = monsterMax;
        slider.maxValue = monsterMax;
    }

    IEnumerator zombieSwpan()
    {
        yield return new WaitForSeconds(2);

        for (int i = 0; i < monsterMax; i++)
        {
            monsterCount--;
            int spawnPos = Random.Range(0, spawnTrs.Count);
            int spawnType = Random.Range(0, 3);
            GameObject zombie = Instantiate(monster[spawnType]);
            zombie.transform.position = spawnTrs[spawnPos].position;
            zombie.transform.parent = GameManager.instance.GetMonsterParent;
            yield return new WaitForSeconds(3);
        }
    }


    void Update()
    {
        mouseCheck();

        slider.value = monsterCount;
    }

    private void mouseCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, tileMask);

        foreach (Transform tile in tiles)
        {
            tile.GetComponent<SpriteRenderer>().enabled = false;
        }

        if (hit.collider && currentPlant)
        {
            if (hit.transform.GetComponent<tiles>().GetUseTileCheck == false)
            {

                hit.collider.GetComponent<SpriteRenderer>().sprite = currentPlantSpr;
                hit.collider.GetComponent<SpriteRenderer>().enabled = true;

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    GameObject plant = Instantiate(currentPlant);

                    plant.transform.position = hit.transform.position;
                    plant.transform.parent = plantParent;
                    //�ڽ��� ��ġ��Ÿ���� ��ũ��Ʈ�������������Ѱ�
                    plant.GetComponent<Plant>().SetTile(hit);

                    hit.transform.GetComponent<tiles>().SetUseTileCheck = true;
                    currentPlant = null;
                    currentPlantSpr = null;
                }
            }
        }
    }
    public void buyPlant(GameObject _plant, Sprite _plantSpr)
    {
        currentPlant = _plant;
        currentPlantSpr = _plantSpr;

    }

    public void AddPoint(float _value)
    {
        usingPoint += _value;
    }

    public void MinusPoint(float _value)
    {
        usingPoint -= _value;
    }
    public void SetEnd()
    {
        EndImage.SetActive(true);
    }
}
