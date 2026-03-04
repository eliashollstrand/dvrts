using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int currentLeg = 1;
    private int currentScore = 501;

    private int turnNumber = 1;
    private int throwNumber = 0;
    private int dartsThisTurn = 0;

    private int startingScoreThisTurn;
    private int bustCount = 0;

    private ThrowLogger throwLogger;
    private LegLogger legLogger;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        throwLogger = new ThrowLogger();
        legLogger = new LegLogger();
    }

    public void RegisterThrow(int score, zoneType zoneType)
    {
        // Start of new turn
        if (dartsThisTurn == 0)
            startingScoreThisTurn = currentScore;

        dartsThisTurn++;
        throwNumber++;

        int dartIndex = dartsThisTurn;

        // Log throw
        throwLogger.LogThrow(
            currentLeg,
            turnNumber,
            dartIndex,
            throwNumber,
            currentScore,
            score,
            zoneType
        );

        int newScore = currentScore - score;

        // Bust conditions
        if (newScore < 0 || newScore == 1)
        {
            HandleBust();
            return;
        }

        bool isDouble = false;
        if(zoneType == zoneType.Double) isDouble = true;

        // Attempted checkout
        if (newScore == 0)
        {
            if (!isDouble)
            {
                Debug.Log("Must finish on a double — bust");
                HandleBust();
                return;
            }

            // Legit double-out
            Debug.Log($"Leg {currentLeg} finished on a double");

            legLogger.LogLeg(
                currentLeg,
                turnNumber,
                throwNumber,
                bustCount
            );

            StartNewLeg();
            return;
        }

        currentScore = newScore;

        // End turn after 3 darts
        if (dartsThisTurn >= 3)
            EndTurn();
    }

    private void HandleBust()
    {
        Debug.Log("Bust");

        bustCount++;
        currentScore = startingScoreThisTurn;

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
    }
}