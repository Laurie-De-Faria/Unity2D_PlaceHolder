using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    #region Variables
    private SpriteRenderer _spriteRenderer;
    private Sprite _sprite;

    [SerializeField] private Animator _animator;
    private PlayerSounds _soundsManager;
    #endregion

    #region Methods
    private void Start()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _soundsManager = gameObject.GetComponent<PlayerSounds>();
    }

    #region Getters/Setters
    public void SetSprite(Sprite sprite)
    {
        _sprite = sprite;
        ChangeSprite();
    }

    public void SetAnimator(Animator animator)
    {
        _animator = animator;
    }

    public bool GetDirectionLookAt()
    {
        if (_spriteRenderer is null)
            return false;
        return _spriteRenderer.flipX;
    }
    #endregion

    private void ChangeSprite()
    {
        _spriteRenderer.sprite = _sprite;
    }

    public void FlipSprite(bool flipX)
    {
        if (_spriteRenderer)
        _spriteRenderer.flipX = flipX;
    }

    #region Start animation
    public void StartWalkAnimation()
    {
        StopAnyAnimation();
        _animator.SetBool("isWalking", true);
    }

    public void StartJumpAnimation()
    {
        StopAnyAnimation();
        _animator.SetTrigger("isJumping");
    }

    public void StartMeleeAttackAnimation()
    {
        StopAnyAnimation();
        _animator.SetTrigger("isMeleeAttack");
        _soundsManager.StartMeleeAttack();
    }

    public void StartRangedAttackAnimation()
    {
        StopAnyAnimation();
        _animator.SetTrigger("isRangedAttack");
        _soundsManager.StartRangedAttack();
    }

    public void StartIdleAnimation()
    {
        StopAnyAnimation();
    }

    public void StartHurtAnimation()
    {
        StopAnyAnimation();
        _animator.SetTrigger("isHurt");
    }

    public void StartDeathAnimation()
    {
        StopAnyAnimation();
        _animator.SetTrigger("isDead");
    }
    #endregion

    #region Stop animation
    private void StopAnyAnimation()
    {
        if (_animator.runtimeAnimatorController != null)
        {
            _animator.SetBool("isWalking", false);
            _animator.SetBool("isJumping", false);
            _animator.SetBool("isMeleeAttack", false);
            _animator.SetBool("isRangedAttack", false);
            _animator.SetBool("isDead", false);
        }
    }
    #endregion
    #endregion
    
}
