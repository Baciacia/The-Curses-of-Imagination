using UnityEngine;
using System.Collections;

public class EnemyDuplicate : MonoBehaviour
{
    public enum EnemyStare{
        Attack,
        Follow,
    }

    GameObject player;
    public EnemyStare currentState = EnemyStare.Follow;
    public GameObject enemyPrefab; // Prefabul inamicului
    public int numSpawnedEnemies = 2; // Numărul de inamici care vor fi spawnați
    public float spawnRadius = 1.0f; // Raza în care vor fi spawnați inamicii mai mici

    private bool Dead = false; // Indicator dacă inamicul a murit

    public float attackRange = 6f; // distanța maximă la care inamicul poate ataca
    public float attackCooldown = 2f; // timpul de așteptare între atacuri
    public GameObject projectilePrefab; // proiectilul pe care îl aruncă inamicul
    public Transform projectileSpawnPoint; // poziția de unde se lansează proiectilul
    public float projectileSpeed = 4f; // viteza de lansare a proiectilului
    private int health = 5;

    public RoomScript room;
    private Transform target; // ținta la care inamicul se uită
    private bool canAttack = true; // dacă inamicul poate ataca sau nu (pentru a evita atacurile repetate)

    void Start()
    {
        // Setăm ținta inițială la jucător
        projectileSpawnPoint.position = transform.position;
        projectileSpawnPoint.rotation = transform.rotation;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    void Update()
    {
        if(IsVisibleOnScreen())
        {
            Follow(3f);
            if (canAttack)
            {
                // Verificăm dacă jucătorul se află în raza de atac
                if (Vector3.Distance(transform.position, target.position) <= attackRange)
                {
                    // Lansăm corutină pentru a începe atacul
                    StartCoroutine(Attack());
                }
            }
        }
        // Verificăm dacă inamicul poate ataca
       
    }

    private IEnumerator Attack()
    {
        canAttack = false;

        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = (target.position - projectileSpawnPoint.position).normalized * projectileSpeed;

        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
    }

     private bool IsVisibleOnScreen()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        return screenPoint.x >= 0 && screenPoint.x <= 1 && screenPoint.y >= 0 && screenPoint.y <= 1;
    }

    private void Follow(float speed)
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position,speed * Time.deltaTime);
        if(Vector3.Distance(transform.position, player.transform.position) < attackRange)
        {
            currentState = EnemyStare.Attack;
        }
    }
    public void Die()
    {
        room.nmbEnemyes--;

        // Spawnează inamicii mai mici
        room.nmbEnemyes ++;
        room.nmbEnemyes ++;
        Vector2 spawnOffset = new Vector2(1, 0);
        Vector2 spawnPosition = (Vector2)transform.position + spawnOffset;
        GameObject spawnedEnemy1 = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        spawnOffset = new Vector2(0, 1);
        spawnPosition = (Vector2)transform.position + spawnOffset;
        GameObject spawnedEnemy2 = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        Fly enemyController1 = spawnedEnemy1.GetComponent<Fly>();
        Fly enemyController2 = spawnedEnemy2.GetComponent<Fly>();
        enemyController1.room = room;
        enemyController2.room = room;
            
        Destroy(gameObject);
    }

    public void TakeDamage(int amount)
    {
        health = health - amount;
    }

    public bool isDead()
    {
        return health <= 0;
    }
    public void BombDmg()
    {
        Debug.Log("bomba pe duplicate");
        health -= 5;
    }
}
