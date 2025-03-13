using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotationSpeed = 100f;

    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime); // Rotate Ring
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Sonic collects the ring
        {
            GameManager.instance.AddRings(1); // Add 1 ring
            Destroy(gameObject); // Remove Ring
        }
    }
}
