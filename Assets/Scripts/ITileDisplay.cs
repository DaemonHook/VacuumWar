using System;
using UnityEngine;

// 控制瓦片显示状态的接口
public interface ITileDisplay
{
    // 进入/退出选中模式
    void TriggerSelectedMode(bool status);
    // 设置移动模式
    void TriggerMoveStatusMode(MoveStatus moveStatus);
    // 绑定瓦片控制器
    void BindController(TileController controller);

    void Init(Vector2Int position);
}
