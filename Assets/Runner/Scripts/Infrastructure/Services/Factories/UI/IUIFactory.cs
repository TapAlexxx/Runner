using Scripts.Infrastructure.Services.Window;
using UnityEngine;

namespace Scripts.Infrastructure.Services.Factories.UI
{
  public interface IUIFactory
  {
    void CreateUiRoot();
    RectTransform CrateWindow(WindowTypeId windowTypeId);
  }
}