using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class BossSpawnData
{
    public int waveNumber; // 特定波數
    public GameObject bossPrefab; // 小Boss預製件
}



public class LevelBasic : MonoBehaviour
{
    public bool spawnedBoss;
    public BossSpawnData[] bossSpawnData;
    #region 抓取檔案以及要被繼承的資料
    [Header("關卡資料結構")]
    public LevelData LevelData;

    [Header("要被資料繼承的怪物總數量")]//測試完畢後可以將要繼承的東西全部改成私有private
    private int EnemyQuantity;
    [Header("要繼承出現正常型態怪物數量")]
    private int NormalEnemy;
    [Header("要繼承出現速度型態怪物數量")]
    private int SpeedEnemy;
    [Header("要繼承出現防禦型態怪物數量")]
    private int DefendEnemy;
    [Header("要繼承出現菁英正常型態怪物數量")]
    private int NormalElite;
    [Header("該關卡出現菁英速度型態怪物數量")]
    private int SpeedElite;
    [Header("該關卡出現菁英防禦型態怪物數量")]
    private int DefendElite;
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
    private TextMeshProUGUI TimerText;
    [Header("UI波數說明")]
    private TextMeshProUGUI WaveText;
    [Header("UI金幣")]
    private TextMeshProUGUI GoldCoinWallet;
    [Header("UI核心血量")]
    private TextMeshProUGUI CoreHPUI;
    [Header("UI過關視窗")]
    [HideInInspector]
    public GameObject WinPanel;
    [Header("UI失敗視窗")]
    private GameObject LoserPanel;
    [Header("暫停視窗")]
    private GameObject StopPanel;
    [Header("1開始按鈕,2直接下一波按鈕,3暫停按鈕,4暫停介面中關閉暫停按鈕")]
    private Button StartButton;
    private Button NextWaveButton;
    private Button StopButton;
    private Button CloseButton;
    [Header("抓取重進遊戲的按鈕")]
    private Button[] ResetButton;
    private GameObject[] buttonObjects;
    [Header("抓取回到首頁的按鈕")]
    private Button[] MenuButton;
    private GameObject[] ObjMenuButton;
    [Header("按鈕物件用來隱藏")]
    private GameObject ObjStartButton;
    private GameObject ObjNextWaveButton;
    //public GameObject ObjStopButton;
    #endregion
    #region 怪物預置物，以及設置怪物生成點
    // 0 正常型態怪物
    // 1 速度型態怪物
    // 2 防禦型態怪物
    // 3 菁英正常型態怪物
    // 4 菁英速度型態怪物
    // 5 菁英防禦型態怪物
    //小Boss與Boss另外寫法
    [Header("怪物預置物陣列，腳本內有詳細順序，共需要6格，不包含小Boss")]
    public GameObject[] Monsters = new GameObject[6];

    [Header("怪物生成點，如果該關卡只有1個重生點複製同樣位置，改名就好")]
    public GameObject MonsterSpawnPoint1, MonsterSpawnPoint2, MonsterSpawnPoint3;
    #endregion
    #region 核心、砲台、城牆
    //public GameObject Core;
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
        this.Boss = LevelData.Boss;
        this.wave = LevelData.wave;
        this.EnemyWave = LevelData.EnemyWave;
        this.CoreHP = LevelData.CoreHP;
        this.Coin = LevelData.Coin;
        #endregion

        #region UI按鈕、介面 靠程式抓取   這個需要全部開啟才能被抓取，如果關閉會抓取不到
        TimerText = GameObject.Find("CountDownTime").GetComponent<TextMeshProUGUI>();
        WaveText = GameObject.Find("Wave").GetComponent<TextMeshProUGUI>();
        GoldCoinWallet = GameObject.Find("GoldCoin").GetComponent<TextMeshProUGUI>();
        CoreHPUI = GameObject.Find("CoreHP").GetComponent<TextMeshProUGUI>();
        WinPanel = GameObject.Find("WinPanel");
        LoserPanel = GameObject.Find("LoserPanel");
        StopPanel = GameObject.Find("StopPanel");
        ObjStartButton = GameObject.Find("StartButton");
        ObjNextWaveButton = GameObject.Find("NextWaveButton");
        //ObjStopButton = GameObject.Find("StopButton");
        StartButton = GameObject.Find("StartButton").GetComponent<Button>();
        NextWaveButton = GameObject.Find("NextWaveButton").GetComponent<Button>();
        StopButton = GameObject.Find("StopButton").GetComponent<Button>();
        CloseButton = GameObject.Find("Close_Button").GetComponent<Button>();
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
        MonsterSpawnPoint1 = GameObject.Find("MonsterSpawnPos1");
        MonsterSpawnPoint2 = GameObject.Find("MonsterSpawnPos2");
        MonsterSpawnPoint3 = GameObject.Find("MonsterSpawnPos3");
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
        StopButton.onClick.AddListener(ClickStopButtom);
        CloseButton.onClick.AddListener(ClickStopButtom);
        for (int i = 0; i < ResetButton.Length; i++)
        {
            ResetButton[i].onClick.AddListener(ResetLevel);
        }
        for (int i = 0; i < MenuButton.Length; i++)
        {
            MenuButton[i].onClick.AddListener(ToMenu);
        }
        #endregion
        UpdateCoinWallet();
        UpdateCoreHP();
    }

    void Update()
    {
        #region 外掛
        if (Input.GetKeyDown(KeyCode.Z))
        {
            CoreHP = 1000;
            UpdateCoreHP();
        }
        #endregion
        #region 波數倒數計時器
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
        #endregion
        #region 核心血量判斷
        if (CoreHP <= 0)
        {
            LoserPanel.SetActive(true);
            Time.timeScale = 0;
        }
        #endregion
    }
    [Header("目前第幾波")]
    public int currentWave = 0; // 目前的波數索引
    [Header("每種怪物剩餘的數量")]
    public int[] remainingEnemies;   // 儲存每種怪物剩餘的數量

    [Header("當每種怪物數量>0時該處便會擁有陣列")]
    public List<int> availableEnemies = new List<int>();

    public float[] enemySpawnProbabilities;

    // 呼叫此方法生成下一波怪物
    public IEnumerator SpawnNextWave()
    {
        /*if (currentWave < wave)
        {
            int totalEnemies = EnemyWave[currentWave];
            // 隨機抽取怪物類型並生成
            for (int i = 0; i < totalEnemies; i++)
            {
                if (availableEnemies.Count > 0)
                {
                    int randomIndex = Random.Range(0, availableEnemies.Count);
                    int enemyType = availableEnemies[randomIndex];
                    
                    Vector3 spawnPosition;
                    int spawnPointIndex = Random.Range(0, 3);
                    switch (spawnPointIndex)
                    {
                        case 0:
                            spawnPosition = MonsterSpawnPoint1.transform.position;
                            break;
                        case 1:
                            spawnPosition = MonsterSpawnPoint2.transform.position;
                            break;
                        case 2:
                            spawnPosition = MonsterSpawnPoint3.transform.position;
                            break;
                        default:
                            spawnPosition = Vector3.zero; // 預設位置
                            break;
                    }
                    switch (enemyType)
                    {
                        case 0:
                            // 生成正常型態怪物
                            Instantiate(Monsters[0], spawnPosition, Quaternion.identity);
                            // 減去剩餘數量
                            remainingEnemies[0]--;
                            break;
                        case 1:
                            // 生成速度型態怪物
                            Instantiate(Monsters[1], spawnPosition, Quaternion.identity);
                            // 減去剩餘數量
                            remainingEnemies[1]--;
                            break;
                        case 2:
                            // 生成防禦型態怪物
                            Instantiate(Monsters[2], spawnPosition, Quaternion.identity);
                            // 減去剩餘數量
                            remainingEnemies[2]--;
                            break;
                        case 3:
                            // 生成正常型態的菁英怪物
                            Instantiate(Monsters[3], spawnPosition, Quaternion.identity);
                            // 減去剩餘數量
                            remainingEnemies[3]--;
                            break;
                        case 4:
                            // 生成速度型態怪物
                            Instantiate(Monsters[4], spawnPosition, Quaternion.identity);
                            // 減去剩餘數量
                            remainingEnemies[4]--;
                            break;
                        case 5:
                            // 生成防禦型態怪物
                            Instantiate(Monsters[5], spawnPosition, Quaternion.identity);
                            // 減去剩餘數量
                            remainingEnemies[5]--;
                            break;
                    }
                    if (ShouldSpawnBoss(currentWave) && !spawnedBoss) // 檢查是否需要生成小Boss
                    {
                        Debug.LogWarning("我有生成小Boss喔");
                        GameObject bossPrefab = GetBossPrefabForWave(currentWave);
                        Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
                        spawnedBoss = true;
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
                    print("我應該跑不到這裡，我在這除錯用");
                }
            }
            if (availableEnemies.Count != 0)
            {
                print("怪物還有你還可以在執行");
                WaveCompleted();
                currentWave++;
                // 在生成完怪物後，將 spawnedBoss 設為 false
                spawnedBoss = false;
                //UpdateWave();
            }
            else
            {
                // 隨機選擇一個生成位置，可以是兩個點位中的一個
                Vector3 spawnPosition = Random.Range(0, 2) == 0 ? MonsterSpawnPoint1.transform.position : MonsterSpawnPoint2.transform.position;
                print("我要生成Boss");
                Instantiate(Boss, spawnPosition, Quaternion.identity);
                print(spawnPosition);
            }
        }*/

        
        if (currentWave < wave)
        {
            int totalEnemies = EnemyWave[currentWave];

            // 計算每個敵人類型的生成機率
            enemySpawnProbabilities = new float[remainingEnemies.Length];
            float totalProbability = 0;

            for (int i = 0; i < remainingEnemies.Length; i++)
            {
                totalProbability += remainingEnemies[i];
                enemySpawnProbabilities[i] = totalProbability;
            }

            // 隨機抽取怪物類型並生成
            for (int i = 0; i < totalEnemies; i++)
            {
                if (availableEnemies.Count > 0)
                {
                    Vector3 spawnPosition;
                    int spawnPointIndex = Random.Range(0, 3);
                    switch (spawnPointIndex)
                    {
                        case 0:
                            spawnPosition = MonsterSpawnPoint1.transform.position;
                            break;
                        case 1:
                            spawnPosition = MonsterSpawnPoint2.transform.position;
                            break;
                        case 2:
                            spawnPosition = MonsterSpawnPoint3.transform.position;
                            break;
                        default:
                            spawnPosition = Vector3.zero; // 預設位置
                            break;
                    }
                    // 隨機生成機率值，決定生成哪個怪物
                    float randomProbability = Random.Range(0, totalProbability);
                    int enemyType = 0;

                    for (int j = 0; j < enemySpawnProbabilities.Length; j++)
                    {
                        if (randomProbability < enemySpawnProbabilities[j])
                        {
                            enemyType = j;
                            break;
                        }
                    }

                    // 根據生成的怪物類型生成怪物
                    Instantiate(Monsters[enemyType], spawnPosition, Quaternion.identity);
                    remainingEnemies[enemyType]--;

                    // 更新機率總和
                    totalProbability = 0;
                    for (int j = 0; j < remainingEnemies.Length; j++)
                    {
                        totalProbability += remainingEnemies[j];
                        enemySpawnProbabilities[j] = totalProbability;
                    }
                    if (ShouldSpawnBoss(currentWave) && !spawnedBoss) // 檢查是否需要生成小Boss
                    {
                        Debug.LogWarning("我有生成小Boss喔");
                        GameObject bossPrefab = GetBossPrefabForWave(currentWave);
                        Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
                        spawnedBoss = true;
                    }

                    // 如果該類型的怪物剩餘數量為0，從可用怪物列表中移除
                    if (remainingEnemies[enemyType] == 0)
                    {
                        availableEnemies.Remove(enemyType);
                    }
                    yield return new WaitForSeconds(0.5f);
                }
                else
                {
                    print("我應該跑不到這裡，我在這除錯用");
                }
            }

            if (availableEnemies.Count != 0)
            {
                print("怪物還有你還可以在執行");
                WaveCompleted();
                currentWave++;
                // 在生成完怪物後，將 spawnedBoss 設為 false
                spawnedBoss = false;
                //UpdateWave();
            }
            else
            {
                // 隨機選擇一個生成位置，可以是兩個點位中的一個
                Vector3 spawnPosition = Random.Range(0, 2) == 0 ? MonsterSpawnPoint1.transform.position : MonsterSpawnPoint2.transform.position;
                print("我要生成Boss");
                Instantiate(Boss, spawnPosition, Quaternion.identity);
                print(spawnPosition);
            }
        }
    }
    Coroutine NextCoroutine;
    Coroutine WaveWaitCoroutine;
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
    #region 測試協成DeBug用
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
        WaveWaitCoroutine= StartCoroutine(WaitAndSpawnNextWave());
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


    #region 判斷生成小Boss
    private bool ShouldSpawnBoss(int waveNumber)
    {
        foreach (var bossData in bossSpawnData)
        {
            if (bossData.waveNumber == waveNumber)
            {
                return true;
            }
        }
        return false;
    }
    private GameObject GetBossPrefabForWave(int waveNumber)
    {
        foreach (var bossData in bossSpawnData)
        {
            if (bossData.waveNumber == waveNumber)
            {
                return bossData.bossPrefab;
            }
        }
        return null;
    }
    #endregion










    #region 更新錢包、波數、血量等
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
    #endregion
    #region 按鈕類腳本，內包含暫停介面按鈕、下一波、回到主選單、重新遊玩
    bool OpenStopPanel = false;
    public void ClickStopButtom()
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
        StopCoroutine(WaveWaitCoroutine);
        StopCoroutine(NextCoroutine);
        UpdateWave();
        countdownTime = 10;
        countdownStart = false;
        ObjNextWaveButton.SetActive(false);
        NextCoroutine = StartCoroutine(SpawnNextWave());
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
    #endregion

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