using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunManager : MonoBehaviour
{
    [SerializeField] private GameObject sunObj;
    [SerializeField] private float sunMaxSpawnDealy = 4;
    [SerializeField] private float sunMinSpawnDealy = 4;
    private float sunSpawnDealy;
    private Transform spawnTrs;
    private float sunTimer = 0.0f;
    void Start()
    {
        spawnTrs = transform.GetChild(0);
        sunSpawnDealy = Random.Range(sunMinSpawnDealy, sunMaxSpawnDealy);
    }

    // Update is called once per frame
    void Update()
    {
        createSun();
        clickSun();
    }

    private void createSun()
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
            sunSpawnDealy = Random.Range(sunMinSpawnDealy, sunMaxSpawnDealy);
        }
    }

    private void clickSun()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity);

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Sun"))
                {
                    sun _sun = hit.transform.GetComponent<sun>();
                    _sun.DestroySun();
                }
            }
        }
    }
}
