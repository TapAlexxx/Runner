namespace Scripts.Infrastructure.Services.StateMachine
{
    public abstract class State : IState, IExitable
    {
        public abstract void Enter();

        public abstract void Exit();
    }
}