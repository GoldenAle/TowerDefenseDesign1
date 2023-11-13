using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TowerBuy : MonoBehaviour
{
    [Header("Arrays")]
    [SerializeField] GameObject[] towerBuyers;
    [SerializeField] GameObject[] towers;
    [Header("Money")]
    [SerializeField] int cost;
    [SerializeField] int costMultiplier = 2;
    [Header("Objects")]
    [SerializeField] GameObject towerBuyerParent;
    [SerializeField] GameObject towerBuyCanvas;

    bool inBuyMode = false;

    public int TowerCost() 
    { 
        return cost;
    }

    public bool InBuyMode() 
    { 
        return inBuyMode;
    }

    private void Start()
    {
        towerBuyCanvas.SetActive(false);
        towerBuyerParent.SetActive(false);
    }

    public void BuyTower(int towerNumber)
    {
        if (inBuyMode && cost < GameManager.GlobalGameManager.CurrentPlayerData.PlayerMoney) 
        {
            towers[towerNumber].SetActive(true);
            Destroy(towerBuyers[towerNumber]);
            GameManager.GlobalGameManager.CurrentPlayerData.PlayerMoney -= cost;
            cost *= costMultiplier;
        }
    }

    public void BuyMode(bool doEnable)
    {        
        if (doEnable) 
        {
            Time.timeScale = 0f;
            inBuyMode = true;
            towerBuyCanvas.SetActive(true);
            towerBuyerParent.SetActive(true);
        }
        else 
        {
            Time.timeScale = 1f;
            inBuyMode = false;
            towerBuyCanvas.SetActive(false);
            towerBuyerParent.SetActive(false);
        }
    }
}