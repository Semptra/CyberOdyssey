using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
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

    private Rigidbody2D rigidbody;

    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody2D>();
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
            float absX = Math.Abs(rigidbody.velocity.x);
            if (absX < StopDelta)
            {
                rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
            }
            else if (absX > 0)
            {
                rigidbody.AddForce(new Vector2(-Math.Sign(rigidbody.velocity.x) * DecelerationSpeed, 0));
            }
        }
        else if (Math.Abs(rigidbody.velocity.x) < MaxSpeed)
        {
            float horizontalForce = _horizontalInput * MovementSpeed;
            if (Math.Sign(rigidbody.velocity.x) != Math.Sign(horizontalForce))
            {
                horizontalForce *= DecelerationSpeed;
            }

            rigidbody.AddForce(new Vector2(horizontalForce, 0));
        }

        if (_verticalInput == 0)
        {
            float absY = Math.Abs(rigidbody.velocity.y);
            if (absY < StopDelta)
            {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
            }
            else if (absY > 0)
            {
                rigidbody.AddForce(new Vector2(0, -Math.Sign(rigidbody.velocity.y) * DecelerationSpeed));
            }
        }
        else if (Math.Abs(rigidbody.velocity.y) < MaxSpeed)
        {
            float verticalForce = _verticalInput * MovementSpeed;
            if (Math.Sign(rigidbody.velocity.y) != Math.Sign(verticalForce))
            {
                verticalForce *= DecelerationSpeed;
            }

            rigidbody.AddForce(new Vector2(0, verticalForce));
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
        var projectileInstance = Instantiate(projectile, transform.position + OffsetVector, RightRotationQuaternion);
        var projectileMovement = projectileInstance.GetComponent<ProjectileMovement>();
        projectileMovement.Velocity = Vector3.right;
    }
}
