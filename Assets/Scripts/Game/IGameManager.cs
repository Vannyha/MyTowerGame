using Context;

namespace Game
{
    public interface IGameManager : IInitResolve
    {
        bool IsGameStarted { get; }
        void StartGame();
        void StopGame();
        void FinishGame();
    }
}