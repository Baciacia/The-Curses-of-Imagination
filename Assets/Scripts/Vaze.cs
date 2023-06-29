using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vaze : MonoBehaviour
{
    private float dist = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collisionObject)
    {
        if (collisionObject.tag == "Player" || collisionObject.tag == "EnemyRange" || collisionObject.tag == "EnemyMelee")
        {
            Vector2 playerPosition = collisionObject.transform.position;
            Vector2 vazaPosition = transform.position;

            

            float offsetX = Mathf.Abs(playerPosition.x - vazaPosition.x);
            float offsetY = Mathf.Abs(playerPosition.y - vazaPosition.y);

            if (offsetY > offsetX)
            {
                if (playerPosition.y > vazaPosition.y)
                {
                    Debug.Log("interactiuneeee1");
                    collisionObject.transform.position = new Vector2(playerPosition.x, vazaPosition.y + dist);
                }
                else if (playerPosition.y < vazaPosition.y)
                {
                    Debug.Log("interactiuneeeeSub");
                    collisionObject.transform.position = new Vector2(playerPosition.x, vazaPosition.y - dist);
                }
            }
            else if (offsetX > offsetY)
            {
                if (playerPosition.x > vazaPosition.x)
                {
                    collisionObject.transform.position = new Vector2(vazaPosition.x + dist, playerPosition.y);
                }
                else if (playerPosition.x < vazaPosition.x)
                {
                    Debug.Log("interactiuneeeedreapta");
                    collisionObject.transform.position = new Vector2(vazaPosition.x - dist, playerPosition.y);
                }
            }
            else
            {
                // Dacă jucătorul se află pe diagonală față de vază, ajustați poziția în funcție de distanța dorită
                Vector2 direction = playerPosition - vazaPosition;
                direction.Normalize();
                Vector2 newPosition = vazaPosition + (direction * dist);
                collisionObject.transform.position = newPosition;
            }
        }
    }





    public void Broken()
    {
        Destroy(gameObject);
        
    }
}
