using System;
using System.Collections;
using Tanks;
using UnityEngine;

namespace Tanks
{
    [RequireComponent(typeof(MoveComponent), typeof(FireComponent),typeof(ConditionComponent))]
    public class EnemyControllerComponent : MonoBehaviour
    {
        MoveComponent _moveComp;
        FireComponent _fireComp;
        ConditionComponent _health;

        [SerializeField]
        private float _defaultMoveTime = 3f;
        private float _moveTime;
       
        private Transform _playerTransform;
        bool _changeDirection = false;
        
        void Start ()
        {
            _moveComp = GetComponent<MoveComponent>();
            _fireComp = GetComponent<FireComponent>();
            _health = GetComponent<ConditionComponent>();

            _playerTransform = GameObject.Find("PlayerTank").transform;

            StartMove();
        }

        private void Update() => _fireComp.OnFire();
        
        private void ResetMoveTime() => _moveTime = _defaultMoveTime;

        private void StartMove()
        {
            if(!_health.GetHitByPlayer)
            {
                StartCoroutine(MoveController(GetRandomDirection()));
            }
            else
            {
                StartCoroutine(MoveController(GetDirectionToPlayer()));
            }
        }

        private IEnumerator MoveController(DirectionType type)
        {
            ResetMoveTime();
            while (_moveTime > 0)
            {
                _moveComp.OnMove(type);
                _moveTime -= Time.deltaTime;
                
                if(_changeDirection)
                {
                    _changeDirection = false;
                    yield break;
                }
                yield return null;
            }
            
            StartMove();
        }

        private DirectionType GetDirectionToPlayer()
        {
            Vector3 difference = _playerTransform.position - transform.position;

            if (Mathf.Abs(difference.x) > Mathf.Abs(difference.y))
            {
                return difference.x > 0 ? DirectionType.Right : DirectionType.Left;
            }
            else
            {
                return difference.y > 0 ? DirectionType.Up : DirectionType.Down;
            }
        }

        private DirectionType GetRandomDirection()
        {
            return (DirectionType)UnityEngine.Random.Range(1, System.Enum.GetValues(typeof(DirectionType)).Length);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision != null)
            {
                var cell = collision.gameObject.GetComponent<CellComponent>();
                if(cell != null)
                {
                    if (_changeDirection == true) return;
                    if (_changeDirection != true && cell.DestroyCell != true)
                    {
                        StartCoroutine(MoveController(GetRandomDirection()));
                        _changeDirection = true;
                        return;
                    }
                }

                var tank = collision.gameObject.GetComponent<ConditionComponent>();
                if (tank != null) 
                {
                    if (_changeDirection == true) return;
                    if (_changeDirection != true)
                    {
                        StartCoroutine(MoveController(GetRandomDirection()));
                        _changeDirection = true;
                        return;
                    }
                }
                
            }
        }
    }
}
