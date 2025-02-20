using UnityEngine;

public class Container : MonoBehaviour, IContainer
{
	private Item _item;

	public void Interact(PlayerStatus playerStatus)
	{
		playerStatus.Get(TakeOut());
	}

	public Item TakeOut()
	{
		return new Item(_item);
	}
}
