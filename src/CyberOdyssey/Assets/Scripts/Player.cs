using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Movement

    [Header("Movement")]
    public float MovementSpeed;

    public float DecelerationSpeed;

    public float MaxSpeed;

    public float StopDelta;

    private float _horizontalInput;

    private float _verticalInput;

    #endregion

    #region Fire

    [Space]
    [Header("Fire")]
    public float Fire1ReloadTime;

    public GameObject Fire1Projectile;

    [Space]
    public float Fire2ReloadTime;

    public GameObject Fire2Projectile;

    [Space]
    public float ReloadTimeDelta;

    private float _currentFire1ReloadTime;

    private float _currentFire2ReloadTime;

    private float _fire1Input;

    private float _fire2Input;

    #endregion

    #region Health

    public int Health;

    #endregion

    private Rigidbody2D _rigidbody;

    void Start()
    {
        _rigidbody = this.GetComponent<Rigidbody2D>();
        GameManager.Instance.UpdateHealth(Health);
    }

    void Update()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        _fire1Input = Input.GetAxisRaw("Fire1");
        _fire2Input = Input.GetAxisRaw("Fire2");
    }

    void FixedUpdate ()
    {
        #region Movement

        if (_horizontalInput == 0)
        {
            float absX = Math.Abs(_rigidbody.velocity.x);
            if (absX < StopDelta)
            {
                _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
            }
            else if (absX > 0)
            {
                _rigidbody.AddForce(new Vector2(-Math.Sign(_rigidbody.velocity.x) * DecelerationSpeed, 0));
            }
        }
        else if (Math.Abs(_rigidbody.velocity.x) < MaxSpeed)
        {
            float horizontalForce = _horizontalInput * MovementSpeed;
            if (Math.Sign(_rigidbody.velocity.x) != Math.Sign(horizontalForce))
            {
                horizontalForce *= DecelerationSpeed;
            }

            _rigidbody.AddForce(new Vector2(horizontalForce, 0));
        }

        if (_verticalInput == 0)
        {
            float absY = Math.Abs(_rigidbody.velocity.y);
            if (absY < StopDelta)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            }
            else if (absY > 0)
            {
                _rigidbody.AddForce(new Vector2(0, -Math.Sign(_rigidbody.velocity.y) * DecelerationSpeed));
            }
        }
        else if (Math.Abs(_rigidbody.velocity.y) < MaxSpeed)
        {
            float verticalForce = _verticalInput * MovementSpeed;
            if (Math.Sign(_rigidbody.velocity.y) != Math.Sign(verticalForce))
            {
                verticalForce *= DecelerationSpeed;
            }

            _rigidbody.AddForce(new Vector2(0, verticalForce));
        }

        #endregion

        #region Fire

        if (_fire1Input != 0 && _currentFire1ReloadTime <= 0)
        {
            CreateProjectile(Fire1Projectile);
            StartCoroutine(ReloadFire1());
        }

        if (_fire2Input != 0 && _currentFire2ReloadTime <= 0)
        {
            CreateProjectile(Fire2Projectile);
            StartCoroutine(ReloadFire2());
        }

        #endregion
    }

    public void Damage(int damage)
    {
        Health = Math.Max(Health - damage, 0);
        GameManager.Instance.UpdateHealth(Health);
    }

    IEnumerator ReloadFire1()
    {
        _currentFire1ReloadTime = Fire1ReloadTime;
        while (_currentFire1ReloadTime > 0)
        {
            yield return new WaitForSeconds(ReloadTimeDelta);
            _currentFire1ReloadTime -= ReloadTimeDelta;
        }
    }

    IEnumerator ReloadFire2()
    {
        _currentFire2ReloadTime = Fire2ReloadTime;
        while (_currentFire2ReloadTime > 0)
        {
            yield return new WaitForSeconds(ReloadTimeDelta);
            _currentFire2ReloadTime -= ReloadTimeDelta;
        }
    }

    private readonly Quaternion RightRotationQuaternion = Quaternion.Euler(0, 0, 90);
    private readonly Vector3 OffsetVector = new Vector3(0.5f, 0, 0);
    void CreateProjectile(GameObject projectile)
    {
        Instantiate(projectile, transform.position + OffsetVector, RightRotationQuaternion);
    }
}
