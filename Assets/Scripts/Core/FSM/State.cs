using System;
using Timer = System.Diagnostics;

namespace DungeonCrawler.Core.FSM
{
    internal class State<TID> : BaseState<TID> where TID : IComparable
    {
        protected readonly Action<State<TID>> onEnter;
        protected readonly Action<State<TID>> onUpdate;
        protected readonly Action<State<TID>> onFixedUpdate;
        protected readonly Action<State<TID>> onExit;
        protected readonly Func<State<TID>, bool> canExit;

        protected FiniteStateMachine<TID> fsm;
        protected readonly Timer.Stopwatch timer;

        public State(
            Action<State<TID>> onEnter = null,
            Action<State<TID>> onUpdate = null,
            Action<State<TID>> onFixedUpdate = null,
            Action<State<TID>> onExit = null,
            Func<State<TID>, bool> canExit = null,
            bool requireTimeToExit = false,
            bool instantTransition = false
        ) : base(requireTimeToExit, instantTransition)
        {
            this.onEnter = onEnter;
            this.onUpdate = onUpdate;
            this.onExit = onExit;
            this.canExit = canExit;

            this.timer = new Timer.Stopwatch();
        }

        public virtual void SetParnetStateMachine(FiniteStateMachine<TID> fsm) => this.fsm = fsm;

        public virtual int GetElapsedTime() => timer.Elapsed.Milliseconds;

        public override void Enter()
        {
            timer.Reset();
            timer.Start();
            onEnter?.Invoke(this);
        }

        public override void Update()
        {
            if (
                requireTimeToExit &&
                canExit != null &&
                fsm != null &&
                fsm.HasPendingTransition() &&
                canExit(this)
            ) fsm.StateCanExit();
            else onUpdate?.Invoke(this);
        }

        public override void FixedUpdate()
        {
            onFixedUpdate?.Invoke(this);
        }

        public override void Exit()
        {
            onExit?.Invoke(this);
        }

        public override void ExitRequest()
        {
            if (canExit != null && canExit(this)) fsm.StateCanExit();
        }
    }

    internal class State : State<string>
    {
        public State(
            Action<State<string>> onEnter = null,
            Action<State<string>> onUpdate = null,
            Action<State<string>> onFixedUpdate = null,
            Action<State<string>> onExit = null,
            Func<State<string>, bool> canExit = null,
            bool requireTimeToExit = false,
            bool instantTransition = false
        ) : base(onEnter, onUpdate, onFixedUpdate, onExit, canExit, requireTimeToExit, instantTransition) { }
    }
}
