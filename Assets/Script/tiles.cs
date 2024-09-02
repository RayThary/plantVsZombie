using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tiles : MonoBehaviour
{
    private bool useTile;
    public bool GetUseTileCheck { get { return useTile; } }

    public bool SetUseTileCheck { set { useTile = value; } }
    private bool tileOnMonster = false;

    public bool GetTileOnMonster {  get { return tileOnMonster; } }
    private BoxCollider2D box2d;
    private void Start()
    {
        box2d = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (box2d.IsTouchingLayers(LayerMask.GetMask("Monster")))
        {
            tileOnMonster = true;
        }
        else
        {
            tileOnMonster = false;
        }
    }

}
