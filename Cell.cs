
using System;
using UnityEngine;

namespace com.marufhow.tictactoe.script
{
    [CreateAssetMenu(fileName = "Cell", menuName = "Game/Cell")]
    public class Cell : ScriptableObject
    {
        public int Id;
        public int Value { get; private set; }
        public bool IsInteractive { get; private set; }

        public event Action<int, int> OnValueChanged;
        public event Action<bool> OnResultApplied;

        public void SetValue(int newValue)
        {
            Value = newValue;
            OnValueChanged?.Invoke(Id, Value);
        }

        public void ApplyResult(bool isWinningCell)
        {
            IsInteractive = false;
            OnResultApplied?.Invoke(isWinningCell);
        }

        public void Reset()
        {
            Value = 0;
            IsInteractive = true;
            OnValueChanged?.Invoke(Id, Value);
        }
    }
}
