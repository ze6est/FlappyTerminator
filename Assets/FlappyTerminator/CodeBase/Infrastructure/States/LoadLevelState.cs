using Assets.FlappyTerminator.CodeBase.Infrastructure.Factory;
using Assets.FlappyTerminator.CodeBase.Infrastructure.Services.PersistentProgress;
using Assets.FlappyTerminator.CodeBase.Logic;
using Assets.FlappyTerminator.CodeBase.Logic.Player;
using Assets.FlappyTerminator.CodeBase.Player;
using Assets.FlappyTerminator.CodeBase.UI;
using UnityEngine;

namespace Assets.FlappyTerminator.CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPointTag = "InitialPoint";

        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;        
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;        

        private PlayerMover _playerMover;
        private PlayerShooter _playerShooter;
        private PlayerDeath _playerDeath;
        private BulletSpawner _bulletSpawner;
        private EnemySpawner _enemySpawner;
        private Window _window;

        private bool _isNotRestart = true;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, IGameFactory gameFactory, IPersistentProgressService progressService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;            
            _gameFactory = gameFactory;
            _progressService = progressService;            
        }

        public void Enter(string sceneName)
        {
            if (_isNotRestart)
                LoadLevel(sceneName);
            else
                RestartLevel();
        }

        public void Exit()
        {
            
        }

        private void LoadLevel(string sceneName)
        {
            _gameFactory.Cleanup();
            _sceneLoader.Load(sceneName, OnLoaded);
            Time.timeScale = 0;
        }

        private void OnLoaded()
        {            
            InitGameWorld();
            InformProgressReaders();                                 
        }

        private void InitGameWorld()
        {
            _isNotRestart = false;

            GameObject player = _gameFactory.CreatePlayer(at: GameObject.FindWithTag(InitialPointTag));
            _playerMover = player.GetComponent<PlayerMover>();
            _playerMover.enabled = false;

            ScoreCounter scoreCounter = _gameFactory.CreateScoreCounter().GetComponent<ScoreCounter>();

            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            playerHealth.Construct(scoreCounter);

            GameObject hud = _gameFactory.CreateHUD();
            hud.GetComponentInChildren<ActorUI>().Construct(playerHealth, scoreCounter);

            _bulletSpawner = _gameFactory.CreateBulletSpawner().GetComponent<BulletSpawner>();
            _bulletSpawner.Construct(_gameFactory, scoreCounter);

            _enemySpawner = _gameFactory.CreateEnemySpawner().GetComponent<EnemySpawner>();
            _enemySpawner.Construct(_gameFactory, _bulletSpawner, player);

            Destroyer destroyer = _gameFactory.CreateDestroyer().GetComponent<Destroyer>();
            TargetTracker tracker = destroyer.GetComponent<TargetTracker>();
            tracker.Follow(player);

            _playerShooter = player.GetComponent<PlayerShooter>();
            _playerShooter.Construct(_bulletSpawner);
            _playerShooter.enabled = false;

            _playerDeath = player.GetComponent<PlayerDeath>();            

            scoreCounter.Construct(_playerDeath);

            _window = hud.GetComponentInChildren<Window>();

            CameraFollow(player);

            StartButton startButton = hud.GetComponentInChildren<StartButton>();
            startButton.GameStarted += OnGameStarted;
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)            
                progressReader.LoadProgress(_progressService.PlayerProgress);            
        }

        private void CameraFollow(GameObject player) =>
            Camera.main.GetComponent<TargetTracker>().Follow(player);
        
        private void RestartLevel()
        {
            Time.timeScale = 0;
            InformProgressReaders();
            InformRestarters();
        }

        private void InformRestarters()
        {
            foreach (IRestarter restarter in _gameFactory.Restarters)
                restarter.Restart();
        }

        private void OnGameStarted()
        {
            _window.gameObject.SetActive(false);

            _playerMover.enabled = true;
            _playerShooter.enabled = true;

            _gameStateMachine.Enter<GameLoopState, PlayerDeath>(_playerDeath);            
        }
    }
}