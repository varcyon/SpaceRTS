using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttacker 
{
    void FindEnemies();
    void Attack();
    void Chase();
}
