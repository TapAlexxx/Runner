using System;
using System.Collections.Generic;

namespace Scripts.Infrastructure.Services.StateMachine
{
    public class GameStateMachine
    {
        private Dictionary<Type, IExitable> states;
        private IExitable currentState;

        public void Initialize() => 
            states = new Dictionary<Type, IExitable>();

        public void AddState<TState>(TState state) where TState : IExitable
        {
            states.Add(typeof(TState), state);
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : PayloadedState<TPayload>, new()
        {
            PayloadedState<TPayload> newState = states[typeof(TState)] as PayloadedState<TPayload>;
            if (newState == null) 
                return;

            if (currentState != null)
            {
                ((PayloadedState<TPayload>)currentState).Exit();
            }

            newState.Enter(payload);
            currentState = newState;
        }
        
        public void Enter<TState>() where TState : State, new()
        {
            State newState = states[typeof(TState)] as State;
            if (newState == null) 
                return;

            if (currentState != null)
            {
                ((State)currentState).Exit();
            }

            newState.Enter();
            currentState = newState;
        }
    }
}