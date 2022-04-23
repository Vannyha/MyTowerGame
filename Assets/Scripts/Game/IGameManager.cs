using Context;

namespace Game
{
    public interface IGameManager : IBean
    {
        bool IsGameStarted { get; }
        void StartGame();
        void StopGame();
    }
}