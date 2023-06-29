using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int[] isFull = new int[4];
    public GameObject[] slots;

    public void init()
    {
        for (int i = 0; i<4;i++)
        {
            isFull[i] = 0;
        }
    }

}
