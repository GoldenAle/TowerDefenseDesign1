using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuy : MonoBehaviour
{
    [SerializeField] GameObject[] towerBuyers;
    [Header("Tower")]
    [SerializeField] GameObject tower;
    [Header("Buyer")]
    [SerializeField] GameObject towerBuyerParent;
    [SerializeField] int[] costs;
    [Header("Canvas")]
    [SerializeField] GameObject towerBuyCanvas;

    bool inBuyMode = false;

    public int TowerCost(int costNumber) 
    { 
        return costs[costNumber];
    }

    private void Start()
    {
        towerBuyCanvas.SetActive(false);
        towerBuyerParent.SetActive(false);
    }

    public void BuyTower(int towerNumber)
    {
        if (inBuyMode && costs[towerNumber] < GameManager.GlobalGameManager.CurrentPlayerData.PlayerMoney) 
        {
            float spawnHeight = 5.5f;
            Instantiate(tower, new Vector3(towerBuyers[towerNumber].transform.position.x, spawnHeight, towerBuyers[towerNumber].transform.position.z), Quaternion.identity);
            Destroy(towerBuyers[towerNumber]);
            GameManager.GlobalGameManager.CurrentPlayerData.PlayerMoney -= costs[towerNumber];
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
