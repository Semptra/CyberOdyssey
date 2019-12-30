using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public int Damage;

    public float MovementSpeed;

    public Vector2 Velocity;

    public AudioClip LaunchAudio;

    public AudioClip HitAudio;

    private Rigidbody2D _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        GameManager.Instance.PlaySound(LaunchAudio, 0.4f);
    }

    void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(Velocity.x * MovementSpeed, Velocity.y * MovementSpeed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<Player>();
        if (player != null)
        {
            player.Damage(Damage);
            GameManager.Instance.PlaySound(HitAudio, 0.6f);
            Destroy(gameObject);
        }
    }
}
