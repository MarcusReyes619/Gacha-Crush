using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(fileName = "Score", menuName = "Scriptable Objects/Score")]
public class ScoreData : ScriptableObject
{
    [SerializeField] private int scoreValue = 0; // This stores the actual score
    [SerializeField] public UnityAction<int> onScoreChanged;

    public void Add(int value)
    {
        scoreValue += value; // Increase the stored score
        onScoreChanged?.Invoke(scoreValue); // Notify listeners
    }
    public int GetScore() 
    {
        return scoreValue;
    }
    public void Subscribe(UnityAction<int> function)
    {
        onScoreChanged += function;
    }


    public void UnSubscribe(UnityAction<int> function)
    {
        onScoreChanged -= function;
    }


}
