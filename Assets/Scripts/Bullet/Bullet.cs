using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 12f;
    [SerializeField] private float lifeTime = 3f;

    private Vector2 direction;
    private int damage;
    private float lifeTimer;

    public void Initialize(Vector2 direction, int damage)
    {
        this.direction = direction.normalized;
        this.damage = damage;
        lifeTimer = lifeTime;
    }

    private void Update()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;
        lifeTimer -= Time.deltaTime;
        if(lifeTimer<=0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Enemy"))
        {
            return;
        }
        Enemy enemy = other.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
