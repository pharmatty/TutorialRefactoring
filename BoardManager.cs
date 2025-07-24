
using System;
using System.Collections.Generic;
using com.marufhow.tictactoe.script.utility;
using UnityEngine;

namespace com.marufhow.tictactoe.script
{
    public class BoardManager : PersistentMonoSingleton<BoardManager>
    {
        public event Action<int, bool> OnGameFinished;
        public event Action OnReset;

        [SerializeField] private List<Cell> cells = new();

        private readonly int[][] winPatterns =
        {
            new[] {0, 1, 2}, new[] {3, 4, 5}, new[] {6, 7, 8},
            new[] {0, 3, 6}, new[] {1, 4, 7}, new[] {2, 5, 8},
            new[] {0, 4, 8}, new[] {2, 4, 6}
        };

        private void Start() => ResetGame();

        private void OnEnable()
        {
            foreach (var cell in cells)
                cell.OnValueChanged += EvaluateBoard;
        }

        private void OnDisable()
        {
            foreach (var cell in cells)
                cell.OnValueChanged -= EvaluateBoard;
        }

        private void EvaluateBoard(int cellId, int value)
        {
            foreach (var pattern in winPatterns)
            {
                if (cells[pattern[0]].Value == value &&
                    cells[pattern[1]].Value == value &&
                    cells[pattern[2]].Value == value &&
                    value != 0)
                {
                    foreach (var i in pattern)
                        cells[i].ApplyResult(true);

                    for (int i = 0; i < cells.Count; i++)
                        if (Array.IndexOf(pattern, i) < 0)
                            cells[i].ApplyResult(false);

                    OnGameFinished?.Invoke(value, true);
                    return;
                }
            }

            bool allFilled = cells.TrueForAll(c => c.Value != 0);
            if (allFilled)
            {
                foreach (var cell in cells)
                    cell.ApplyResult(false);

                OnGameFinished?.Invoke(0, false);
            }
        }

        public void ResetGame()
        {
            foreach (var cell in cells)
                cell.Reset();

            OnReset?.Invoke();
        }

        protected override void Initialize() { }
    }
}
