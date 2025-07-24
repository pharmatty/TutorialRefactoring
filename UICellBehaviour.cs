
using UnityEngine;
using UnityEngine.UI;

namespace com.marufhow.tictactoe.script
{
    public class UICellBehaviour : MonoBehaviour
    {
        [SerializeField] private Cell cell;
        [SerializeField] private Button button;
        [SerializeField] private Image image;
        [SerializeField] private Color defaultColor;
        [SerializeField] private Color winColor;
        [SerializeField] private Color loseColor;

        private Sprite xSprite;
        private Sprite oSprite;
        private Sprite blankSprite;

        private void Awake()
        {
            xSprite = Resources.Load<Sprite>("x");
            oSprite = Resources.Load<Sprite>("o");
            blankSprite = Resources.Load<Sprite>("b");
        }

        private void Start()
        {
            button.onClick.AddListener(HandleClick);
        }

        private void OnEnable()
        {
            cell.OnValueChanged += UpdateCellVisual;
            cell.OnResultApplied += ApplyResultColor;
        }

        private void OnDisable()
        {
            cell.OnValueChanged -= UpdateCellVisual;
            cell.OnResultApplied -= ApplyResultColor;
        }

        private void HandleClick()
        {
            if (!cell.IsInteractive || cell.Value != 0) return;

            bool isXTurn = TurnManager.Instance.GetAndToggleTurn();
            cell.SetValue(isXTurn ? 1 : 2);
        }

        private void UpdateCellVisual(int id, int value)
        {
            image.sprite = value switch
            {
                1 => xSprite,
                2 => oSprite,
                _ => blankSprite
            };
            image.color = defaultColor;
        }

        private void ApplyResultColor(bool isWinningCell)
        {
            image.color = isWinningCell ? winColor : loseColor;
        }
    }
}
