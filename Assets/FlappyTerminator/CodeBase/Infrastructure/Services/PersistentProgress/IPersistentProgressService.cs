using Assets.FlappyTerminator.CodeBase.Data;

namespace Assets.FlappyTerminator.CodeBase.Infrastructure.Services.PersistentProgress
{
    public interface IPersistentProgressService : IService
    {
        PlayerProgress PlayerProgress { get; set; }
    }
}