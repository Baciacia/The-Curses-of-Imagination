using UnityEngine;

public class PiercingBullet : Item
{
    bool piercing;
    bool useditem = true;
    void Start()
    {
        piercing = false;
    }
    public override void UseItem()
    {
        if(useditem)
        {
            useditem = false;
            Debug.Log("folosesc piercing");
            piercing = true;
        }
    }
    public bool getPiercing()
    {
        return piercing;
    }
    public void setPiercingF(bool fls)
    {
        piercing = fls;
    }
     
}