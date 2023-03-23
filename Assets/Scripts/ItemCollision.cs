using UnityEngine;

public class ItemCollision : MonoBehaviour
{
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collisionObject)
    {
        if(collisionObject.tag == "Player")
        {
            MainPlayerMovements.contorMoneyCollected += 1;
            Destroy(gameObject);
        }
    }
}
