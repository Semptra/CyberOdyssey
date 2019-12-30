using System.Collections;
using UnityEngine;

public class Spacesheep : MonoBehaviour
{
    public int Health;

    public float MovementSpeed;

    public Vector2 Velocity;

    public int Score;

    public AudioClip DestroySound;

    public GameObject Projectile;

    public float ReloadTime;

    private Rigidbody2D _rigidbody;

    void Start()
    {
        _rigidbody = this.GetComponent<Rigidbody2D>();

        var spacesheepCollider = this.GetComponent<PolygonCollider2D>();
        var player = FindObjectOfType<Player>();
        var playerCollider = player.gameObject.GetComponent<PolygonCollider2D>();
        Physics2D.IgnoreCollision(spacesheepCollider, playerCollider);

        var borders = GameObject.FindGameObjectsWithTag("Border");
        foreach(var border in borders)
        {
            var borderCollider = border.GetComponent<BoxCollider2D>();
            Physics2D.IgnoreCollision(spacesheepCollider, borderCollider);
        }

        var spacesheeps = FindObjectsOfType<Spacesheep>();
        foreach(var spacesheep in spacesheeps)
        {
            var otherSpacesheepCollider = spacesheep.GetComponent<PolygonCollider2D>();
            Physics2D.IgnoreCollision(spacesheepCollider, otherSpacesheepCollider);
        }

        StartCoroutine(FireProjectoleRoutine());
    }

    void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(Velocity.x * MovementSpeed, Velocity.y * MovementSpeed);
    }

    public void Damage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            GameManager.Instance.PlaySound(DestroySound);
            GameManager.Instance.AddScore(Score);
            Destroy(gameObject);
        }
    }

    private readonly Quaternion RightRotationQuaternion = Quaternion.Euler(0, 0, -90);
    private readonly Vector3 OffsetVector = new Vector3(-0.5f, 0, 0);
    IEnumerator FireProjectoleRoutine()
    {
        while (true)
        {
            Instantiate(Projectile, transform.position + OffsetVector, RightRotationQuaternion);
            yield return new WaitForSeconds(ReloadTime);
        }
    }
}
