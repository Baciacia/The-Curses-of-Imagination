using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private Inventory inventory;
    public GameObject itemButton;
    public GameObject canvas;
    public int price;
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            
            if(GameMan.Instance.CanAfford(price))
            {
                GameMan.Instance.PurchaseItem(price);
                for (int i = 0; i < inventory.slots.Length; i++)
                {
                    Debug.Log(itemButton);
                    if (inventory.isFull[i] == 0) 
                    {
                        inventory.isFull[i] = 1;
                        GameObject itemInv = Instantiate(itemButton, inventory.slots[i].transform.position, Quaternion.identity, inventory.slots[i].transform);
                        Destroy(gameObject);
                        break; 
                    }
                }
            }
            else Debug.Log("nu poti!");
            
        }
    }
    void Update()
    {
        
    }
}
