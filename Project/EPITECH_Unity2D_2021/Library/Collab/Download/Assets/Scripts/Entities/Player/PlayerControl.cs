using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerControl : MonoBehaviour, IAgressif, ILivingEntity
{
    #region Variables
    private Rigidbody2D _rb;
    private PlayerAnimation _animator;

    private Animator _animatorComponent;

    [Header("Statistiques:")]
    [SerializeField] private float _life;
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _attack;
    [SerializeField] private float _defence;
    [SerializeField] private float _jumpForce;

    [SerializeField] private bool _isOnGround = false;
    private bool _isAttackMelee = false;

    [Header("Weapons and armor:")]
    [SerializeField] private Projectiles _projectile;
    [SerializeField] private GameObject _projectileObject;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Armor _armor;

    [Header("Timer")]
    private float _lastAttackTime = 0f;

    [Header("References world:")]
    [SerializeField] private float _limitY = -27f;

    [SerializeField] private PanelManager _panelManager;
    #endregion

    #region Methods
    #region Initialization
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _animator = gameObject.GetComponent<PlayerAnimation>();
        _animatorComponent = gameObject.GetComponent<Animator>();
        if (_projectileObject == null)
            _projectileObject = Resources.Load<GameObject>("Prefabs/Entities/Projectiles");
        InitEvents();
        _animator.SetAnimator(_animatorComponent);
        InitPlayer(Resources.Load<Player>("ScriptableObjects/Entities/Players/Godric"));
    }


    private void InitEvents()
    {
        KeyEvents.OnClickMouseLeft += MeleeAttack;
        KeyEvents.OnClickMouseRight += RangedAttack;
        KeyEvents.OnPressZ += Jump;
        KeyEvents.OnPressQ += MoveLeft;
        KeyEvents.OnPressD += MoveRight;

        GameStatus.OnGameOver += Die;
    }

    private void ResetEvents()
    {
        KeyEvents.OnClickMouseLeft -= MeleeAttack;
        KeyEvents.OnClickMouseRight -= RangedAttack;
        KeyEvents.OnPressZ -= Jump;
        KeyEvents.OnPressQ -= MoveLeft;
        KeyEvents.OnPressD -= MoveRight;

        GameStatus.OnGameOver -= Die;
    }

    public void InitPlayer(Player player)
    {
        Animator srcAnimComponent = null;

        _life = player._life;
        _speed = player._speed;
        _attack = player._attack;
        _defence = player._defence;
        _animator.SetSprite(player._sprite);

        srcAnimComponent = player._prefab.GetComponent<Animator>();
        _animatorComponent.runtimeAnimatorController = srcAnimComponent.runtimeAnimatorController;

        _panelManager.HealthUpdate(_life);

        SetArmor(Resources.Load<Armor>("ScriptableObjects/Stuff/Armors/EmptyArmor"));
    }
    #endregion

    private void Update()
    {
        if (_isAttackMelee)// TO REMOVE : when we found set _isAttack to false when animation finish
        {
            if (_lastAttackTime >= 0.5f)
            {
                _isAttackMelee = false;
            }
            else
                _lastAttackTime += Time.deltaTime;
        }

        if (gameObject.transform.position.y <= _limitY && !GameStatus.isPause)
            Die();
    }

    #region Manage stuff
    #region Set
    /// <summary>
    /// Add or replace weapon of player.
    /// </summary>
    /// <param name="weapon">Weapon to set.</param>
    public void SetWeapon(Weapon weapon)
    {
        _weapon = weapon;
    }

    /// <summary>
    /// Add or replace projectile weapon of player.
    /// </summary>
    /// <param name="projectile">Projectile to set.</param>
    /// <param name="projectilesObject">GameObject of projectile to set.</param>
    public void SetProjectile(Projectiles projectile)
    {
        if (projectile is null)
            Logger.LogError("Projectile was null.");
        _projectile = projectile;
    }

    #region Armor
    /// <summary>
    /// Add or replace armor of player.
    /// </summary>
    /// <param name="armor">Armor to set.</param>
    public void SetArmor(Armor armor)
    {
        _armor = armor;
    }

    /// <summary>
    /// Add or replace a piece of player's armor.
    /// </summary>
    /// <param name="pieceOfArmor">Piece of armor to set.</param>
    public void SetArmor(PieceOfArmor pieceOfArmor)
    {
        StuffEnums.TYPE_PIECE_OF_ARMOR typeOfPiece = pieceOfArmor._type;

        foreach (PieceOfArmor piece in _armor._pieces)
        {
            if (piece._type == typeOfPiece)
            {
                _armor._pieces.Remove(piece);
                break;
            }
        }
        _armor._pieces.Add(pieceOfArmor);
    }
    #endregion
    #endregion

    #region Lose
    public void LoseWeapon()
    {
        _weapon = null;
    }

    public void LoseProjectile()
    {
        _projectile = null;
    }

    public void LoseArmor()
    {
        _armor = null;
    }
    #endregion
    #endregion

    #region Actions
    #region Movement
    private void MoveRight()
    {
        _animator.FlipSprite(false);
        if (_isOnGround)
            _animator.StartWalkAnimation();
        _rb.velocity = new Vector2(1 * _speed, _rb.velocity.y);
    }

    private void MoveLeft()
    {
        _animator.FlipSprite(true);
        if (_isOnGround)
            _animator.StartWalkAnimation();
        _rb.velocity = new Vector2(-1 * _speed, _rb.velocity.y);
    }

    public void Jump()
    {
        if (!_isOnGround)
            return;
        _animator.StartJumpAnimation();
        _rb.AddForce(new Vector2(0.0f, 10f) * _jumpForce);
    }
    #endregion

    #region Attacks
    public void RangedAttack()
    {
        GameObject newProjectile;
        Projectile newProjectileScript;
        Vector3 positionProjectile;
        bool isLookRight = _animator.GetDirectionLookAt();

        _animator.StartRangedAttackAnimation();
        if (_projectile is null)
            Logger.LogError("Player hasn't projectile for ranged attack.");
        else
        {
            if (isLookRight)
                positionProjectile = gameObject.transform.position + new Vector3(-1, 0, 0);
            else
                positionProjectile = gameObject.transform.position + new Vector3(1, 0, 0);
            newProjectile = Instantiate(_projectileObject, gameObject.transform);
            newProjectile.transform.position = positionProjectile;
            newProjectileScript = newProjectile.GetComponent<Projectile>();
            newProjectileScript.InitProjectile(_projectile, isLookRight);
            Destroy(newProjectile, 8);
        }
    }

    public void MeleeAttack()
    {
        _isAttackMelee = true;
        _lastAttackTime = 0f;
        _animator.StartMeleeAttackAnimation();
    }
    #endregion

    #region Defence
    // use shield
    #endregion
    #endregion

    #region Collisions
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            _isOnGround = true;
        if (collision.gameObject.tag == "Enemy" && _isAttackMelee)
            AttackEnemy(collision.gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && _isAttackMelee)
        {
            AttackEnemy(collision.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            _isOnGround = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Collectable")
        {
            Collect(collision.gameObject.GetComponent<Collectable>());
        }
    }
    #endregion

    #region Life
    public void ReceiveDamages(float damages)
    {
        float finalDamage = damages - _defence - Armor.CalculDefence(_armor);

        if (finalDamage < 0)
            finalDamage = 0;
        _life -= finalDamage;
        Logger.Log("Player receive " + finalDamage + " damages. -> Attack of enemy (" + damages + ") - defence of player (" + _defence + ") - armor (0)");
        if (_life <= 0)
            GameStatus.isGameOver = true;
        else
            _animator.StartHurtAnimation();
        _panelManager.HealthUpdate(_life);
    }

    public void ReceiveHeal(float heal)
    {
        _life += heal;
        Logger.Log("Player receive heal (" + heal + ").");
    }

    public void Die()
    {
        GameStatus.isPause = true;
        _animator.StartDeathAnimation();
        // when animation finish, destroy OR send to finish panel (UI).

        _panelManager.GameOver();
    }
    #endregion

    #region Status
    // bonus status (heal, etc.)

    // malus status (poison, etc.)
    #endregion

    private void AttackEnemy(GameObject enemy)
    {
        enemy.GetComponent<EnemyControl>().ReceiveDamages(_attack + _weapon._attack);
        _isAttackMelee = false;
    }

    private void Collect(Collectable obj)
    {
        Logger.Log("Player collect " + obj.objectName);
        Destroy(obj.gameObject);
        _panelManager.CoinUpdate();
    }
    #endregion

    private void OnDestroy()
    {
        ResetEvents();
    }
}
