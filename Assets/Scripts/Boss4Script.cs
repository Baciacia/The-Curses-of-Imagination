using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss4Script : MonoBehaviour
{
    public GameObject enemyPrefab1, enemyPrefab2, enemyPrefab3;
    public float spawnCooldown = 30f;
    GameObject player;
    private float currentCooldown = 28f;
    private float finalCD = 0;
    private bool canSpawn = true;
    private int health = 40;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
         if(IsVisibleOnScreen())
        {
            currentCooldown += Time.deltaTime;
            if (currentCooldown >= spawnCooldown && canSpawn)
            {
                SpawnEnemy();
                currentCooldown = 0f;
                canSpawn = false;
                Invoke("ResetSpawnCooldown", spawnCooldown);
            }
        }
    }

    private void SpawnEnemy()
    {
        int number = UnityEngine.Random.Range(0, 3);

        switch (number)
        {
            case 0:
                Instantiate(enemyPrefab1, transform.position, Quaternion.identity);
                break;

            case 1:
                Instantiate(enemyPrefab2, transform.position, Quaternion.identity);
                break;

            case 2:
                Instantiate(enemyPrefab3, transform.position, Quaternion.identity);
                break;

            default:
                break;
        }

    }

    private void Follow(float speed)
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position,speed * Time.deltaTime);
    }

    private bool IsVisibleOnScreen()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        return screenPoint.x >= 0 && screenPoint.x <= 1 && screenPoint.y >= 0 && screenPoint.y <= 1;
    }

    private void ResetSpawnCooldown()
    {
        canSpawn = true;
    }

    public void TakeDamage()
    {
        health--;
    }

    public bool isDead()
    {
        return health <= 0;
    }

    public void Die()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("EndGame"); 
        
    }
}
