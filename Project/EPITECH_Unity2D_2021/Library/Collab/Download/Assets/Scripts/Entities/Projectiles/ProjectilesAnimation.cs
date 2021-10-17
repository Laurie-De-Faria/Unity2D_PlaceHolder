using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilesAnimation : MonoBehaviour
{
    #region Variables
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private Sprite _sprite;
    #endregion

    #region Methods
    private void Start()
    {
        if (_spriteRenderer is null)
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    #region Getters/Setters
    public void SetSprite(Sprite sprite)
    {
        _sprite = sprite;
        ChangeSprite();
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
    private void StartAnimation(/*arguments*/)
    {
        StopAnimation();
        Logger.Log("Start animation of projectile.");
    }

    public void StartMoveAnimation()
    {
        Logger.Log("Start animation of projectile.");
        StartAnimation();
    }

    public void StartCollsionAnimation()
    {
        Logger.Log("Start collision animation of projectile.");
        StartAnimation();
    }
    #endregion

    #region Stop animation
    private void StopAnimation()
    {
        Logger.Log("Stop actual animation of projectile.");
    }

    public void StopMoveAnimation()
    {
        Logger.Log("Stop animation of projectile.");
    }

    public void StopCollsionAnimation()
    {
        Logger.Log("Stop collision animation of projectile.");
    }
    #endregion
    #endregion
}
