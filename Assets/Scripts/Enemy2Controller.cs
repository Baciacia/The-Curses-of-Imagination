using UnityEngine;
using System.Collections;

public class Enemy2Controller : MonoBehaviour
{
    public enum EnemyStare{
        Attack,
        Follow,
    }

    GameObject player;
    public EnemyStare currentState = EnemyStare.Follow;
    public float attackRange = 6f; // distanța maximă la care inamicul poate ataca
    public float attackCooldown = 2f; // timpul de așteptare între atacuri
    public GameObject projectilePrefab; // proiectilul pe care îl aruncă inamicul
    public Transform projectileSpawnPoint; // poziția de unde se lansează proiectilul
    public float projectileSpeed = 4f; // viteza de lansare a proiectilului
    private int health = 2;

    public RoomScript room;
    private Transform target; // ținta la care inamicul se uită
    private bool canAttack = true; // dacă inamicul poate ataca sau nu (pentru a evita atacurile repetate)

    private void Start()
    {
        projectileSpawnPoint.position = transform.position;
        projectileSpawnPoint.rotation = transform.rotation;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    private void Update()
    {
        if(IsVisibleOnScreen())
        {
            Follow(3f);
            if (canAttack)
            {
                if (Vector3.Distance(transform.position, target.position) <= attackRange)
                {
                    StartCoroutine(Attack());
                }
            }
        }
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
        Debug.Log("Number of enemyes in a room :" + room.nmbEnemyes);
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
        health -= 2;
    }
}
