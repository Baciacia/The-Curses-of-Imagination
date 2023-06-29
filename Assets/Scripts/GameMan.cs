using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMan : MonoBehaviour
{
    public static GameMan Instance;
    public Text moneyText,levelText,bombsText;

    public int playerCurrency = 0,level = 0, bombs = 5;

    public void Start()
    {
        moneyText.text = "Money: " + playerCurrency.ToString(); 
        levelText.text = "Level: " + level.ToString(); 
        bombsText.text = "Bombs: " + bombs.ToString();
    }

    public void Update()
    {

    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool CanAfford(int cost)
    {
        return playerCurrency >= cost;
    }

    public int GetPlayerMoney()
    {
        return playerCurrency;
    }

    public void PurchaseItem(int cost)
    {
        if (CanAfford(cost))
        {
            playerCurrency -= cost;
            moneyText.text = "Coins: " + playerCurrency;
            Debug.Log("Item purchased for " + cost + " currency.");
        }
        else
        {
            Debug.Log("Not enough currency to purchase item.");
        }
    }

    public void AddCurrency(int amount)
    {
        playerCurrency += amount;
        moneyText.text = "Coins: " + playerCurrency;
       // Debug.Log(amount + " currency added.");
    }

    public void BombNumber(int amount)
    {
        bombs += amount;
        bombsText.text = "Bombs: "+ bombs;
    } 

    public int bombnmbs()
    {
        return bombs;
    }

    public void NextLevel()
    {
        level+=1;
        levelText.text = "Level: " + level.ToString(); 
    }
    public void setStatus()
    {
        playerCurrency = 0;
    }
}
