using UnityEngine;

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
                //Destroy(gameObject);
            }
        }
    }
}














/*using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 1;
    public float lifetime = 3f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Bullet hit: " + other.gameObject.name); // Debugging

        if (other.CompareTag("Obstacle"))
        {
            Obstacle obstacle = other.GetComponent<Obstacle>();
            if (obstacle != null)
            {
                obstacle.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}*/
