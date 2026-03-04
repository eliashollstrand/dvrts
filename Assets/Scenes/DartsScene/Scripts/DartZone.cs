using UnityEngine;

public enum zoneType
{
    Single,
    Double,
    Triple,
    Missed
}

public class DartZone : MonoBehaviour
{
    public int score = 0;
    public zoneType type;

    void Awake()
    {
        SetScoreFromName();
    }

    void SetScoreFromName()
    {
        // Expecting names like "Single_4", "Double_12", "Triple_2", "Bull", "OuterBull"
        string name = gameObject.name;

        if (name.StartsWith("Outside"))
        {
            score = 0;
        }
        if (name.StartsWith("Bullseye"))
        {
            score = 50; // Bullseye
        }
        else if (name.StartsWith("Outer_Bull"))
        {
            score = 25; // Outer bull
        }
        else
        {
            // Split "Multiplier_Number" -> ["Multiplier", "Number"]
            string[] parts = name.Split('_');
            if (parts.Length == 2)
            {
                Debug.Log("Hit zone: " + parts[0]);
                int baseValue = int.Parse(parts[1]); // e.g., 4, 12, 20
                switch (parts[0])
                {
                    case "Single":
                        score = baseValue;
                        type = zoneType.Single;
                        break;
                    case "Double":
                        score = baseValue * 2;
                        type = zoneType.Double;
                        break;
                    case "Triple":
                        score = baseValue * 3;
                        type = zoneType.Triple;
                        break;
                    default:
                        Debug.LogWarning("Unknown prefix in dart zone name: " + name);
                        score = baseValue;
                        break;
                }
            }
            else
            {
                Debug.LogWarning("Unexpected dart zone name format: " + name);
            }
        }
    }
}