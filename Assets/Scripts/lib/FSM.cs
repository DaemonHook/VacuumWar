/*
 * file: StateMachine.cs
 * author: D.H.
 * feature: 有限状态机实现
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DHFSM
{
    public class State<Event>
    {
        public FinateStateMachine<Event> fsm;
        public Action onUpdate;
        public Action onEnter;
        public Action onExit;
        public Action<Event> onEvent;

        // 状态存储
        public Dictionary<string, object> dict = new Dictionary<string, object>();

        public State(FinateStateMachine<Event> fsm, Action onUpdate = null, Action onEnter = null,
            Action onExit = null, Action<Event> eventHandler = null)
        {
            this.fsm = fsm;
            this.onUpdate = onUpdate;
            this.onEnter = onEnter;
            this.onExit = onExit;
            this.onEvent = eventHandler;
        }

        public void Enter()
        {
            onEnter?.Invoke();
        }

        public void Exit()
        {
            onExit?.Invoke();
        }

        public void Update()
        {
            onUpdate?.Invoke();
        }

        public void GetEvent(Event e)
        {
            onEvent?.Invoke(e);
        }

        /// <summary>
        /// 转换状态
        /// </summary>
        /// <param name="to"></param>
        public void Yield(State<Event> to)
        {
            
        }
    }

    public class FinateStateMachine<Event>
    {
        protected State<Event> mCurState;

        public FinateStateMachine(State<Event> initialState)
        {
            mCurState = initialState;
            initialState.Enter();
        }

        public void GetEvent(Event e)
        {
            mCurState?.GetEvent(e);
        }
        
        public void SwitchState(State<Event> curState)
        {
            if (curState != this.mCurState)
            {
                Debug.LogError($"GetYield: 当前状态错误!");
            }
            curState.Exit();
            mCurState = curState;
        }
    }
}
