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
    // 移动至
    void MoveTo(Vector2Int position);

    void ShowAttackEffect();
    //void 
}
