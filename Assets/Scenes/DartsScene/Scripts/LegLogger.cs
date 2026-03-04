using System.IO;
using UnityEngine;

public class LegLogger
{
    private string filePath;

    public LegLogger()
    {
        filePath = Path.Combine(Application.persistentDataPath, "legs_log.csv");

        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "Leg,TurnsUsed,TotalThrows,Busts\n");
        }
    }

    public void LogLeg(int leg, int turnsUsed, int totalThrows, int busts)
    {
        string entry = $"{leg},{turnsUsed},{totalThrows},{busts}\n";
        File.AppendAllText(filePath, entry);
    }
}