using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainPlayerScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public int health = 10000;
    public Text healthText;
    private Rigidbody2D rb;
    public GameObject projectilePrefab;
    public GameObject canvasGame,canvasDeath;
    public float projectileForce = 20f,lastBullet;
    public RoomScript room;
    public GameObject inventory,InterfataGame;
    public float attackCooldown = 0.5f;

    public GameObject explosionPrefab,bombPrefab;
    public PiercingBullet piercingBullet;
    public float explosionRadius = 2f;
    public float explosionForce = 10f;
    Color bulletColor = Color.black;
    Color bulletColor2 = Color.blue;
    private bool canPlaceBomb = true;

    private void Start()
    {
        Debug.Log(canvasDeath);
        healthText.text = "Health: " + health.ToString();
        rb = GetComponent<Rigidbody2D>();
        piercingBullet.setPiercingF(false);
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        float horizontalCoordonatesWaterGun = Input.GetAxis("AttackGunHorizontal");
        float verticalCoordonatesWaterGun = Input.GetAxis("AttackGunVertical");

        Vector2 movement = new Vector2(horizontalInput, verticalInput);
        rb.velocity = movement.normalized * moveSpeed;

        if((horizontalCoordonatesWaterGun != 0 || verticalCoordonatesWaterGun != 0) && Time.time > lastBullet + attackCooldown)
        {
            WaterShoot(horizontalCoordonatesWaterGun, verticalCoordonatesWaterGun);
            lastBullet = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            // Deschide inventarul
            inventory.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            // Închide inventarul
            inventory.SetActive(false);
        }

         if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Deschide inventarul
            InterfataGame.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            // Închide inventarul
            InterfataGame.SetActive(false);
        }
        if (canPlaceBomb && Input.GetKeyDown(KeyCode.Space) &&  GameMan.Instance.bombnmbs() > 0) // verificam daca mai sunt bombe;
        {
            Debug.Log("bomba!!");
            PlaceBomb();
            GameMan.Instance.BombNumber(-1);

        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            // Deschide inventarul
            SceneManager.LoadScene("etaj1"); 
        }
        else if (Input.GetKeyUp(KeyCode.I))
        {
            // Închide inventarul
            SceneManager.LoadScene("etaj2"); 
        }

         if (Input.GetKeyDown(KeyCode.O))
        {
            // Deschide inventarul
            SceneManager.LoadScene("etaj3"); 
        }
       
    }
    void WaterShoot(float x, float y)
    {
        if(piercingBullet.getPiercing())
        {
            Debug.Log(piercingBullet.getPiercing() + "piecing??");
            GameObject waterProjectile = Instantiate(projectilePrefab,transform.position, transform.rotation) as GameObject;
            SpriteRenderer spriteRenderer = waterProjectile.GetComponent<SpriteRenderer>();
            spriteRenderer.color = bulletColor;
            float nextProjectileStepH = (x < 0) ? Mathf.Floor(x) * projectileForce : Mathf.Ceil(x) * projectileForce;
            float nextProjectileStepV = (y < 0) ? Mathf.Floor(y) * projectileForce : Mathf.Ceil(y) * projectileForce;
            waterProjectile.GetComponent<Rigidbody2D>().velocity = new Vector3(nextProjectileStepH,nextProjectileStepV,0);
        }
        else
        {
            Debug.Log(piercingBullet.getPiercing() + "piecing??");
            GameObject waterProjectile = Instantiate(projectilePrefab,transform.position, transform.rotation) as GameObject;
            SpriteRenderer spriteRenderer = waterProjectile.GetComponent<SpriteRenderer>();
            spriteRenderer.color = bulletColor2;
            float nextProjectileStepH = (x < 0) ? Mathf.Floor(x) * projectileForce : Mathf.Ceil(x) * projectileForce;
            float nextProjectileStepV = (y < 0) ? Mathf.Floor(y) * projectileForce : Mathf.Ceil(y) * projectileForce;
            waterProjectile.GetComponent<Rigidbody2D>().velocity = new Vector3(nextProjectileStepH,nextProjectileStepV,0);
        }
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "WallT")
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y-0.2f);
        }
        else if(other.tag == "WallB")
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x , gameObject.transform.position.y+ 0.2f );
        }
        else if(other.tag == "WallR")
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x-0.2f, gameObject.transform.position.y);
        }
        else if(other.tag == "WallL")
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x + 0.2f, gameObject.transform.position.y);
        }
        if (other.CompareTag("Vaza")) // Verificați dacă colliderul cu care s-a produs coliziunea este o piatră
        {
           // Debug.Log("vaza!");
            rb.velocity = Vector2.zero;
        }
    
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthText.text = "Health: " + health.ToString();
        if (health <= 0)
        {
            Die();
        }
        // else if(health > 100)
        // {
        //     health = 100;
        //     healthText.text = "Health: " + health.ToString();
        // }
    }
    public void setStatus()
    {
        health = 10000;
        GameMan.Instance.setStatus();
    }
    void Die()
    {
        SceneManager.LoadScene("EndMenu"); 
        
        // Implementeaza ce se intampla cand jucatorul moare
    }
    public void Knockback(Vector3 origin, float force)
    {
        Vector3 direction = (transform.position - origin).normalized;
        rb.AddForce(direction * force, ForceMode2D.Impulse);
    }
    void PlaceBomb()
    {
        canPlaceBomb = false;

        // Instantiate the bomb prefab at the player's position
        GameObject bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
        Vector3 bombpoz = transform.position;
        StartCoroutine(DelayedAction(bomb, bombpoz));
    }
    IEnumerator DelayedAction(GameObject bomb, Vector3 poz)
    {
        // Așteaptă 2 secunde
        yield return new WaitForSeconds(1.5f);
        Destroy(bomb);

        // Instantiate an explosion effect
        GameObject expl = Instantiate(explosionPrefab, poz, Quaternion.identity);
        Collider2D[] colliders2 = Physics2D.OverlapCircleAll(poz, 2f);
        foreach (Collider2D collider2 in colliders2)
        {
            // Cauzează daune inamicilor
            EnemyController enemy = collider2.GetComponent<EnemyController>();
            Enemy2Controller enemy2 = collider2.GetComponent<Enemy2Controller>();
            EnemyDuplicate enemy3 = collider2.GetComponent<EnemyDuplicate>();
            Fly enemy4 = collider2.GetComponent<Fly>();

            if (enemy != null)
            {
                enemy.BombDmg();
                if(enemy.isDead())
                {
                    enemy.Die();
                    if (Random.value < 0.4f) 
                    {
                        GameObject coin = Instantiate(Resources.Load("Coin")) as GameObject; 
                        coin.transform.position = transform.position; 
                    }
                }
            }
            else if(enemy2 != null)
            {
                enemy2.BombDmg();
                if(enemy2.isDead())
                {
                    enemy2.Die();
                    if (Random.value < 0.4f) 
                    {
                        GameObject coin = Instantiate(Resources.Load("Coin")) as GameObject; 
                        coin.transform.position = transform.position; 
                    }
                }
            }
            else if(enemy3!= null)
            {
                enemy3.BombDmg();
                if(enemy3.isDead())
                {
                    enemy3.Die();
                    if (Random.value < 0.4f) 
                    {
                        GameObject coin = Instantiate(Resources.Load("Coin")) as GameObject; 
                        coin.transform.position = transform.position; 
                    }
                }
            }
            else if(enemy4 != null)
            {
                enemy4.BombDmg();
                if(enemy4.isDead())
                {
                    enemy4.Die();
                    if (Random.value < 0.4f) 
                    {
                        GameObject coin = Instantiate(Resources.Load("Coin")) as GameObject; 
                        coin.transform.position = transform.position; 
                    }
                }
            }

            // Cauzează daune obiectelor
            Vaze destructibleObject = collider2.GetComponent<Vaze>();
            if (destructibleObject != null)
            {
                Debug.Log("salut!!");
                Destroy(destructibleObject.gameObject);
                if (Random.value < 0.4f) 
                {
                    GameObject coin = Instantiate(Resources.Load("Coin")) as GameObject; 
                    coin.transform.position = poz; 
                }
            }
        }
        // Allow the player to place another bomb
        canPlaceBomb = true;
        StartCoroutine(DelayedAction2(expl));

    }
    IEnumerator DelayedAction2(GameObject expl)
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(expl);
    }

    public void ApplySlow(float slowFactor, float slowDuration)
    {
        moveSpeed = moveSpeed * slowFactor;
        StartCoroutine(ResetSpeedAfterDelay(slowDuration));
    }

    private IEnumerator ResetSpeedAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Restabilim viteza normală a personajului
        moveSpeed = 5f;
    }

    public void attackspeed()
    {
        projectileForce *= 1.5f;
        projectilePrefab.transform.localScale = new Vector3(projectilePrefab.transform.localScale.x * 1.5f, projectilePrefab.transform.localScale.y *1.5f, 0);
    }
}
    




