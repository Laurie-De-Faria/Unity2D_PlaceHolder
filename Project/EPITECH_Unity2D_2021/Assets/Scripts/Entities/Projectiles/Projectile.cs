using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    #region Variables
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private ProjectilesAnimation _animator;

    private float _attack = 0.5f;
    private float _speed = 10f;
    #endregion

    #region Methods
    #region Initialization
    void Start()
    {
        if (_rb is null)
            _rb = gameObject.GetComponent<Rigidbody2D>();
        if (_animator is null)
            _animator = gameObject.GetComponent<ProjectilesAnimation>();
    }

    public void InitProjectile(Projectiles projectile, bool isRightPosition)
    {
        _attack = projectile._attack;
        _speed = projectile._speed;
        InitAnimation(projectile);
        if (isRightPosition)
        {
            _speed = -_speed;
            _animator.FlipSprite(true);

        }
        _rb.AddForce(new Vector2(_speed, 0));
    }

    private void InitAnimation(Projectiles projectile)
    {
        _animator.SetSprite(projectile._sprite);
        _animator.StartMoveAnimation();
    }
    #endregion

    #region Collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyControl enemy = collision.gameObject.GetComponent<EnemyControl>();
            
            enemy.ReceiveDamages(_attack);
            DestroyProjectile();
        } else
        {
            DestroyProjectile();
        }
    }

    #endregion

    private void DestroyProjectile()
    {
        _animator.StartCollsionAnimation();
        Destroy(gameObject);// il faut que le destroy attende (Coroutine) la fin de l'animation
    }
    #endregion
}
