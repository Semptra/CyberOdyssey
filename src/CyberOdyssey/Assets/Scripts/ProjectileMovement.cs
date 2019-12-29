using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float Speed;

    public int Damage;

    public Vector2 Velocity;

    private Rigidbody2D _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(Velocity.x * Speed, Velocity.y * Speed);

        if (transform.position.magnitude > 200f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var spacesheep = other.GetComponent<Spacesheep>();
        if (spacesheep != null)
        {
            spacesheep.Damage(Damage);
            Destroy(gameObject);
        }
    }
}
