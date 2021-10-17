using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    [Header("Pathfinding")]
    [SerializeField] private Transform _target;
    [SerializeField] private float _activateDistance = 50f;
    [SerializeField] private float _pathUpdateSeconds = 0.5f;

    [Header("Physics")]
    [SerializeField] private float _speed = 200f;
    [SerializeField] private float _nextWaypointDistance = 3f;
    [SerializeField] private float _jumpModifier = 0.3f;
    [SerializeField] private float _jumpCheckOffset = 0.1f;

    [Header("Custom Behavior")]
    [SerializeField] private bool _followEnabled = true;
    [SerializeField] private bool _jumpEnabled = true;
    [SerializeField] private bool _directionLookEnabled = true;

    [Header("Graphics")]
    [SerializeField] private Transform enemyGFX;


    private Path _path;
    private int _currentWaypoint = 0;
    private bool _isGrounded = false;
    private Seeker _seeker;
    private Rigidbody2D _rb;


    private void Start()
    {
        _seeker = GetComponent<Seeker>();
        _rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, _pathUpdateSeconds);
    }

    private void FixedUpdate()
    {
        if (TargetInDistance() && _followEnabled)
        {
            PathFollow();
        }
    }

    private void UpdatePath()
    {
        if (_followEnabled && TargetInDistance() && _seeker.IsDone())
        {
            _seeker.StartPath(_rb.position, _target.position, OnPathComplete);
        }
    }

    private void PathFollow()
    {
        if (_path == null)
        {
            return;
        }

        // Reached end of path
        if (_currentWaypoint >= _path.vectorPath.Count)
        {
            return;
        }

        // See if colliding with anything
        Vector3 startOffset = transform.position - new Vector3(0f, GetComponent<Collider2D>().bounds.extents.y + _jumpCheckOffset);
        _isGrounded = Physics2D.Raycast(startOffset, -Vector3.up, 0.05f);

        // Direction Calculation
        Vector2 direction = ((Vector2)_path.vectorPath[_currentWaypoint] - _rb.position).normalized;
        Vector2 force = direction * (_speed / 2) * Time.deltaTime;

        // Jump
        if (_jumpEnabled && _isGrounded)
        {
            _rb.AddForce(Vector2.up * (_speed / 2) * _jumpModifier);
        }

        // Movement
        _rb.AddForce(force);

        // Next Waypoint
        float distance = Vector2.Distance(_rb.position, _path.vectorPath[_currentWaypoint]);
        if (distance < _nextWaypointDistance)
        {
            _currentWaypoint++;
        }

        // Direction Graphics Handling
        if (_directionLookEnabled)
        {
            if (force.x > 0.05f)
            {
                enemyGFX.localScale = new Vector3(Mathf.Abs(enemyGFX.localScale.x), enemyGFX.localScale.y, enemyGFX.localScale.z);
            } else if (force.x < -0.05f)
            {
                enemyGFX.localScale = new Vector3(-1f * Mathf.Abs(enemyGFX.localScale.x), enemyGFX.localScale.y, enemyGFX.localScale.z);
            }
        }
    }

    private bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, _target.transform.position) < _activateDistance;
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            _path = p;
            _currentWaypoint = 0;
        }
    }

}