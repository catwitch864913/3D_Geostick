using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBurn : MonoBehaviour
{
    public int HP = 1;

    void Update()
    {
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
