using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyControl : MonoBehaviour, ILivingEntity
{

    [SerializeField] private Transform _target;
    [SerializeField] private Enemy enemy;
    [SerializeField] private Animator animator;

    [SerializeField] private Vector3 _attackOffset;
    [SerializeField] private float _attackRange = 3f;
    [SerializeField] private LayerMask _attackMask;
    [SerializeField] private PanelManager _panelManager;


    private int _id;
    private string _name;
    private float _life;
    private float _defence;
    private float _attack;
    private bool death = false;

    private Rigidbody2D _rb;

    void Start()
    {
        _id = enemy._id;
        _name = enemy._name;
        _life = enemy._life;
        _defence = enemy._defence;
        _attack = enemy._attack;

        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (Vector2.Distance(_target.transform.position, _rb.position) <= _attackRange)
        {
            animator.SetTrigger("attack");
        }
    }

    public void Attack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * _attackOffset.x;
        pos += transform.up * _attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, _attackRange, _attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<PlayerControl>().ReceiveDamages(_attack);
        }
    }

    #region Life
    public void ReceiveDamages(float damages)
    {
        if (death)
        {
            return;
        }
        float finalDamage = damages - _defence;

        _life -= finalDamage;
        if (_life <= 0)
        {
            death = true;
            Die();
        } else
        {
            animator.SetTrigger("hit");
        }
        Logger.Log("Enemy " + _name + " receive " + finalDamage + " damages. -> Attack of player (" + damages + ") - defence of enemy (" + _defence + ")");
    }

    public void ReceiveHeal(float heal)
    {
        _life += heal;
        Logger.Log("Enemy receive heal (" + heal + ").");
    }

    public void Die()
    {
        animator.SetTrigger("death");

        Destroy(gameObject.GetComponent<AIDestinationSetter>());
        Destroy(gameObject.GetComponent<AIPath>());
        Destroy(gameObject.GetComponent<EnemyAI>());
        Destroy(gameObject.GetComponent<Seeker>());
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        Destroy(gameObject.GetComponent<BoxCollider2D>());
        Destroy(this);

        _panelManager.ScoreUpdate(50);
    }
    #endregion
}