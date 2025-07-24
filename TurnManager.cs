
using com.marufhow.tictactoe.script.utility;
using UnityEngine;

namespace com.marufhow.tictactoe.script
{
    public class TurnManager : PersistentMonoSingleton<TurnManager>
    {
        private bool isXTurn = true;

        private void Start()
        {
            isXTurn = true;
        }

        public bool GetAndToggleTurn()
        {
            bool currentTurn = isXTurn;
            isXTurn = !isXTurn;
            return currentTurn;
        }

        protected override void Initialize() { }
    }
}
