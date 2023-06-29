using System.Collections;
using UnityEngine;

public class EnemyInv : MonoBehaviour
{
    public enum EnemyStare{
        Calm,
        Attack,
        Follow,
        Wait
    }
    public EnemyStare currentState = EnemyStare.Follow;
    GameObject player;
    public RoomScript room;
    private MainPlayerScript playerr;
    public float invisibilityDuration = 2f;   // Durata invizibilității
    public float attackRange = 1.5f;          // Distanța de atac
    public int attackDamage = 10;             // Daunele de atac
    public float attackCooldown = 2f;         // Cooldown-ul dintre atacuri

    private bool isInvisible = false;         // Indicator pentru invizibilitate
    private bool canAttack = true;          // Indicator pentru atac în desfășurare
    private Renderer enemyRenderer;           // Referință la componenta Renderer
    private Transform target;
     private int health = 5;

    private void Start()
    {
        enemyRenderer = GetComponent<Renderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerr = GameObject.FindGameObjectWithTag("Player").GetComponent<MainPlayerScript>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(StealthRoutine());
    }

    private IEnumerator StealthRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);  
            SetInvisibility(true);  
           
            yield return new WaitForSeconds(2f);  
            SetInvisibility(false);  
        }
    }

    private void SetInvisibility(bool invisible)
    {
        isInvisible = invisible;

        enemyRenderer.enabled = !invisible;
    }

    private void Update()
    {
        if(IsVisibleOnScreen())
        {
            Follow(2f);
           
            if (canAttack)
            {
                // Verificăm dacă jucătorul se află în raza de atac
                if (Vector3.Distance(transform.position, target.position) <= attackRange)
                {
                    // Lansăm corutină pentru a începe atacul
                    StartCoroutine(AttackRoutine());
                }
            }
        }
    }

    private IEnumerator AttackRoutine()
    {
        canAttack = false;

        playerr.TakeDamage(attackDamage);
        
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
     public void Die()
    {
        room.nmbEnemyes--;
        Destroy(gameObject);
    }
}
