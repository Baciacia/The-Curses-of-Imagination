using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public Item item;
    //public SpriteRenderer spriteRenderer;

    private bool isSelected = false;

    private void Start()
    {
        Slot slot = GetComponentInParent<Slot>();
        //spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        if (!isSelected)
        {
            isSelected = true;
            //spriteRenderer.color = Color.red;
            Debug.Log("Selected item: " + item.itemName);
        }
        else
        {
            isSelected = false;
            //spriteRenderer.color = Color.white;
        }
    }

    private void Update()
    {
        Slot slot = GetComponentInParent<Slot>();
        if (isSelected && Input.GetKeyDown(KeyCode.B))
        {
            if (PurchaseItem())
            {
                isSelected = false;
                slot.DropItems();
                
            }
        }
    }

    private bool PurchaseItem()
    {
        if (GameMan.Instance.CanAfford(item.price))
        {
            GameMan.Instance.PurchaseItem(item.price);
            item.isPurchased = true;
            Debug.Log("Item " + item.itemName + " purchased for " + item.price + " coins!");
            return true;
        }
        else
        {
            Debug.Log("You don't have enough coins to purchase this item!");
            return false;
        }
    }
}
