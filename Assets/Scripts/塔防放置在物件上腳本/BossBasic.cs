using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossBasic : MonsterFather
{
    public static BossBasic bossBasic;

    [Header("Boss����")]
    public BossType bossType;
    [Header("�ثe�O�_�A����")]
    public bool isWalking;

    [Header("�����e���W�O�S��")]
    public GameObject chargingEffect;
    [Header("�����S�Ĺw�m��")]
    public GameObject attackEffectsPrefab;
    [Header("�l��ǰe���w�m��")]
    public GameObject CallPortal;
    [Header("�W�O�ɶ�")]
    public float chargingTime = 5f;
    [Header("����������N�o�ɶ�")]
    public float attackCooldownTime = 5f;
    [Header("�Ĩ맹����N�o�ɶ�")]
    public float speedCooldownTime = 10f;
    [Header("�l�꧹����N�o�ɶ�")]
    public float callCooldownTime = 15f;

    [Header("�n�l�ꪺ�Ǫ��w�m��")]
    public GameObject[] callMonster = new GameObject[6];

    //�����N�o
    bool isAttackCooldown;
    //�Ĩ�N�o
    bool isSpeedCooldown;
    //�l��ĤH�N�o
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
        target = Waypoints.points[0]; // �ؼ� = ���|�I
        walking = StartCoroutine(walk());

        UpdateMonsterName();
    }
    public void Update()
    {
        #region �~��
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


    #region �����󦨪�Coroutine
    Coroutine attack;
    Coroutine waitAattacking;
    Coroutine coolDown;
    #endregion

    #region �t�ר󦨪�Coroutine
    Coroutine speed;
    Coroutine waitSpeeding;
    Coroutine speedcoolDown;
    #endregion

    #region ���m�󦨪�Coroutine
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
    #region ������Boss�Ϊ���
    IEnumerator Attack()
    {
        print("�ڥ��b����");
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
        print("�ڦ���ӵ���");
        yield return new WaitForSeconds(chargingTime);
        print("�ڵ��ݧ����F");
        attack = StartCoroutine(Attack());
    }
    IEnumerator AttackCooldown()
    {
        print("�ڥ��b�N�o");
        yield return new WaitForSeconds(attackCooldownTime);
        isAttackCooldown = false;
        print("�ڧN�o�����F");
    }
    #endregion

    #region �t�׫�Boss�Ϊ���
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
        print("�ڦ���ӵ���");
        yield return new WaitForSeconds(chargingTime);
        print("�ڵ��ݧ����F");
        isSpeedCooldown = true;
        speed = StartCoroutine(Speeding());
    }
    IEnumerator SpeedCooldown()
    {
        print("�ڥ��b�N�o");
        Speed = 0.1f;
        yield return new WaitForSeconds(speedCooldownTime);
        print("�ڧN�o�����F");
        isSpeedCooldown = false;
    }
    #endregion

    #region ���m��Boss�Ϊ���
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
        print("�ڦ���ӵ���");
        yield return new WaitForSeconds(chargingTime);
        print("�ڵ��ݧ����F");
        isCallCooldown = true;
        call = StartCoroutine(Calling());
    }
    IEnumerator CallCooldown()
    {
        print("�ڥ��b�N�o");
        walking = StartCoroutine(walk());
        yield return new WaitForSeconds(callCooldownTime);
        print("�ڧN�o�����F");
        isCallCooldown = false;
    }
    #endregion

    IEnumerator walk()
    {

        while (isWalking)
        {
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * Speed * Time.deltaTime, Space.World); // ���� �n��t�ק�speed�Y�i

            if (Vector3.Distance(transform.position, target.position) <= 0.002f) // ���U�@�Ӹ��|�I
            {
                GetNextWaypoint();
            }
            yield return new WaitForSeconds(0);
        }
    }
    #region ����I��
    public void OnCollisionEnter(Collision other) // �I��
    {
        Debug.Log("Ĳ�o�I���ƥ�");
    }
    public void OnCollisionStay(Collision other) // �I��
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
