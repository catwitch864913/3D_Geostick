using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPoison : MonoBehaviour
{
    public int HP = 2;

    void Update()
    {
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
