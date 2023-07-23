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
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace DHFSM
{
    public class State<Event>
    {
        public FinateStateMachine<Event> fsm;
        public Action onEnter;
        public Action onExit;
        public Action<Event> onEvent;

        // 状态存储
        public Dictionary<string, object> dict = new Dictionary<string, object>();

        // 具体事件之后再绑定
        public State()
        {
            //this.fsm = fsm;
            //this.onUpdate = onUpdate;
            //this.onEnter = onEnter;
            //this.onExit = onExit;
            //this.onEvent = eventHandler;
        }

        public void Init(FinateStateMachine<Event> fsm, Action onEnter, Action onExit, Action<Event> onEvent)
        {
            this.fsm = fsm;
            this.onEnter = onEnter;
            this.onExit = onExit;
            this.onEvent = onEvent;
        }

        public void Enter()
        {
            onEnter?.Invoke();
        }

        public void Exit()
        {
            onExit?.Invoke();
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
            fsm.SwitchState(to);
        }
    }

    public class FinateStateMachine<Event>
    {
        protected State<Event> mCurState;
        
        public void SetInitialState(State<Event> initialState)
        {
            mCurState = initialState;
            initialState.Enter();
        }

        public void GetEvent(Event e)
        {
            mCurState?.GetEvent(e);
        }

        public void SwitchState(State<Event> nextState)
        {
            mCurState.Exit();
            mCurState = nextState;
            nextState.Enter();
        }
    }
}
