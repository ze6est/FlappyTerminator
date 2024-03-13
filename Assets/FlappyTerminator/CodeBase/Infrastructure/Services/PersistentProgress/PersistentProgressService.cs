using Assets.FlappyTerminator.CodeBase.Data;

namespace Assets.FlappyTerminator.CodeBase.Infrastructure.Services.PersistentProgress
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public PlayerProgress PlayerProgress { get; set; }
    }
}