using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private int hitCount = 0;
    private int health = 2; // Takes 2 hits to destroy

    public void TakeDamage()
    {
        hitCount++;
        if (hitCount >= health)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.PlayerHit();  // Call game over logic
        }
    }
}




/*using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private int hitCount = 0;
    //public int health = 2; // Takes 2 hits to destroy
    public void TakeDamage()
    {
        hitCount++;
        if (hitCount >= 2)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.PlayerHit();  // Reduce attempts
        }
    }
}*/