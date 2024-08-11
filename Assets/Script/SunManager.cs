using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunManager : MonoBehaviour
{
    [SerializeField] private GameObject sunObj;
    [SerializeField] private float sunSpawnDealy = 4;
    private Transform spawnTrs;
    private float sunTimer = 0.0f;
    void Start()
    {
        spawnTrs = transform.GetChild(0);

    }

    // Update is called once per frame
    void Update()
    {
        sunTimer += Time.deltaTime;
        if (sunTimer >= sunSpawnDealy)
        {
            float spawnX = Random.Range(-8.5f, 6);
            float endPos = Random.Range(3f, -4.5f);
            GameObject Sun = Instantiate(sunObj, transform);
            Sun.transform.position = new Vector2(spawnX, spawnTrs.transform.position.y);
            Sun.GetComponent<sun>().SetEndPosY(endPos);
            sunTimer = 0;
        }
    }
}
