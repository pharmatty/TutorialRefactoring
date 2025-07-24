
using com.marufhow.tictactoe.script.utility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace com.marufhow.tictactoe.script
{
    public class UIBehaviour : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI gameStatusText;
        [SerializeField] private Button restartButton;
        [SerializeField] private GameObject resultPanel;

        private void OnEnable()
        {
            BoardManager.Instance.OnGameFinished += DisplayResult;
            BoardManager.Instance.OnReset += ResetUI;
        }

        private void OnDisable()
        {
            BoardManager.Instance.OnGameFinished -= DisplayResult;
            BoardManager.Instance.OnReset -= ResetUI;
        }

        private void Start()
        {
            restartButton.onClick.AddListener(() => BoardManager.Instance.ResetGame());
        }

        private void DisplayResult(int winner, bool isWin)
        {
            gameStatusText.text = isWin 
                ? $"{(winner == 1 ? "X" : "O")} Player Wins!"
                : "It's a Draw! Try Again.";

            resultPanel.SetActive(true);
        }

        private void ResetUI()
        {
            gameStatusText.text = string.Empty;
            resultPanel.SetActive(false);
        }
    }
}
