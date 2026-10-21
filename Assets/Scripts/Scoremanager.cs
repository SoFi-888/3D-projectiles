using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public TMP_Text scoreText;

    int kills;
    int score;

    void Awake() { Instance = this; UpdateUI(); }

    public void AddKill()
    {
        kills++;
        score += PointsForKill(kills);
        UpdateUI();
    }

    // Kills 1-2: +1 | Kills 3-5: +2 | Kill 6 onward: +4
    int PointsForKill(int killNumber)
    {
        if (killNumber >= 6) return 4;
        if (killNumber >= 3) return 2;
        return 1;
    }

    void UpdateUI()
    {
        if (scoreText != null) scoreText.text = $"Score: {score}\nKills: {kills}";
    }
}