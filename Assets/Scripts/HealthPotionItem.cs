using UnityEngine;

public class HealthPotionItem : Item
{
    public int healthAmount;
    bool useditem = true;
    
    public override void UseItem()
    {
        if(useditem)
        {
            useditem = false;
            player.TakeDamage(-healthAmount);
            Slot slot = GetComponentInParent<Slot>();
            Debug.Log("folosit item2222" + "slot:" + slot);
            if (slot != null && slot.transform.childCount > 0)
            {
                Debug.Log("folosit item");
                slot.DropItems();
            }
        }
    }
}