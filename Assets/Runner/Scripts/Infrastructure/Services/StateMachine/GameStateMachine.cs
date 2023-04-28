using System;
using System.Collections.Generic;
using Scripts.Infrastructure.Services.StateMachine.States;
using UnityEngine;

namespace Scripts.Infrastructure.Services.StateMachine
{
    public class GameStateMachine
    {
        private Dictionary<Type, object> states;
        private object currentState;

        public void Initialize() => 
            states = new Dictionary<Type, object>();

        public void AddState<TState>(TState state) => 
            states.Add(typeof(TState), state);

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
    }
}