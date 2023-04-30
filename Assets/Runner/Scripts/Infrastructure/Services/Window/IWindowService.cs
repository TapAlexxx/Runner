using Scripts.Infrastructure.Services.Factories.Game;
using Scripts.Infrastructure.Services.Factories.UI;
using Scripts.Infrastructure.Services.StateMachine;

namespace Scripts.Infrastructure.Services.Window
{
  public interface IWindowService
  {
    void Open(WindowTypeId windowTypeId);
    void TryCloseLastOpened();
    void Initialize(IUIFactory uiFactory, IGameFactory gameFactory, GameStateMachine gameStateMachine);
  }
}