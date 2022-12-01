using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlamingApes.Underwater
{
    public interface IAttack
    {
        IEnumerator Attack();
        float GetAttackRange { get; }
        bool IsAttacking { get; }
    }
}
