using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tiles : MonoBehaviour
{
    //����ȭ �����ʿ�
    [SerializeField]private bool useTile;
    public bool GetUseTileCheck { get { return useTile; } }

    public bool SetUseTileCheck { set { useTile = value; } }


}
