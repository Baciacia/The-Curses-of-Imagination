using UnityEngine;
using UnityEngine.UI;


public class MainPlayerMovements : MonoBehaviour
{
    public float speedPlayer, waterProjectileSpeed, lastBullet, delay;
    Rigidbody2D rigidbodyPlayer;
    public Text textMoneyCollected;
    public static int contorMoneyCollected = 0;
    public GameObject projectile;
    void Start()
    {
        rigidbodyPlayer = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        float horizontalCoordonatesPlayer = Input.GetAxis("Horizontal");
        float verticalCoordonatesPlayer = Input.GetAxis("Vertical");

        float horizontalCoordonatesWaterGun = Input.GetAxis("AttackGunHorizontal");
        float verticalCoordonatesWaterGun = Input.GetAxis("AttackGunVertical");

        float nextStepHorizontaly = horizontalCoordonatesPlayer * speedPlayer;
        float nextStepVerticaly = verticalCoordonatesPlayer * speedPlayer;

        if((horizontalCoordonatesWaterGun != 0 || verticalCoordonatesWaterGun != 0) && Time.time > lastBullet + delay)
        {
            WaterShoot(horizontalCoordonatesWaterGun, verticalCoordonatesWaterGun);
            lastBullet = Time.time;
        }

        rigidbodyPlayer.velocity = new Vector3(nextStepHorizontaly, nextStepVerticaly, 0); 
        textMoneyCollected.text = "Money collected: " + contorMoneyCollected;
    }

    void WaterShoot(float x, float y)
    {
        GameObject waterProjectile = Instantiate(projectile,transform.position, transform.rotation) as GameObject;
        waterProjectile.AddComponent<Rigidbody2D>().gravityScale = 0.1F;

        float nextProjectileStepH = (x < 0) ? Mathf.Floor(x) * waterProjectileSpeed : Mathf.Ceil(x) * waterProjectileSpeed;
        float nextProjectileStepV = (y < 0) ? Mathf.Floor(y) * waterProjectileSpeed : Mathf.Ceil(y) * waterProjectileSpeed;

        waterProjectile.GetComponent<Rigidbody2D>().velocity = new Vector3(nextProjectileStepH,nextProjectileStepV,0);
    }
}
