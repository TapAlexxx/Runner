namespace Scripts.Infrastructure.Services.StateMachine
{
    public interface IPayloadedState<TPayload>
    {
        void Enter(TPayload payload);
    }
}