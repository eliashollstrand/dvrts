using System.IO;
using UnityEngine;

public class ThrowLogger
{
    private string filePath;

    public ThrowLogger()
    {
        filePath = Path.Combine(Application.persistentDataPath, "throws_log.csv");

        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "Leg,Turn,DartIndex,Throw,ScoreBefore,Score,ZoneType\n");
        }
    }

    public void LogThrow(
        int leg,
        int turn,
        int dartIndex,
        int throwNumber,
        int scoreBefore,
        int score,
        zoneType zoneType)
    {
        string entry =
            $"{leg},{turn},{dartIndex},{throwNumber},{scoreBefore},{score},{zoneType}\n";

        File.AppendAllText(filePath, entry);
    }
}