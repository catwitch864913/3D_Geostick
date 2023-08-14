using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(menuName = "Benchmark/LevelData")]
public class LevelData : ScriptableObject
{
    [Header("第幾關")]  //給UI抓取呈現說目前是第幾關的，可有可無。
    public int Level;
    [Header("怪物總數量")]//在打總數量時需要再看該關是否有小Boss，要自行++
    public int EnemyQuantity;
    [Header("該關卡出現正常型態怪物數量")]
    public int NormalEnemy;
    [Header("該關卡出現速度型態怪物數量")]
    public int SpeedEnemy;
    [Header("該關卡出現防禦型態怪物數量")]
    public int DefendEnemy;
    [Header("該關卡出現菁英正常型態怪物數量")]
    public int NormalElite;
    [Header("該關卡出現菁英速度型態怪物數量")]
    public int SpeedElite;
    [Header("該關卡出現菁英防禦型態怪物數量")]
    public int DefendElite;
    [Header("該關卡出現的Boss")]//要在Boss身上新增另外腳本，當Boss被打死關卡結束
    public GameObject Boss; //數值直接在Boss身上調整
    [Header("關卡波數")]
    public int wave;
    [Header("波數怪物數量陣列")]
    public int[] EnemyWave;
    [Header("關卡核心血量")]
    public int CoreHP=10;
    [Header("初始金錢")]
    public int Coin;
}
