using UnityEngine;
using System.Collections;

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

                // Find and disable nearby obstacles
                Collider[] obstacles = Physics.OverlapSphere(transform.position, 10f); // Adjust radius
                foreach (Collider col in obstacles)
                {
                    if (col.CompareTag("Obstacle"))
                    {
                        col.enabled = false;
                        StartCoroutine(ReEnableCollider(col, boostDuration));
                    }
                }

                Destroy(gameObject); // Remove the power-up after collection
            }
        }
    }

    private IEnumerator ReEnableCollider(Collider col, float duration)
    {
        yield return new WaitForSeconds(duration);
        col.enabled = true;
    }
}
