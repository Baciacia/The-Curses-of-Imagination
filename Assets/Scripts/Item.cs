using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public string itemName;
    public int price;
    public bool isPurchased = false;
    public MainPlayerScript player;
    public float interactionRange = 0.1f;

    public abstract void UseItem();

    public void SetPlayer(MainPlayerScript player)
    {
        this.player = player;
    }
}
