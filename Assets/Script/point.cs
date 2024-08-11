using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class point : MonoBehaviour
{
    private TextMeshProUGUI text;
    private float nowPoint;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        nowPoint = GameManager.instance.GetPoint;
        text.text = nowPoint.ToString();
    }
}
