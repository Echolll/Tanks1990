using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks
{
    public class MoveComponent : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 1f;

        public void OnMove(DirectionType type)
        {
            transform.position += type.ConvertTypeFromDirection() * (_speed * Time.deltaTime);
            transform.eulerAngles = type.ConvertTypeFromRotation();
        }
    }
}
