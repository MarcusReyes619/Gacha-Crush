using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "ScriptableObject/Highscore", order = 1)]
public class HighScoreData : ScriptableObject
{
    public int CurrentScore;
    public int HighScore;
}
