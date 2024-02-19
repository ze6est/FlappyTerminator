using Assets.CodeBase.Logic;
using Assets.FlappyTerminator.CodeBase.Services.Inputs;

namespace Assets.FlappyTerminator.CodeBase.Infrastructure.AssetManagement
{
    public class Game
    {
        public static IInputService InputService;

        public GameStateMachine GameStateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain) 
        {
            GameStateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain);
        }
    }
}