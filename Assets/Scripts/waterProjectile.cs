using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterProjectile : MonoBehaviour
{
    public float lifeTime;
    void Start()
    {
        StartCoroutine(death());
    }

    void Update()
    {
        
    }
    IEnumerator death()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collisionObject)
    {
        if(collisionObject.tag == "Enemy1")
        {
            collisionObject.gameObject.GetComponent<enemy1Move>().Die();
            Destroy(gameObject);
        }
    }
}
