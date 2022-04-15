using Context;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour, IGameManager
    {
        private bool isGameStarted = false;

        public bool IsGameStarted { get; set; }

        public void SetupBeans(GameContext context)
        {
            //
        }
    }
}