using Assets.FlappyTerminator.CodeBase.Services.Inputs;
using UnityEngine;

namespace Assets.FlappyTerminator.CodeBase.Infrastructure.AssetManagement
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";

        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            RegisterServices();
            _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
        }

        public void Exit()
        {
            
        }

        private void RegisterServices()
        {
            Game.InputService = RegisterInputService();
        }

        private void EnterLoadLevel() => 
            _gameStateMachine.Enter<LoadLevelState, string>("Game");

        private static IInputService RegisterInputService()
        {
            if (Application.isMobilePlatform)
                return new MobileInputService();
            else
                return new StandaloneInputService();
        }
    }
}
