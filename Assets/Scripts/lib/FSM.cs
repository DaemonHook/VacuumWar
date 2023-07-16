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

namespace DHFSM
{
    public class State<Event>
    {
        public Action onUpdate;
        public Action onEnter;
        public Action onExit;
        public Action<Event> onEvent;

        public bool Done { get; private set; }
        public State<Event> NextState { get; private set; }

        public State(Action onUpdate = null, Action onEnter = null,
            Action onExit = null, Action<Event> eventHandler = null)
        {
            this.onUpdate = onUpdate;
            this.onEnter = onEnter;
            this.onExit = onExit;
            this.onEvent = eventHandler;
        }

        public void Enter()
        {
            Done = false;
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

        public void Yield(State<Event> to)
        {
            Done = true;
            NextState = to;
        }
    }

    public class FinateStateMachine<Event>
    {
        protected State<Event> mCurState;

        public void GetEvent(Event e)
        {
            mCurState?.GetEvent(e);
        }

        public void Update()
        {
            if (mCurState != null)
            {
                if (mCurState.Done)
                {
                    mCurState = mCurState.NextState;
                }
                else mCurState.Update();
            }
        }

    }
}
