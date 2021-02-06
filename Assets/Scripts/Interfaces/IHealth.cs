using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    void TakeDamage(int damage, string damageSourceId);
    void TakeHealing(int healing);
}
