using UnityEngine;
using UnityEngine.UI;

public class gameStats : MonoBehaviour
{

    public static gameStats gStats;
    private static int health = 10, maxHealth = 10;
    public Text healthBar;
    public static int setgetHealth
    {
        get { return health; }
        set { health= value; }
    }

    public static int setgetMaxHealth
    {
        get { return maxHealth; }
        set { maxHealth= value; }
    }
    private void Awake()
    {
        if(gStats == null)
        {
            gStats = this;
        }
    }

    public static void getDamage(int damage)
    {
        health = health - damage;
        if(health < 0)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Destroy(player);
        }
    }

    public static void getHeal(int heal)
    {
        health = Mathf.Min(health + heal,maxHealth);
    }

    void Update()
    {
        healthBar.text = "Health : " + health;
    }
}
