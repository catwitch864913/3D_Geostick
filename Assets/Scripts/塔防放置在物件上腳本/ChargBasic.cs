using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargBasic : MonoBehaviour
{
    [Header("�ɶ�++")]
    public float Timer;
    [Header("�R���һݭn�ɶ�")]
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
