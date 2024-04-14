using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks
{
    public class DestroyInGameManager : MonoBehaviour
    {
        GameManagerComponent _gameManager;

        void Start()
        {
            _gameManager = GameObject.Find("GameManager").GetComponent<GameManagerComponent>();
        }

        private void OnDestroy()
        {
            if( _gameManager != null ) 
            _gameManager.TankWasDestoy(gameObject);
        }
    }
}
