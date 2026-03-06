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

    private Color bustColor = new Color(0.686f, 0.090f, 0.090f, 1f); // Red
    private Color checkoutColor = new Color(0.094f, 0.439f, 0.075f, 1f); // Green

    private void Start()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("ScoreboardUI: GameManager.Instance is null — check script execution order");
            return;
        }

        GameManager.Instance.OnStateChanged += UpdateDisplay;
        GameManager.Instance.OnBust += ShowBust;
        GameManager.Instance.OnLegComplete += ShowLegComplete;
        GameManager.Instance.OnTurnComplete += ResetState;

        // Populate immediately with starting values

        UpdateDisplay(501, 1, 1, 0);
        ResetState();
    }

    private void OnDestroy()
    {
        if (GameManager.Instance == null) return;
        GameManager.Instance.OnStateChanged -= UpdateDisplay;
        GameManager.Instance.OnBust -= ShowBust;
        GameManager.Instance.OnLegComplete -= ShowLegComplete;
    }

    private void UpdateDisplay(int score, int leg, int turn, int darts)
    {
        scoreText.text = score.ToString();
        legText.text = $"LEG {leg}";
        turnText.text = $"TURN {turn}";
        dartsText.text = $"DART {darts + 1}/3"; // darts goes from 0 - 2
    }

    private void ShowBust()
    {
        statusText.text = "BUST!";
        statusText.color = bustColor;  
    }

    private void ShowLegComplete()
    {
        statusText.text = "LEG COMPLETE!";
        statusText.color = checkoutColor;
    }

    private void ResetState()
    {
        statusText.text = "";
    }
}