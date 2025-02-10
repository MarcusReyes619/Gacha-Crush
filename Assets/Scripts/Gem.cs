using UnityEngine;
using UnityEngine.UI;

public enum GemType { Blue, Green, Orange, Purple, Red, Teal }

public class Gem : MonoBehaviour
{
	

	[SerializeField] private GemType gemType;
	[SerializeField] private SpriteRenderer sprite;
	
	private void Start()
	{
		
	}
}
