using UnityEngine;

public class spikes : MonoBehaviour
{
    public int damagePerTick = 10; // Daunele cauzate de tepi per tick
    public float tickRate = 0.3f; // Frecvența la care se aplică daunele (în secunde)

    private bool isPlayerTouching = false; // Verifică dacă personajul este în contact cu tepii
    private float timer = 0.3f; // Timer pentru a gestiona frecvența aplicării daunelor

    private void Update()
    {
        if (isPlayerTouching)
        {
            // Verificăm dacă a trecut suficient timp pentru a aplica daunele
            timer += Time.deltaTime;
            if (timer >= tickRate)
            {
                // Cauzăm daune personajului
                MainPlayerScript player = FindObjectOfType<MainPlayerScript>();
                if (player != null)
                {
                    player.TakeDamage(damagePerTick);
                }
                
                timer = 0f; // Resetează timer-ul
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerTouching = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerTouching = false;
            timer = 0f; // Resetează timer-ul când personajul nu mai este în contact cu tepii
        }
    }
}
