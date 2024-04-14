using System.Collections;
using System.Collections.Generic;
using Tanks;
using UnityEngine;

namespace Tanks
{
    public class GameManagerComponent : MonoBehaviour
    {
        [Header("Simple settings")]
        [SerializeField, Range(5, 20)]
        private int _maxEnemyTanks = 5;
        [SerializeField, Range(1, 5)]
        private int _maxLiveEnemyTanks = 2;

        [SerializeField]
        private List<GameObject> _enemyTanksInStock;
        [SerializeField]
        private List<GameObject> _liveEnemyTanks;

        [Space, Header("Tanks prefab")]
        [SerializeField]
        private List<GameObject> _enemyTanksPrefabs;

        private void Start()
        {
            OnCreateTanks();
        }

        private void OnCreateTanks()
        {
            for (int i = 0; i < _maxEnemyTanks; i++)
            {
                int index = Random.Range(0, _enemyTanksPrefabs.Count);
                GameObject tank = Instantiate(_enemyTanksPrefabs[index]);
                _enemyTanksInStock.Add(tank);
                tank.SetActive(false);
            }
        }

        private void Update()
        {
            if (_liveEnemyTanks.Count != _maxLiveEnemyTanks)
            {
                SpawnTank();
            }

            GameEnded();
        }

        private void SpawnTank()
        {
            int index = Random.Range(0, _enemyTanksInStock.Count);
            GameObject tank = _enemyTanksInStock[index];
            
            _liveEnemyTanks.Add(tank);
            _enemyTanksInStock.Remove(tank);
                      
            float x = Random.Range(-7f,7);
            float y = Random.Range(3.5f, 8f);
            tank.transform.position = new Vector2 (x, y);
            tank.SetActive(true);

        }

        public void TankWasDestoy(GameObject tank)
        {
            if (_liveEnemyTanks.Contains(tank))
            {
                _liveEnemyTanks.Remove(tank);
            }
        }

        public void GameEnded()
        {
            if(_enemyTanksInStock.Count == 0 && _liveEnemyTanks.Count == 0)
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }
}
