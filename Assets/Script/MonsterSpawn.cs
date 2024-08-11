using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnTrs = new List<Transform>();

    [SerializeField] private List<GameObject> monster = new List<GameObject>();

    void Start()
    {
        InvokeRepeating("zombieSwpan", 2, 3);
    }



    private void zombieSwpan()
    {
        int spawnPos = Random.Range(0, spawnTrs.Count);

        GameObject zombie = Instantiate(monster[0]);
        zombie.transform.position = spawnTrs[spawnPos].position;
        zombie.transform.parent = GameManager.instance.GetMonsterParent;
    }
}
