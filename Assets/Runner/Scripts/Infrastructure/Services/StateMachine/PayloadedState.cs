namespace Scripts.Infrastructure.Services.StateMachine
{
    public abstract class PayloadedState<T> : IPayloadedState<T>, IExitable
    {
        public abstract void Enter(T param);
        public abstract void Exit();
    }
}