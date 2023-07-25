using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

// 单位显示接口
public interface IUnitDisplay
{
    // 是否空闲
    bool IsIdle();

    void SetPosition(Vector2Int position);

    // 移动至地点（逻辑）
    void MoveTo(Vector2Int position);

    void ShowAttackEffect();
    
    // 刷新状态显示
    void RefreshStatus(UnitStatus status);
    
    // 刷新可用性（当前是否可以行动）
    void TriggerAvailability(bool availability);
}
