using Assets.FlappyTerminator.CodeBase.Services.Inputs;

namespace Assets.FlappyTerminator.CodeBase.Infrastructure.AssetManagement
{
    public class Game
    {
        public static IInputService InputService;

        public GameStateMachine GameStateMachine;

        public Game(ICoroutineRunner coroutineRunner) 
        {
            GameStateMachine = new GameStateMachine(new SceneLoader(coroutineRunner));
        }
    }
}