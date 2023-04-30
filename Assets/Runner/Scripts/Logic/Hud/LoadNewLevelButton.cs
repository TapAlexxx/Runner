using System;
using Scripts.Infrastructure.Services.StateMachine;
using Scripts.Infrastructure.Services.StateMachine.States;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scripts.Logic.Hud
{

    public class LoadNewLevelButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        
        private GameStateMachine _gameStateMachine;

        private void OnValidate()
        {
            button = GetComponentInChildren<Button>();
        }

        public void Initialize(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        private void Start()
        {
            button.onClick.AddListener(LoadNewLevel);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(LoadNewLevel);
        }

        private void LoadNewLevel()
        {
            string sceneName = SceneManager.GetActiveScene().name;
            _gameStateMachine.Enter<LoadLevelState, string>(sceneName);
        }
    }

}