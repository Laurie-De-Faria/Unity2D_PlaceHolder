using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILivingEntity
{
    /// <summary>
    /// Calcul life to lose corresponding to damages receive and defence of this entity.
    /// </summary>
    /// <param name="damages">Value of damages receive.</param>
    public void ReceiveDamages(float damages);
    /// <summary>
    /// Calcul life to won corresponding to heal receive.
    /// </summary>
    /// <param name="heal">Value of heal receive.</param>
    public void ReceiveHeal(float heal);
    /// <summary>
    /// Start animation of die and destroy entity when animation was finish.
    /// </summary>
    public void Die();
}

