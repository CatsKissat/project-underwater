using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlamingApes.Underwater
{
    public interface IAttack
    {
        IEnumerator Attack();
        int GetDamage { get; }
        float GetAttackRange { get; }
        bool IsAttacking { get; }
    }
}
