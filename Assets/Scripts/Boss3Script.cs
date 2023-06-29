using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss3Script : MonoBehaviour
{
    public GameObject bombPrefab;
    public float bombCooldown = 2f;
    public float explosionRadius = 5f;
    public LayerMask explosionMask;
    private Transform playerTransform;
    private int health = 20;
    private bool canThrowBomb = true;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

    }

    private void Update()
    {
        if(IsVisibleOnScreen())
        {
            Follow(3);
            if (canThrowBomb)
            {
                
                ThrowBomb();
                canThrowBomb = false;
                Invoke("ResetBombCooldown", bombCooldown);
            }
        }
    }

    private void ThrowBomb()
    {
        for (int i = 0; i < 8; i++)
        {
            GameObject bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
            Rigidbody2D bombRigidbody = bomb.GetComponent<Rigidbody2D>();
            bomb.GetComponent<Rigidbody2D>().velocity = GetThrowDirection() * 5f;
        }
    }
    private Vector2 GetThrowDirection()
    {
        Vector2 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector2 bossPosition = transform.position;

        Vector2 throwDirection = playerPosition - bossPosition;
        Debug.Log(throwDirection);
        throwDirection.Normalize();

        return throwDirection;
    }

    private void ResetBombCooldown()
    {
        canThrowBomb = true;
    }

    private void Follow(float speed)
    {
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position,speed * Time.deltaTime);
    }

    private bool IsVisibleOnScreen()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        return screenPoint.x >= 0 && screenPoint.x <= 1 && screenPoint.y >= 0 && screenPoint.y <= 1;
    }

    public void Die()
    {
        Destroy(gameObject);
        GameMan.Instance.NextLevel();
        SceneManager.LoadScene("etaj3"); 
        
    }
    public void TakeDamage()
    {
        health--;
    }

    public bool isDead()
    {
        return health <= 0;
    }
}
