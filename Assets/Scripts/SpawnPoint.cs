using System.Collections;
using System.Collections.Generic;
using Tanks;
using UnityEngine;

namespace Tanks
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField]
        private float _checkRadious;

        public bool IsSomeoneNear()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _checkRadious);
            foreach (Collider2D collider in colliders)
            {
                var enemyTank = collider.gameObject.GetComponent<ConditionComponent>();
                var playerTank = collider.gameObject.GetComponent<PlayerConditionComponent>();
                if (enemyTank != null)
                {
                    return false;
                }
                else if (playerTank != null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return true;
        }
    }
}
