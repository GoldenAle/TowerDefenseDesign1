using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuy : MonoBehaviour
{
    [SerializeField] GameObject tower;
    [SerializeField] Vector3[] towerPositions;

    [SerializeField] GameObject towerBuyCanvas;

    bool inBuyMode = false;

    private void Start()
    {
        towerBuyCanvas.SetActive(false);
    }
    public void BuyTower(int spawnPos)
    {
        //Instantiate(tower, towerPositions[spawnPos], Quaternion.identity); Debug.Log("w4e");
    }

    public void BuyMode(bool doEnable)
    {
        towerBuyCanvas.SetActive(doEnable);
        
        if (doEnable) 
        {
            Time.timeScale = 0f;
            inBuyMode = true;
        }
        else 
        {
            Time.timeScale = 1f;
            inBuyMode = false;
        }
    }
}
