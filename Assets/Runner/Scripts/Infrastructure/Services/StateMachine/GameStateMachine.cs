using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Infrastructure.Services.StateMachine
{
    public class GameStateMachine : MonoBehaviour
    {
        private Dictionary<Type, IExitable> _states;
        private IExitable _currentState;

        public void Initialize() => 
            _states = new Dictionary<Type, IExitable>();

        public void BindState<TState>(TState state) where TState : IExitable
        {
            _states.Add(typeof(TState), state);
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : PayloadedState<TPayload>
        {
            PayloadedState<TPayload> newState = _states[typeof(TState)] as PayloadedState<TPayload>;
            if (newState == null) 
                return;

            if (_currentState != null) 
                _currentState.Exit();

            newState.Enter(payload);
            _currentState = newState;
        }
        
        public void Enter<TState>() where TState : State
        {
            State newState = _states[typeof(TState)] as State;
            if (newState == null) 
                return;
            
            if (_currentState != null) 
                _currentState.Exit();

            newState.Enter();
            _currentState = newState;
        }
    }
}