using System.Collections.Generic;
using UnityEngine;

public class Item
{
	public int id;
	public string name;
	
	public Item(string name)
	{
		this.name = name;
		id = ItemIDGenerator.GetID(name);
	}

	public Item(Item item)
	{
		id = item.id;
		name = item.name;
	}

	// 비교 연산
	public static bool operator == (Item lhs, Item rhs)
	{
		if (lhs is null && rhs is null) 
		{ 
			return true; 
		}
		if (lhs is null || rhs is null)
		{
			return false;
		}
		return lhs.id == rhs.id;
	}

	public static bool operator != (Item lhs, Item rhs)
	{
		return !(lhs == rhs);
	}

	public override bool Equals(object obj)
	{
		if (obj is Item other)
		{
			return this == other;
		}
		return false;
	}

	public override int GetHashCode()
	{
		return id.GetHashCode();
	}
}

public static class ItemIDGenerator
{
	private static Dictionary<string, int> _idMap = new Dictionary<string, int>();
	private static int _nextID = 1;

	public static int GetID(string name)
	{
		if (!_idMap.ContainsKey(name))
		{
			_idMap[name] = _nextID++;
		}
		return _idMap[name];
	}
}