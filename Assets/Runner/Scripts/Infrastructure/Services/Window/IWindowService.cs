using Scripts.Infrastructure.Services.Factories.Game;
using Scripts.Infrastructure.Services.Factories.UI;
using Scripts.Infrastructure.Services.StateMachine;
using Scripts.Infrastructure.Services.StaticData;

namespace Scripts.Infrastructure.Services.Window
{
  public interface IWindowService
  {
    void Initialize(IUIFactory uiFactory, IGameFactory gameFactory, GameStateMachine gameStateMachine, IStaticDataService staticDataService);
    void Open(WindowTypeId windowTypeId);
    void TryCloseLastOpened();
  }
}