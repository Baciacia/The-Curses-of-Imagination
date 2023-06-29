using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss1Script : MonoBehaviour
{
    public enum BossStare{
        Attack,
        Follow,
    }


    public BossStare currentState = BossStare.Follow;
    public GameObject bulletPrefab;
    GameObject player;
    public float attackInterval = 4f;
    
    public float bulletSpeed = 15f;
    private float speed;
    private float rangeAttack = 1.5f;

    private int health = 30;
    private float timer;

    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        if(IsVisibleOnScreen())
        {
            timer += Time.deltaTime;
            Follow(3f);
            if (timer >= attackInterval)
            {
                Attack();
                timer = 0f;
            }
        }
    }

    private void Attack()
    {
        for (int i = 0; i < 25; i++)
        {
            Vector3 direction = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)) * Vector3.up;

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        }
    }
    private void Follow(float speed)
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position,speed * Time.deltaTime);
        if(Vector3.Distance(transform.position, player.transform.position) < rangeAttack)
        {
            currentState = BossStare.Attack;
        }
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
        SceneManager.LoadScene("etaj1"); 
        
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
