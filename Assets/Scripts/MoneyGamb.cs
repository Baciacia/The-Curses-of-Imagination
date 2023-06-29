using UnityEngine;
using System.Collections.Generic;

public class MoneyGamb : Item
{
    bool useditem = true;
    public override void UseItem()
    {Debug.Log("folosescgamb");
        int CoinNumber = UnityEngine.Random.Range(2, 20);
        if(useditem)
        {
            useditem = false;
            GameMan.Instance.AddCurrency(CoinNumber);
        }
    }
}