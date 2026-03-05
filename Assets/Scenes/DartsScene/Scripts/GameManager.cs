using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // UI subscribes to these
    public event Action<int, int, int, int> OnScoreChanged; // score, leg, turn, dartsThisTurn
    public event Action OnBust;
    public event Action OnLegComplete;

    private int currentLeg = 1;
    // private int currentScore = 501;
    private int currentScore = 100; // Debugging
    private int turnNumber = 1;
    private int throwNumber = 0;
    private int dartsThisTurn = 0;
    private int startingScoreThisTurn;
    private int bustCount = 0;
    private ThrowLogger throwLogger;
    private LegLogger legLogger;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        throwLogger = new ThrowLogger();
        legLogger = new LegLogger();
    }

    public void RegisterThrow(int score, zoneType zoneType)
    {
        if (dartsThisTurn == 0)
            startingScoreThisTurn = currentScore;

        dartsThisTurn++;
        throwNumber++;
        int dartIndex = dartsThisTurn;

        throwLogger.LogThrow(currentLeg, turnNumber, dartIndex, throwNumber, currentScore, score, zoneType);

        int newScore = currentScore - score;

        if (newScore < 0 || newScore == 1)
        {
            HandleBust();
            return;
        }

        bool isDouble = zoneType == zoneType.Double;

        if (newScore == 0)
        {
            if (!isDouble)
            {
                HandleBust();
                return;
            }

            legLogger.LogLeg(currentLeg, turnNumber, throwNumber, bustCount);
            OnLegComplete?.Invoke();
            StartNewLeg();
            return;
        }

        currentScore = newScore;
        OnScoreChanged?.Invoke(currentScore, currentLeg, turnNumber, dartsThisTurn);

        if (dartsThisTurn >= 3)
            EndTurn();
    }

    private void HandleBust()
    {
        bustCount++;
        currentScore = startingScoreThisTurn;
        OnBust?.Invoke();
        OnScoreChanged?.Invoke(currentScore, currentLeg, turnNumber, dartsThisTurn);
        EndTurn();
    }

    private void EndTurn()
    {
        dartsThisTurn = 0;
        turnNumber++;
    }

    private void StartNewLeg()
    {
        currentLeg++;
        currentScore = 501;
        turnNumber = 1;
        throwNumber = 0;
        dartsThisTurn = 0;
        bustCount = 0;
        OnScoreChanged?.Invoke(currentScore, currentLeg, turnNumber, dartsThisTurn);
    }
}