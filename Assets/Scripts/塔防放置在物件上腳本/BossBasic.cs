using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossBasic : MonsterFather
{
    public static BossBasic bossBasic;

    [Header("Boss種類")]
    public BossType bossType;
    [Header("目前是否再走路")]
    public bool isWalking;

    [Header("攻擊前的蓄力特效")]
    public GameObject chargingEffect;
    [Header("攻擊特效預置物")]
    public GameObject attackEffectsPrefab;
    [Header("召喚傳送門預置物")]
    public GameObject CallPortal;
    [Header("蓄力時間")]
    public float chargingTime = 5f;
    [Header("攻擊完畢後冷卻時間")]
    public float attackCooldownTime = 5f;
    [Header("衝刺完畢後冷卻時間")]
    public float speedCooldownTime = 10f;
    [Header("召喚完畢後冷卻時間")]
    public float callCooldownTime = 15f;

    [Header("要召喚的怪物預置物")]
    public GameObject[] callMonster = new GameObject[6];

    //攻擊冷卻
    bool isAttackCooldown;
    //衝刺冷卻
    bool isSpeedCooldown;
    //召喚敵人冷卻
    public bool isCallCooldown;


    public void Awake()
    {
        levelBasic = GameObject.Find("GM").GetComponent<LevelBasic>();
        bossBasic = this;
        Hp = data.HP;
        Speed = data.Speed;
        MonsterName = transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        MonsterHp = transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>();
        rb = GetComponent<Rigidbody>();
        myAnimator = GetComponent<Animator>();
    }
    private void Start()
    {
        isWalking = true;
        target = Waypoints.points[0]; // 目標 = 路徑點
        walking = StartCoroutine(walk());

        UpdateMonsterName();
    }
    public void Update()
    {
        #region 外掛
        if (Input.GetKeyDown(KeyCode.X))
        {
            Hp -= 5;
        }
        #endregion
        if (Hp <= 0)
        {
            Dead();
        }
        UpdateMonsterHp();
    }


    #region 攻擊協成的Coroutine
    Coroutine attack;
    Coroutine waitAattacking;
    Coroutine coolDown;
    #endregion

    #region 速度協成的Coroutine
    Coroutine speed;
    Coroutine waitSpeeding;
    Coroutine speedcoolDown;
    #endregion

    #region 防禦協成的Coroutine
    Coroutine call;
    Coroutine waitCalling;
    Coroutine callcoolDown;
    #endregion

    Coroutine walking;
    [SerializeField] public List<GameObject> ArtilleryList = new List<GameObject>();

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Artillery"))
        {
            ArtilleryList.Add(other.gameObject);
            if (!isAttackCooldown && bossType == BossType.AttackBoss)
            {
                waitAattacking = StartCoroutine(waitAttack());
                isWalking = false;
            }
            if (!isSpeedCooldown && bossType == BossType.SpeedBoss)
            {
                waitSpeeding = StartCoroutine(waitSpeed());
                isWalking = false;
            }
            if (!isCallCooldown && bossType == BossType.DefendBoss)
            {
                waitSpeeding = StartCoroutine(waitCall());
                isWalking = false;
            }

        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Artillery"))
        {
            ArtilleryList.Remove(other.gameObject);
        }
    }
    #region 攻擊型Boss用的協成
    IEnumerator Attack()
    {
        print("我正在攻擊");
        GameObject targetArtillery = ArtilleryList[0];
        GameObject attackWave = Instantiate(attackEffectsPrefab, transform.position, Quaternion.identity);
        Vector3 directionToCannon = targetArtillery.transform.position - transform.position;
        attackWave.GetComponent<Rigidbody>().velocity = directionToCannon.normalized * 0.1f;
        yield return new WaitForSeconds(1f);
        StopCoroutine(attack);
        isWalking = true;
        isAttackCooldown = true;
        ArtilleryList.Remove(targetArtillery);
        coolDown = StartCoroutine(AttackCooldown());
        walking = StartCoroutine(walk());
    }
    IEnumerator waitAttack()
    {
        StopCoroutine(walking);
        Instantiate(chargingEffect, transform.position, Quaternion.identity);
        print("我有近來等待");
        yield return new WaitForSeconds(chargingTime);
        print("我等待完畢了");
        attack = StartCoroutine(Attack());
    }
    IEnumerator AttackCooldown()
    {
        print("我正在冷卻");
        yield return new WaitForSeconds(attackCooldownTime);
        isAttackCooldown = false;
        print("我冷卻完畢了");
    }
    #endregion

    #region 速度型Boss用的協成
    IEnumerator Speeding()
    {
        Speed = 1f;
        isWalking = true;
        walking = StartCoroutine(walk());
        yield return new WaitForSeconds(1f);
        //isSpeedCooldown = true;
        speedcoolDown = StartCoroutine(SpeedCooldown());
        StopCoroutine(speed);
    }

    IEnumerator waitSpeed()
    {
        StopCoroutine(walking);
        Instantiate(chargingEffect, transform.position, Quaternion.identity);
        print("我有近來等待");
        yield return new WaitForSeconds(chargingTime);
        print("我等待完畢了");
        isSpeedCooldown = true;
        speed = StartCoroutine(Speeding());
    }
    IEnumerator SpeedCooldown()
    {
        print("我正在冷卻");
        Speed = 0.1f;
        yield return new WaitForSeconds(speedCooldownTime);
        print("我冷卻完畢了");
        isSpeedCooldown = false;
    }
    #endregion

    #region 防禦型Boss用的協成
    IEnumerator Calling()
    {
        Instantiate(CallPortal, transform.position, Quaternion.identity);
        for (int i = 0; i < 5; i++)
        {
            int randomIndex = Random.Range(0, callMonster.Length);
            GameObject randomMonsterPrefab = callMonster[randomIndex];
            Instantiate(randomMonsterPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(1f);
        isWalking = true;
        callcoolDown = StartCoroutine(CallCooldown());
        StopCoroutine(call);
    }

    IEnumerator waitCall()
    {
        StopCoroutine(walking);
        Instantiate(chargingEffect, transform.position, Quaternion.identity);
        print("我有近來等待");
        yield return new WaitForSeconds(chargingTime);
        print("我等待完畢了");
        isCallCooldown = true;
        call = StartCoroutine(Calling());
    }
    IEnumerator CallCooldown()
    {
        print("我正在冷卻");
        walking = StartCoroutine(walk());
        yield return new WaitForSeconds(callCooldownTime);
        print("我冷卻完畢了");
        isCallCooldown = false;
    }
    #endregion

    IEnumerator walk()
    {

        while (isWalking)
        {
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * Speed * Time.deltaTime, Space.World); // 移動 要改速度改speed即可

            if (Vector3.Distance(transform.position, target.position) <= 0.002f) // 換下一個路徑點
            {
                GetNextWaypoint();
            }
            yield return new WaitForSeconds(0);
        }
    }
    #region 城牆碰撞
    public void OnCollisionEnter(Collision other) // 碰撞
    {
        Debug.Log("觸發碰撞事件");
    }
    public void OnCollisionStay(Collision other) // 碰撞
    {

        AttackingTime -= Time.deltaTime;
        if (AttackingTime <= 0)
        {
            switch (other.gameObject.tag)
            {
                case "Wall_Burn":
                    other.gameObject.GetComponent<Wall>().HP -= 1;
                    Hp -= 10;
                    gameObject.GetComponent<MeshRenderer>().material.color = new Color(1, 0.3f, 0.3f, 1);
                    colorchanged = true;
                    AttackingTime = 1;
                    if (other.gameObject == null)
                    {
                        AttackingTime = 1;
                    }
                    break;
                case "Wall_Poison":
                    isPoisoned = true;
                    PoisonCount = 5;
                    other.gameObject.GetComponent<Wall>().HP -= 1;
                    AttackingTime = 1;
                    break;
                case "Wall_Normal":
                    other.gameObject.GetComponent<Wall>().HP -= 1;
                    AttackingTime = 1;
                    break;
            }
            AttackingTime = 1;
            myAnimator.SetBool("Attack", false);

        }
        if (other.gameObject.GetComponent<Wall>().HP <= 0)
        {
            myAnimator.SetBool("Attack", false);
        }
    }
    public void OnCollisionExit(Collision other)
    {
        AttackingTime = 1;
        myAnimator.SetBool("Attack", false);
    }
    #endregion
}
