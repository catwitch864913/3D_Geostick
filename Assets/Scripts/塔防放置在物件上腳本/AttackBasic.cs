using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class AttackBasic : MonoBehaviour
{
    
    public GameObject target;
    private void Start()
    {
        target = BossBasic.bossBasic.ArtilleryList[0];
    }
    private void Update()
    {
        gameObject.transform.LookAt(target.transform.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Artillery"))
        {
            Destroy(other.gameObject); // 刪除Artillery物件
            Destroy(gameObject); // 刪除武器物件
        }
    }
}
