using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelBasic : MonoBehaviour
{
    // [Header("重製關卡名字")]
    // public string LevelName;
    #region 抓取檔案以及要被繼承的資料
    [Header("關卡資料結構")]
    public LevelData LevelData;

    [Header("要被資料繼承的怪物總數量")]//測試完畢後可以將要繼承的東西全部改成私有private
    public int EnemyQuantity;
    [Header("要繼承出現正常型態怪物數量")]
    public int NormalEnemy;
    [Header("要繼承出現速度型態怪物數量")]
    public int SpeedEnemy;
    [Header("要繼承出現防禦型態怪物數量")]
    public int DefendEnemy;
    [Header("要繼承出現菁英正常型態怪物數量")]
    public int NormalElite;
    [Header("該關卡出現菁英速度型態怪物數量")]
    public int SpeedElite;
    [Header("該關卡出現菁英防禦型態怪物數量")]
    public int DefendElite;
    [Header("該關卡出現的小Boss數量")]
    public int AttackLittleBoss, SpeedLittleBoss, DefendLittleBoss;
    [Header("該關卡出現的Boss")]
    public GameObject Boss;
    [Header("關卡波數")]
    public int wave;
    [Header("波數怪物數量陣列")]
    public int[] EnemyWave;
    [Header("關卡核心血量")]
    public int CoreHP;
    [Header("關卡金幣")]
    public int Coin;
    #endregion
    #region UI物件
    [Header("UI時間倒數計時器")]
    public TextMeshProUGUI TimerText;
    [Header("UI波數說明")]
    public TextMeshProUGUI WaveText;
    [Header("UI金幣")]
    public TextMeshProUGUI GoldCoinWallet;
    [Header("UI核心血量")]
    public TextMeshProUGUI CoreHPUI;
    [Header("UI過關視窗")]
    public GameObject WinPanel;
    [Header("UI失敗視窗")]
    public GameObject LoserPanel;
    [Header("暫停視窗")]
    public GameObject StopPanel;
    [Header("1開始按鈕,2直接下一波按鈕,3暫停按鈕")]
    public Button StartButton;
    public Button NextWaveButton;
    public Button StopButton;
    [Header("抓取重進遊戲的按鈕")]
    public Button[] ResetButton;
    public GameObject[] buttonObjects;
    [Header("抓取回到首頁的按鈕")]
    public Button[] MenuButton;
    public GameObject[] ObjMenuButton;
    [Header("按鈕物件用來隱藏")]
    public GameObject ObjStartButton;
    public GameObject ObjNextWaveButton;
    //public GameObject ObjStopButton;
    #endregion
    #region 怪物預置物，以及設置怪物生成點
    // 0 正常型態怪物
    // 1 速度型態怪物
    // 2 防禦型態怪物
    // 3 菁英正常型態怪物
    // 4 菁英速度型態怪物
    // 5 菁英防禦型態怪物
    // 6 攻擊型態小Boss    此處需考慮小Boss出現方式是否跟Boss一樣
    // 7 速度型態小Boss
    // 8 防禦型態小Boss
    // Boss寫在上方的繼承資料內
    [Header("怪物預置物陣列，腳本內有詳細順序，共需要9格")]
    public GameObject[] Monsters = new GameObject[9];

    [Header("怪物生成點")]
    public GameObject MonsterSpawnPoint;
    #endregion
    #region 核心、砲台、城牆
    public GameObject Core;
    #endregion


    float countdownTime = 10f; // 倒數計時的秒數，這裡設定為 10秒
    bool countdownStart; //倒數計時開始布林
    private void Awake()
    {
        //初始化遊戲時間
        Time.timeScale = 1f;
        #region 該處是繼承資料
        this.EnemyQuantity = LevelData.EnemyQuantity;
        this.NormalEnemy = LevelData.NormalEnemy;
        this.SpeedEnemy = LevelData.SpeedEnemy;
        this.DefendEnemy = LevelData.DefendEnemy;
        this.NormalElite = LevelData.NormalElite;
        this.SpeedElite = LevelData.SpeedElite;
        this.DefendElite = LevelData.DefendElite;
        this.AttackLittleBoss = LevelData.AttackLittleBoss;
        this.SpeedLittleBoss = LevelData.SpeedLittleBoss;
        this.DefendLittleBoss = LevelData.DefendLittleBoss;
        this.Boss = LevelData.Boss;
        this.wave = LevelData.wave;
        this.EnemyWave = LevelData.EnemyWave;
        this.CoreHP = LevelData.CoreHP;
        this.Coin = LevelData.Coin;
        UpdateCoinWallet();
        UpdateCoreHP();
        #endregion

        #region UI按鈕、介面 靠程式抓取   這個需要全部開啟才能被抓取，如果關閉會抓取不到
        WinPanel = GameObject.Find("WinPanel");
        LoserPanel = GameObject.Find("LoserPanel");
        StopPanel = GameObject.Find("StopPanel");
        ObjStartButton = GameObject.Find("StartButton");
        ObjNextWaveButton = GameObject.Find("NextWaveButton");
        //ObjStopButton = GameObject.Find("StopButton");
        StartButton = GameObject.Find("StartButton").GetComponent<Button>();
        NextWaveButton = GameObject.Find("NextWaveButton").GetComponent<Button>();
        StopButton = GameObject.Find("StopButton").GetComponent<Button>();
        buttonObjects = GameObject.FindGameObjectsWithTag("ReLevelButton");
        ResetButton = new Button[buttonObjects.Length];
        for (int i = 0; i < buttonObjects.Length; i++)
        {
            ResetButton[i] = buttonObjects[i].GetComponent<Button>();
        }
        ObjMenuButton = GameObject.FindGameObjectsWithTag("MenuButton");
        MenuButton = new Button[ObjMenuButton.Length];
        for (int i = 0; i < ObjMenuButton.Length; i++)
        {
            MenuButton[i] = ObjMenuButton[i].GetComponent<Button>();
        }

        #endregion

        #region 陣列抓取敵人數量、設定怪物重生點
        MonsterSpawnPoint = GameObject.Find("MonsterSpawnPos");
        // 先將可用的怪物類型加入到 availableEnemies 中
        remainingEnemies = new int[6];
        remainingEnemies[0] = this.NormalEnemy;
        remainingEnemies[1] = this.SpeedEnemy;
        remainingEnemies[2] = this.DefendEnemy;
        remainingEnemies[3] = this.NormalElite;
        remainingEnemies[4] = this.SpeedElite;
        remainingEnemies[5] = this.DefendElite;
        if (NormalEnemy > 0) availableEnemies.Add(0);
        if (SpeedEnemy > 0) availableEnemies.Add(1);
        if (DefendEnemy > 0) availableEnemies.Add(2);
        if (NormalElite > 0) availableEnemies.Add(3);
        if (SpeedElite > 0) availableEnemies.Add(4);
        if (DefendElite > 0) availableEnemies.Add(5);
        #endregion

    }
    void Start()
    {
        #region UI按鈕、介面抓取後部分隱藏，部分抓取腳本
        WinPanel.GetComponent<CanvasGroup>().alpha = 1;
        LoserPanel.GetComponent<CanvasGroup>().alpha = 1;
        StopPanel.GetComponent<CanvasGroup>().alpha = 1;
        WinPanel.SetActive(false);
        LoserPanel.SetActive(false);
        StopPanel.SetActive(false);
        ObjNextWaveButton.SetActive(false);
        StartButton.onClick.AddListener(StartGame);
        NextWaveButton.onClick.AddListener(ClickNextWave);
        StopButton.onClick.AddListener(UpdateStopPanel);
        for (int i = 0; i < ResetButton.Length; i++)
        {
            ResetButton[i].onClick.AddListener(ResetLevel) ;
        }
        for (int i = 0; i < MenuButton.Length; i++)
        {
            MenuButton[i].onClick.AddListener(ToMenu);
        }
        #endregion
    }

    void Update()
    {
        //Debug.LogWarning(countdownTime);
        #region 外掛
        if (Input.GetKeyDown(KeyCode.Z))
        {
            CoreHP = 1000;
            UpdateCoreHP();
        }
        #endregion
        if (countdownStart)
        {
            TimerText.gameObject.SetActive(true);
            int seconds;
            seconds = Mathf.FloorToInt(countdownTime % 60f);
            countdownTime -= Time.deltaTime;
            // 將秒數轉換為字串形式，並顯示在 UI 上
            TimerText.text = "下一波時間倒數：" + string.Format("{0:00}", seconds);
        }
        else
        {
            TimerText.gameObject.SetActive(false);
        }

        if (CoreHP <= 0)
        {
            LoserPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
    [Header("目前第幾波")]
    public int currentWave = 0; // 目前的波數索引
    [Header("每種怪物剩餘的數量")]
    public int[] remainingEnemies;   // 儲存每種怪物剩餘的數量

    [Header("當每種怪物數量>0時該處便會擁有陣列")]
    public List<int> availableEnemies = new List<int>();
    // 呼叫此方法生成下一波怪物
    public IEnumerator SpawnNextWave()
    {
        if (currentWave < wave)
        {
            int totalEnemies = EnemyWave[currentWave];
            // 隨機抽取怪物類型並生成
            for (int i = 0; i < totalEnemies; i++)
            {
                if (availableEnemies.Count > 0)
                {
                    int randomIndex = Random.Range(0, availableEnemies.Count);
                    int enemyType = availableEnemies[randomIndex];

                    switch (enemyType)
                    {
                        case 0:
                            // 生成正常型態怪物
                            Instantiate(Monsters[0], MonsterSpawnPoint.transform.position, Quaternion.identity);
                            // 減去剩餘數量
                            remainingEnemies[0]--;
                            break;
                        case 1:
                            // 生成速度型態怪物
                            Instantiate(Monsters[1], MonsterSpawnPoint.transform.position, Quaternion.identity);
                            // 減去剩餘數量
                            remainingEnemies[1]--;
                            break;
                        case 2:
                            // 生成防禦型態怪物
                            Instantiate(Monsters[2], MonsterSpawnPoint.transform.position, Quaternion.identity);
                            // 減去剩餘數量
                            remainingEnemies[2]--;
                            break;
                        case 3:
                            // 生成正常型態的菁英怪物
                            Instantiate(Monsters[3], MonsterSpawnPoint.transform.position, Quaternion.identity);
                            // 減去剩餘數量
                            remainingEnemies[3]--;
                            break;
                        case 4:
                            // 生成速度型態怪物
                            Instantiate(Monsters[4], MonsterSpawnPoint.transform.position, Quaternion.identity);
                            // 減去剩餘數量
                            remainingEnemies[4]--;
                            break;
                        case 5:
                            // 生成防禦型態怪物
                            Instantiate(Monsters[5], MonsterSpawnPoint.transform.position, Quaternion.identity);
                            // 減去剩餘數量
                            remainingEnemies[5]--;
                            break;
                    }


                    // 如果該類型的怪物剩餘數量為0，從可用怪物列表中移除
                    if (remainingEnemies[enemyType] == 0)
                    {
                        availableEnemies.Remove(enemyType);
                    }
                    yield return new WaitForSeconds(1f);
                }
                else
                {
                    print("我因該跑步到這裡，我在這除錯用");
                }
            }
            if (availableEnemies.Count != 0)
            {
                print("怪物還有你還可以在執行");
                WaveCompleted();
                currentWave++;
                //UpdateWave();
            }
            else
            {
                print("我要生成Boss");
                Instantiate(Boss, MonsterSpawnPoint.transform.position, Quaternion.identity);

            }
        }
    }
    Coroutine NextCoroutine;
    //Coroutine WaveWaitCoroutine;
    // 呼叫此方法開始遊戲
    public void StartGame()
    {
        //currentWave = 0;
        UpdateWave();
        NextCoroutine = StartCoroutine(SpawnNextWave());
        //a =StartCoroutine(我是測試協成());
        ObjStartButton.SetActive(false);
        ObjNextWaveButton.SetActive(false);
        Time.timeScale = 1f;
    }
    #region 測試協成Bug用
    /*Coroutine a;
    
    IEnumerator 我是測試協成()
    {
        while (true)
        {
            print("我還在");
            yield return new WaitForSeconds(1f);
        }
    }
    
    public void 我要暫停協成()
    {
        StopCoroutine(a);
        //StopCoroutine("我是測試協成");
        print("我是暫停協成 我有備案到");
    }*/
    #endregion

    // 呼叫此方法在怪物生成後，減去生成的數量，如果某種怪物的數量已經為0，表示不再生成此種怪物
    public void ReduceRemainingEnemies(int normal, int speed, int defend, int normalElite, int speedElite, int defendElite)
    {
        remainingEnemies[0] -= normal;
        remainingEnemies[1] -= speed;
        remainingEnemies[2] -= defend;
        remainingEnemies[3] -= normalElite;
        remainingEnemies[4] -= speedElite;
        remainingEnemies[5] -= defendElite;
    }

    // 呼叫此方法判斷是否還有剩餘怪物可以生成
    public bool HasRemainingEnemies()
    {
        foreach (int count in remainingEnemies)
        {
            if (count > 0)
            {
                return true;
            }
        }

        return false;
    }
    // 呼叫此方法在下一波的怪物數量為0時進行等待，然後生成下一波怪物
    public void WaveCompleted()
    {
        StartCoroutine(WaitAndSpawnNextWave());
    }


    // 等待一段時間後生成下一波怪物
    private IEnumerator WaitAndSpawnNextWave()
    {
        if (countdownTime > 0)
        {
            countdownStart = true;
            ObjNextWaveButton.SetActive(true);
        }
        yield return new WaitForSeconds(10f); // 等待10秒，你可以自行調整等待時間
        // 停止 SpawnNextWave 協程（如果在這期間已經啟動）
        StopCoroutine(NextCoroutine);
        NextCoroutine = StartCoroutine(SpawnNextWave());
        countdownTime = 10;
        countdownStart = false;
        ObjNextWaveButton.SetActive(false);
        UpdateWave();
    }
    ///更新金幣
    public void UpdateCoinWallet()
    {
        GoldCoinWallet.text = "金幣：" + Coin.ToString();
    }
    public void UpdateWave()
    {
        WaveText.text = "波數：" + (currentWave + 1) + "/" + wave;
    }
    public void UpdateCoreHP()
    {
        CoreHPUI.text = "核心血量：" + CoreHP.ToString();
    }

    bool OpenStopPanel = false;
    public void UpdateStopPanel()
    {
        OpenStopPanel = !OpenStopPanel;
        StopPanel.SetActive(OpenStopPanel);
        if (OpenStopPanel)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void ClickNextWave()
    {
        print("開始下一波");
        //暫停倒數下一波協成
        StopCoroutine(NextCoroutine);
        //print(StopCoroutine(WaitAndSpawnNextWave()));
        NextCoroutine = StartCoroutine(SpawnNextWave());
        UpdateWave();
        countdownTime = 10;
        countdownStart = false;
        ObjNextWaveButton.SetActive(false);
    }

    public void ResetLevel()
    {
        //Time.timeScale放在此處無效
        //Time.timeScale = 1f;
        //此處要算好前面有幾個場景改變後面的-1，如果該關卡是level4，前方還有3個關卡+1個首頁時 只需要-掉首頁
        SceneManager.LoadScene(LevelData.Level - 1);
    }
    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }


    //GM不可以寫這個 該段須寫在核心或是怪物腳本身上
    /*private void OnTriggerEnter(Collider other)
    {
        print(other);
        /*if (other.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            Collider coreCollider = GetComponent<Collider>();
            if (coreCollider == other)
            {
                CoreHP--;
                UpdateCoreHP();
            }
        }
        if (other.CompareTag("Monster"))
        {
            CoreHP--;

            UpdateCoreHP();
        }
    }*/









}
