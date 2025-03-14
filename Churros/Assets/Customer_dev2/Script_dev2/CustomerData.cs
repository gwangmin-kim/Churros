using UnityEngine;
using UnityEngine.PlayerLoop;

[CreateAssetMenu(fileName = "New Customer", menuName = "Game/Customer")]


public class CustomerData : ScriptableObject
{
	public string[] _foodOptions;
	public float _minWaitTime;
	public float _maxWaitTime;

	public string GetRandomFood()
	{
		return _foodOptions[Random.Range(0, _foodOptions.Length)];

	}

	public float GetRandomWaitTime()
	{
		return Random.Range(_minWaitTime, _maxWaitTime);
	}


}
