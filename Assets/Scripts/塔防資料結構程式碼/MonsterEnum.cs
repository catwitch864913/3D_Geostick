using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//怪物型態
public enum EnemyType    //此種類用來計算關卡怪物出來的是哪種來去剪掉關卡的資料結構。
{
    None,
    NormalEnemy,
    SpeedEnemy,
    DefendEnemy,
    AttackEnemy,
}
//怪物種類
public enum MonsterType  //此種類用來判斷關卡Boss過關，以及金幣獲取量
{
    None,
    Boss,
    LittleBoss,
    EliteEnemy,
    CommonEnemy,
}
