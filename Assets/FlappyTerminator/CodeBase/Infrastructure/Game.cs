using Assets.FlappyTerminator.CodeBase.Infrastructure.Services;
using Assets.FlappyTerminator.CodeBase.Infrastructure.States;

namespace Assets.FlappyTerminator.CodeBase.Infrastructure
{
    public class Game
    {
        public GameStateMachine GameStateMachine;

        public Game(ICoroutineRunner coroutineRunner)
        {
            GameStateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), AllServices.Container);
        }
    }
}