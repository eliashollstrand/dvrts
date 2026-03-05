using UnityEngine;
using TMPro;

public class ScoreboardUI : MonoBehaviour
{
    [Header("Text Fields")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI legText;
    [SerializeField] private TextMeshProUGUI turnText;
    [SerializeField] private TextMeshProUGUI dartsText;
    [SerializeField] private TextMeshProUGUI statusText;

    private void Start()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("ScoreboardUI: GameManager.Instance is null — check script execution order");
            return;
        }

        GameManager.Instance.OnScoreChanged += UpdateDisplay;
        GameManager.Instance.OnBust += ShowBust;
        GameManager.Instance.OnLegComplete += ShowLegComplete;

        // Populate immediately with starting values
        UpdateDisplay(501, 1, 1, 0);
        statusText.text = "";
    }

    private void OnDestroy()
    {
        if (GameManager.Instance == null) return;
        GameManager.Instance.OnScoreChanged -= UpdateDisplay;
        GameManager.Instance.OnBust -= ShowBust;
        GameManager.Instance.OnLegComplete -= ShowLegComplete;
    }

    private void UpdateDisplay(int score, int leg, int turn, int darts)
    {
        scoreText.text = score.ToString();
        legText.text = $"Leg {leg}";
        turnText.text = $"Turn {turn}";
        dartsText.text = $"Darts {darts}/3";
        statusText.text = "";
    }

    private void ShowBust() => statusText.text = "BUST";
    private void ShowLegComplete() => statusText.text = "LEG WON!";
}