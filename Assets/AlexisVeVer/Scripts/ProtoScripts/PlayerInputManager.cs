using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AlexisVeVer.Scripts.ProtoScripts
{
    public class PlayerInputManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] _playerPrefabs;
        [SerializeField] private Transform[] _spawnPoints;

        private int _prefabToSpawn = 0;
        private int _playerNumber = 0;
        
        private bool _zqsdJoined;
        private bool _maximumNumberReached;


        private void Update()
        {
            if (Keyboard.current == null) return;

            if (!_zqsdJoined && Keyboard.current.spaceKey.wasPressedThisFrame && !_maximumNumberReached)
            {
                var player = PlayerInput.Instantiate(_playerPrefabs[_prefabToSpawn],
                    controlScheme: "Keybaord&Mouse",
                    pairWithDevice: Keyboard.current);

                if (_spawnPoints.Length > 0)
                {
                    player.transform.position = _spawnPoints[_prefabToSpawn].position;
                }
                
                _zqsdJoined = true;
                _prefabToSpawn++;
                _playerNumber++;
            }

            foreach (var gamePad in Gamepad.all)
            {
                if (gamePad.buttonSouth.wasPressedThisFrame && !_maximumNumberReached)
                {
                    PlayerInput.Instantiate(_playerPrefabs[_prefabToSpawn],
                        controlScheme: "GamePad",
                        pairWithDevice: gamePad);

                    _prefabToSpawn++;
                    _playerNumber++;
                }
            }

            if (_prefabToSpawn >= _playerPrefabs.Length)
            {
                _prefabToSpawn = 0;
            }
            
            if (_playerNumber == 4)
            {
                _maximumNumberReached = true;
            }
        }
    }
}