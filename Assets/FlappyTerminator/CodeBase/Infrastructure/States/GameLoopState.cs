using Assets.FlappyTerminator.CodeBase.Infrastructure.Services.SaveLoad;
using Assets.FlappyTerminator.CodeBase.Player;
using UnityEngine;

namespace Assets.FlappyTerminator.CodeBase.Infrastructure.States
{
    public class GameLoopState : IPayloadedState<PlayerDeath>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly ISaveLoadService _saveLoadService;

        private PlayerDeath _playerDeath;

        public GameLoopState(GameStateMachine gameStateMachine, ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _saveLoadService = saveLoadService;
        }

        public void Enter(PlayerDeath playerDeath)
        {
            _playerDeath = playerDeath;

            _playerDeath.Died += OnDied;

            Time.timeScale = 1f;
        }

        public void Exit()
        {
            
        }

        private void OnDied()
        {
            _saveLoadService.SaveProgress();
            
            _gameStateMachine.Enter<LoadProgressState>();
        }
    }
}
