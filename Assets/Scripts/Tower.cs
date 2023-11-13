using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
using Unity.VisualScripting;

public class Tower : MonoBehaviour
{
    public Enemy EnemyData = new Enemy();

    [SerializeField] GameObject upgradeCanvas;
    [SerializeField] int prizeIncrease = 10;
    [SerializeField] int towerNumber;
    [Header("Colors")]
    [SerializeField] Color canAffordColor;
    [SerializeField] Color lookedColor;
    [Header("TowerDataDisplay")]
    [SerializeField] TextMeshProUGUI towerInfoText;
    [SerializeField] TextMeshProUGUI[] upgradePrizeTexts;
    [SerializeField] Button[] upgradeButtons;

    [SerializeField] int[] upgradePrizes;
    [SerializeField] float fireRateDivider = 1.2f;
    [SerializeField] int damageMultiplier = 2;
    [SerializeField] int moneyOnKillMultiplier = 2;

    string path = "No Path";
    int upgradeLevel = 0;
    bool hasUpgraded = false;

    TowerBuy towerBuy;

    private void Awake()
    {
        towerBuy = FindObjectOfType<TowerBuy>();
    }

    private void Update()
    {
        #region ugly code
        if (hasUpgraded) 
        {
            return;
        }
        if (upgradePrizes[0] >= GameManager.GlobalGameManager.CurrentPlayerData.PlayerMoney) 
        {
            upgradeButtons[0].image.color = lookedColor;
        }
        if (upgradePrizes[1] >= GameManager.GlobalGameManager.CurrentPlayerData.PlayerMoney)
        {
            upgradeButtons[1].image.color = lookedColor;
        }
        if (upgradePrizes[2] >= GameManager.GlobalGameManager.CurrentPlayerData.PlayerMoney)
        {
            upgradeButtons[2].image.color = lookedColor;
        }
        if (upgradePrizes[0] <= GameManager.GlobalGameManager.CurrentPlayerData.PlayerMoney)
        {
            upgradeButtons[0].image.color = canAffordColor;
        }
        if (upgradePrizes[1] <= GameManager.GlobalGameManager.CurrentPlayerData.PlayerMoney)
        {
            upgradeButtons[1].image.color = canAffordColor;
        }
        if (upgradePrizes[2] <= GameManager.GlobalGameManager.CurrentPlayerData.PlayerMoney)
        {
            upgradeButtons[2].image.color = canAffordColor;
        }
        #endregion ugly Ccde
    }

    private void Start()
    {
        upgradePrizeTexts[0].text = upgradePrizes[0] + "$";
        upgradePrizeTexts[1].text = upgradePrizes[1] + "$";
        upgradePrizeTexts[2].text = upgradePrizes[2] + "$";
    }

    public void ShowAvalableUpgrades(bool doShow)
    {
        towerInfoText.text = ("Tower " + towerNumber + "\n" + path + " Level: " + upgradeLevel);
        if (doShow && towerBuy.InBuyMode())
        {
            upgradeCanvas.SetActive(true);
        }
        else
        {
            upgradeCanvas.SetActive(false);
        }
    }

    public void Upgrade(int upgradeNumber)
    {
        hasUpgraded = true;
        if (upgradeNumber == 0 && path != "Damage" && path != "Bullets" && upgradePrizes[upgradeNumber] <= GameManager.GlobalGameManager.CurrentPlayerData.PlayerMoney)
        {
            path = "FireRate";
            upgradeButtons[1].image.color = lookedColor;
            upgradeButtons[2].image.color = lookedColor;

            upgradeLevel++;
            GameManager.GlobalGameManager.CurrentPlayerData.PlayerMoney -= upgradePrizes[upgradeNumber];

            upgradePrizes[upgradeNumber] *= prizeIncrease;
            upgradePrizeTexts[upgradeNumber].text = upgradePrizes[upgradeNumber] + "$";

            List<Bullet> bullets = new List<Bullet>();
            foreach(Bullet bullet in bullets) 
            {
                Test.test.CurrentBulletData.FireDelay /= fireRateDivider;
            }
        }
        else if (upgradeNumber == 1 && path != "FireRate" && path != "Bullets" && upgradePrizes[upgradeNumber] <= GameManager.GlobalGameManager.CurrentPlayerData.PlayerMoney)
        {
            path = "Damage";
            upgradeButtons[0].image.color = lookedColor;
            upgradeButtons[2].image.color = lookedColor;

            upgradeLevel++;
            GameManager.GlobalGameManager.CurrentPlayerData.PlayerMoney -= upgradePrizes[upgradeNumber];

            upgradePrizes[upgradeNumber] *= prizeIncrease;
            
            upgradePrizeTexts[upgradeNumber].text = upgradePrizes[upgradeNumber] + "$";

            List<Bullet> bullets = new List<Bullet>();
            foreach (Bullet bullet in bullets)
            {
                Test.test.CurrentBulletData.ImpactDamage *= damageMultiplier;
            }
        }
        else if (path != "FireRate" && path != "Damage" && upgradePrizes[upgradeNumber] <= GameManager.GlobalGameManager.CurrentPlayerData.PlayerMoney)
        {
            path = "Bullets";
            upgradeButtons[0].image.color = lookedColor;
            upgradeButtons[1].image.color = lookedColor;

            upgradeLevel++;
            GameManager.GlobalGameManager.CurrentPlayerData.PlayerMoney -= upgradePrizes[upgradeNumber];

            upgradePrizes[upgradeNumber] *= prizeIncrease;
            upgradePrizeTexts[upgradeNumber].text = upgradePrizes[upgradeNumber] + "$";

            List<EnemyBase> enemies = new List<EnemyBase>();
            foreach(EnemyBase enemy in enemies) 
            {
                enemy.EnemyData.MoneyOnKill *= moneyOnKillMultiplier;
            }
        }
    }
}
