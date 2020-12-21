using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
   public class EnemyOne : Enemy_BaseClass
    {
        GameObject Enemy;
        public EnemyOne(GameObject enemy)
        {
            Enemy = enemy;
        }
        protected override void Tick()
        {
           
        }
    }
}
