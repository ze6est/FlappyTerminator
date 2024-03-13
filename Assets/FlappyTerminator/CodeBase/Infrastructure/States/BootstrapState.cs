using Assets.FlappyTerminator.CodeBase.Infrastructure.AssetManagement;
using Assets.FlappyTerminator.CodeBase.Infrastructure.Factory;
using Assets.FlappyTerminator.CodeBase.Infrastructure.Services;
using Assets.FlappyTerminator.CodeBase.Infrastructure.Services.PersistentProgress;
using Assets.FlappyTerminator.CodeBase.Infrastructure.Services.SaveLoad;
using Assets.FlappyTerminator.CodeBase.Services.Inputs;
using UnityEngine;

namespace Assets.FlappyTerminator.CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";

        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }

        public void Enter()
        {            
            _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
        }

        public void Exit()
        {

        }

        private void RegisterServices()
        {            
            _services.RegisterSingle<IInputService>(GetInputService());
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssetProvider>()));
            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IPersistentProgressService>(), _services.Single<IGameFactory>()));            
        }

        private void EnterLoadLevel() =>
            _gameStateMachine.Enter<LoadProgressState>();

        private IInputService GetInputService()
        {
            if (Application.isMobilePlatform)
                return new MobileInputService();
            else
                return new StandaloneInputService();
        }
    }
}