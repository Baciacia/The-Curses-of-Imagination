using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss2Script : MonoBehaviour
{
    public float teleportDistance = 10f;
    public float attackRange = 2f;
    public float moveSpeed = 5f;
    public int attackDamage = 10;
    public float attackCooldown = 2f,jumpcooldown = 4f;
    private float lastAttackTime = 0f,lastTP = 0;
    private Transform playerTransform;
    private Rigidbody2D rb;
    GameObject player;    
    private float speed=1;
    private int health = 20;
    private float timer;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if(IsVisibleOnScreen())
        {
            Follow(3);
            if (Vector2.Distance(transform.position, playerTransform.position) <= teleportDistance)
            {
                if (Time.time >= lastTP + jumpcooldown)
                {
                    transform.position = playerTransform.position;
                    lastTP = Time.time;
                }                

                if (Vector2.Distance(transform.position, playerTransform.position) <= attackRange)
                {
                    Attack();
                }
            }
        }
    }

    private void Attack()
    {
        // Verificăm dacă este momentul pentru un atac nou
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            // Atacăm jucătorul
            playerTransform.GetComponent<MainPlayerScript>().TakeDamage(attackDamage);
            lastAttackTime = Time.time;
        }
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
        SceneManager.LoadScene("etaj2"); 
        
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
