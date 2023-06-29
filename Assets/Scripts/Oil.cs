using UnityEngine;

public class Oil : MonoBehaviour
{
    public float slowFactor = 0.3f; // Factorul de reducere a vitezei
    public float slowDuration = 2f; // Durata efectului de slow (în secunde)

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Reducem viteza personajului
            MainPlayerScript player = other.GetComponent<MainPlayerScript>();
            if (player != null)
            {
                player.ApplySlow(slowFactor, slowDuration);
            }
        }
    }
}
