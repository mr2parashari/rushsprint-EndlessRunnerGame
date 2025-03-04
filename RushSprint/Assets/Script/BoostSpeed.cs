using UnityEngine;

public class BoostSpeed : MonoBehaviour
{
    public float boostAmount = 15f; // How much to increase speed
    public float boostDuration = 5f; // How long the boost lasts

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.ActivateSpeedBoost(boostAmount, boostDuration);
                Destroy(gameObject); // Remove the power-up after collection
            }
        }
    }
}
