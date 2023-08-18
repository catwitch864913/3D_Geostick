using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallNormal : MonoBehaviour
{
    public int HP = 5;

    void Update()
    {
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
