using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(fileName = "Score", menuName = "Scriptable Objects/Score")]
public class ScoreData : ScriptableObject
{
    [SerializeField] public UnityAction<int> score;

    public void Add(int value)
    {
        score?.Invoke(value);
    }

    public void Subscribe(UnityAction<int> function)
    {
        score += function;
    }


    public void UnSubscribe(UnityAction<int> function)
    {
        score -= function;
    }

   
}
