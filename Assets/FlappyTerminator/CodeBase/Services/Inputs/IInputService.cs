using Assets.FlappyTerminator.CodeBase.Infrastructure.Services;

namespace Assets.FlappyTerminator.CodeBase.Services.Inputs
{
    public interface IInputService : IService
    {
        bool IsJumpButtonDown();
        bool IsFireButtonDown();
    }
}