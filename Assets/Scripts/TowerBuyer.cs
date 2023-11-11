using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuyer : MonoBehaviour
{
    [SerializeField] TextMesh costText;
    [SerializeField, Tooltip("The tower number in the array")] int towerNumber;
    [Header("Colors")]
    [SerializeField] Color cantAffordColor;
    [SerializeField] Color canAffordColor;

    TowerBuy towerBuy;

    private void Awake()
    {
        towerBuy = FindObjectOfType<TowerBuy>();
    }

    private void Update() 
    { 
        costText.text = towerBuy.TowerCost(towerNumber) + "$";
        if (towerBuy.TowerCost(towerNumber) > GameManager.GlobalGameManager.CurrentPlayerData.PlayerMoney) 
        { 
            costText.color = cantAffordColor;
        }
        else 
        {
            costText.color = canAffordColor;
        }     
    }
}