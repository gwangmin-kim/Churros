using Unity.VisualScripting;
using UnityEngine;

public abstract class ObjectController : MonoBehaviour
{
	// 타일맵에서 차지하는 가로세로 칸 수
	public Vector2 size;

	// 회전 시 가로세로 크기가 변경됨
	public void SwapSize()
	{
		float temp = size.x;
		size.x = size.y;
		size.y = temp;
	} 
}
