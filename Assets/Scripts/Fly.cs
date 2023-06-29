using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    public enum EnemyStare{
        Calm,
        Attack,
        Follow,
        Wait
    }

    public int speed;
    GameObject player;
    private MainPlayerScript playerr;
    public int range, rangeAttack;
    private bool directionSet = false;
    private Vector3 randomDirection;
    public EnemyStare currentState = EnemyStare.Follow;
    public int cooldownSecondsAttackEnemy;
    private bool cooldown = false;
    public RoomScript room;
    private int health = 2;
    private int enemyes;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerr = GameObject.FindGameObjectWithTag("Player").GetComponent<MainPlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //  if (isInRoom && !roomBounds.Contains(transform.position)) {
        //     // Împinge inamicul înapoi în interiorul camerei
        //     transform.position = roomBounds.ClosestPoint(transform.position);
        // }
        if(IsVisibleOnScreen())
        {
            switch(currentState)
            {
                case EnemyStare.Calm:
                {
                    Calm();
                    break;
                }
                case EnemyStare.Follow:
                {
                    Follow();
                    break;
                }
                case EnemyStare.Attack:
                {
                    Attack();
                    break;
                }
            }

            if(IsPlayerInRange(range))
            {
                currentState = EnemyStare.Follow;
            }
            else if(!IsPlayerInRange(range))
            {
                currentState = EnemyStare.Calm;
            }
            if(Vector3.Distance(transform.position, player.transform.position) <= rangeAttack)
            {
                currentState = EnemyStare.Attack;
            }
        }
      

    }
    private bool IsPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }

    private IEnumerator ChooseDirection()
    {
        directionSet = true;
        yield return new WaitForSeconds(Random.Range(1f,3f));
        randomDirection = new Vector3(0,0,Random.Range(0,360));
        Quaternion rotatii = Quaternion.Euler(randomDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation,rotatii,Random.Range(0.5f,3f));
        directionSet = false;
    }

    private void Calm()
    {
        if(!directionSet)
        {
            StartCoroutine(ChooseDirection());
        }

        transform.position += - transform.right * speed * Time.deltaTime; 
        
        if(IsPlayerInRange(range))
        {
            currentState = EnemyStare.Follow;
        }
    }

    private void Attack()
    {
        if(!cooldown)
        {
            playerr.TakeDamage(5);
            StartCoroutine(CoolDownAttack());
        }
        
        if(Vector3.Distance(transform.position, player.transform.position) > rangeAttack)
        {
            currentState = EnemyStare.Follow;
        }
    }

    private IEnumerator CoolDownAttack()
    {
        cooldown = true;
        yield return new WaitForSeconds(cooldownSecondsAttackEnemy);
        cooldown = false;
    }

    private void Follow()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position,speed * Time.deltaTime);
        if(Vector3.Distance(transform.position, player.transform.position) > range)
        {
            currentState = EnemyStare.Calm;
        }
        else
        {
            if(Vector3.Distance(transform.position, player.transform.position) < rangeAttack)
            {
                currentState = EnemyStare.Attack;
            }
        }
    }

    private bool IsVisibleOnScreen()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        return screenPoint.x >= 0 && screenPoint.x <= 1 && screenPoint.y >= 0 && screenPoint.y <= 1;
    }
    public void Die()
    {
        room.nmbEnemyes--;
        Debug.Log("number of enemyes in room-fly: " + room.nmbEnemyes);
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
