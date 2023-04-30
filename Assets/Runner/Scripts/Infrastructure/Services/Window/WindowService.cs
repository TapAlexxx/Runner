using Scripts.Infrastructure.Services.Factories.UI;
using UnityEngine;

namespace Scripts.Infrastructure.Services.Window
{
    public class WindowService : IWindowService
    {
        private readonly IUIFactory _uiFactory;
        private RectTransform _lastOpened;

        public WindowService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public void Open(WindowTypeId windowTypeId)
        {
            _lastOpened = _uiFactory.CrateWindow(windowTypeId);
        }

        public void TryCloseLastOpened()
        {
            if(_lastOpened == null)
                return;
            Object.Destroy(_lastOpened);
        }
    }
}