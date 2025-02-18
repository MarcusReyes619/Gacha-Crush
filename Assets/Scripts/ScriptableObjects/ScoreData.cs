using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(fileName = "Scroe", menuName = "Scriptable Objects/Scroe")]
public class ScroeData : ScriptableObject
{
    public UnityAction<int> score;

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
