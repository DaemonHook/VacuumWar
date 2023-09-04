/*
 * file: GroundController.cs
 * author: D.H.
 * feature: 地表控制
 */

using UnityEngine;
using System.Collections;

public class GroundController : MonoBehaviour
{
	// 地形高度
	public int height;

	private Vector2Int logicPosition;

	private IGroundDisplay display;

	public void Init(Vector2Int position)
	{
		display = GetComponent<IGroundDisplay>();
		logicPosition = position;
		display.SetPosition(logicPosition);
	}

	// Use this for initialization
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
			
	}
}

