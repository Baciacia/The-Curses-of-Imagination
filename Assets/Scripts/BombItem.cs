using UnityEngine;

public class BombItem : Item
{
    public int BombNumber = 5;
    bool useditem = true;
    
    public override void UseItem()
    {Debug.Log("folosescbombaaa");
        if(useditem)
        {Debug.Log("folosescbombaaa2");
            useditem = false;
            GameMan.Instance.BombNumber(BombNumber);
        }
    }
}