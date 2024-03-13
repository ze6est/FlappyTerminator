using Assets.FlappyTerminator.CodeBase.Data;
using Assets.FlappyTerminator.CodeBase.Infrastructure.Services.PersistentProgress;
using Assets.FlappyTerminator.CodeBase.Infrastructure.Services.SaveLoad;

namespace Assets.FlappyTerminator.CodeBase.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            _gameStateMachine.Enter<LoadLevelState, string>(_progressService.PlayerProgress.WorldData.PositionOnLevel.Level);
        }

        public void Exit()
        {
            
        }

        private void LoadProgressOrInitNew() => 
            _progressService.PlayerProgress = _saveLoadService.LoadProgress() ?? CreateNewProgress();

        private PlayerProgress CreateNewProgress()
        {
            var progress = new PlayerProgress(initialLevel: "Game", 0);

            progress.PlayerState.MaxHP = 3;
            progress.PlayerState.ResetHP();

            return progress;
        }
    }
}
