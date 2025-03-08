using UnityEngine;

public class BulletPickup : MonoBehaviour
{
    public int ammoAmount = 4;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.CollectBullets(ammoAmount);
                Destroy(gameObject);
            }
        }
    }
}

