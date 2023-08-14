using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class AchievementUI : MonoBehaviour
{
    //public Game記分器 game;
    [SerializeField] public Text nameLabel;
    [SerializeField] public Text descriptionLabel;
    public float timer;
    [SerializeField] public Text timeBonusLabel;
    public Image iconImage;

    public void Awake()
    {
        Game記分器.instance.下載檔案();

    }
    public void 按我更新()
    {
        nameLabel.text = Game記分器.instance.獎盃資料.name;
        descriptionLabel.text = Game記分器.instance.獎盃資料.description;
        timer = Game記分器.instance.獎盃資料.timeBonus;
        timeBonusLabel.text = timer.ToString();
    }
    /*public string nameLabel
    {
        get { return nameLabel.text; }
        set { nameLabel.text = value; }
    }*/
}
