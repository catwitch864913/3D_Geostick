using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public WallType wallType;
    public int HP;
    private void Start()
    {
        if (wallType == WallType.Wall_Burn)
        {
            HP = 1;
        }
        else if (wallType == WallType.Wall_Normal)
        {
            HP = 5;
        }
        else if (wallType == WallType.Wall_Poison)
        {
            HP = 2;
        }
        else
        {
            print("§Ú¨S§ì¨ì«°ÀðType");
        }

    }
    void Update()
    {
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
