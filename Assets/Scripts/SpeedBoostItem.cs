using UnityEngine;

public class SpeedBoostItem : Item
{
    public float boostDuration = 95f;
    public float boostAmount = 3f;
    

    public override void UseItem()
    {
        player.moveSpeed += boostAmount;
        Invoke(nameof(RemoveSpeedBoost), boostDuration);
    }

    private void RemoveSpeedBoost()
    {
        player.moveSpeed -= boostAmount;
    }
}