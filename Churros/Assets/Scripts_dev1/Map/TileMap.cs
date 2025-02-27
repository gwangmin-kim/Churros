using TMPro;
using UnityEngine;

public class TileMap : MonoBehaviour
{
	// 타일맵은 하나만 존재한다고 가정(사실상 static class임)
	// 위치를 찍기 위한 raycast에 사용할 레이어마스크
	public static LayerMask layer;
	// 타일맵 원점
	private static Vector3 _origin;
	// 타일 간격
	[SerializeField] private float _tileInterval;
	public static float tileInterval;

	private void Start()
	{
		// 타일맵이 움직이면 안됨
		gameObject.isStatic = true;
		layer = LayerMask.GetMask("TileMap");
		_origin = transform.position;
		tileInterval = _tileInterval;
	}

	// 임의의 좌표를 가장 가까운 타일맵 좌표로 변환
	public static Vector3 GetAlignedPosition(Vector3 position, Vector2 size)
	{
		// Tilemap 원점에 대한 상대좌표
		Vector2 relativePosition = new Vector2(position.x - _origin.x, position.z - _origin.z);

		Vector2 coordinate = CalculateCoordinate(relativePosition, size);

		return _origin + new Vector3(coordinate.x, 0, coordinate.y);
	}

	// 위치와 크기를 받아서 타일맵 기준 좌표를 계산
	private static Vector2 CalculateCoordinate(Vector2 position, Vector2 size)
	{
		Vector2 offset = CalculateOffset(size);
		Vector2 adjustedPosition = position - offset;
		Vector2 coordinate = new Vector2(CalculateLengthByTile(adjustedPosition.x), CalculateLengthByTile(adjustedPosition.y));
		Vector2 adjustedCoordinate = coordinate + offset;
		return adjustedCoordinate;
	}

	// 길이를 받아 타일 간격의 배수에 맞취 반올림
	private static float CalculateLengthByTile(float length)
	{
		return Mathf.RoundToInt(length / tileInterval) * tileInterval;
	}

	// 크기에 따라 타일맵에서 보정할 좌표값 계산
	// 짝수 길이면 타일 반 개 길이만큼 보정해야 함
	private static Vector2 CalculateOffset(Vector2 size)
	{
		float offsetX = ((int)size.x) % 2 != 0 ? 0 : tileInterval / 2;
		float offsetY = ((int)size.y) % 2 != 0 ? 0 : tileInterval / 2;

		return new Vector2(offsetX, offsetY);
	}
}