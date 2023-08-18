using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//怪物型態
public enum BossType    //此種類用來判斷是哪一種Boss有什麼能力。
{
    None,
    AttackBoss,
    SpeedBoss,
    DefendBoss,
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
//城牆種類
public enum WallType
{
    None,
    Wall_Burn,
    Wall_Poison,
    Wall_Normal

}