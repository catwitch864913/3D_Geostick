using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargBasic : MonoBehaviour
{
    [Header("時間++")]
    public float Timer;
    [Header("刪除所需要時間")]
    public float DestroyTime=5f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer >= DestroyTime)
        {
            Destroy(gameObject);
        }   
    }
}
