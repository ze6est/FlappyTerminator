using Assets.FlappyTerminator.CodeBase.Data;

namespace Assets.FlappyTerminator.CodeBase.Infrastructure.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}
