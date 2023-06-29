using UnityEngine;
using System.Collections;

public class PowerItem : Item
{
    public float projectileSizeMultiplier = 2.5f;
    public float projectileSpeedMultiplier = 1.5f;
    bool useditem = true;
    //public float effectDuration = 15f;

    public override void UseItem()
    {

        if(useditem)
        {
            useditem = false;
            player.attackspeed();
        }
    }

}
