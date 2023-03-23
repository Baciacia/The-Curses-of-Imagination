using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Enemy1
{
    Calm,
    Follow,
    Attack,
}

public class enemy1Move : MonoBehaviour
{
    GameObject player,stats;
    public Enemy1 currentState = Enemy1.Calm;
    public float enemyRange, enemySpeed,enemyAttackRange;
    public int cooldownSecondsAttackEnemy;
    private bool directionSet = false, cooldownAttackEnemy = false;
    private Vector3 randomDirection;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        switch(currentState)
        {
            case(Enemy1.Calm):
            {
                Calm();
                break;
            }
            case(Enemy1.Follow):
            {
                Follow();
                break;
            }
            case(Enemy1.Attack):
            {
                Attack();
                break;
            }
        }
    }

    private IEnumerator ChooseDirection()
    {
        directionSet = true;
        yield return new WaitForSeconds(Random.Range(2f,8f));
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

        transform.position += - transform.right * enemySpeed * Time.deltaTime; 
        if(Vector3.Distance(transform.position, player.transform.position) < enemyRange)
        {
            currentState = Enemy1.Follow;
        }
    }

    private void Attack()
    {
        if(!cooldownAttackEnemy)
        {
            gameStats.getDamage(1);
            StartCoroutine(CoolDownAttack());
        }
        
        if(Vector3.Distance(transform.position, player.transform.position) > enemyAttackRange)
            {
                currentState = Enemy1.Follow;
            }
    }

    private IEnumerator CoolDownAttack()
    {
        cooldownAttackEnemy = true;
        yield return new WaitForSeconds(cooldownSecondsAttackEnemy);
        cooldownAttackEnemy = false;
    }
    private void Follow()
    {
        //if pentru range de attack;
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position,enemySpeed * Time.deltaTime);
            if(Vector3.Distance(transform.position, player.transform.position) > enemyRange)
            {
                currentState = Enemy1.Calm;
            }
            else
                if(Vector3.Distance(transform.position, player.transform.position) < enemyAttackRange)
                {
                    currentState = Enemy1.Attack;
                }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
