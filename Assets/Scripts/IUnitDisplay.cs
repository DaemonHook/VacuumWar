using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

// 单位显示接口
public interface IUnitDisplay
{
    bool IsIdle();
    void MoveTo(Vector2Int position);
    //void 
}
