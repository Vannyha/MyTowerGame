using Context;

namespace Game
{
    public interface IGameManager : IBean
    {
        bool IsGameStarted { get; set; }
    }
}