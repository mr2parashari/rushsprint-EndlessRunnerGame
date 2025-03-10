using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            // Call TakeDamage() on the obstacle
            Obstacle obstacle = other.GetComponent<Obstacle>();
            if (obstacle != null)
            {
                obstacle.TakeDamage(); // Reduce obstacle health
            }

            Destroy(gameObject); // Destroy bullet on impact
        }
    }
}



/*using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    private int hitCount = 0;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            hitCount++;

            if (hitCount >= 2)
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }
    }
}*/