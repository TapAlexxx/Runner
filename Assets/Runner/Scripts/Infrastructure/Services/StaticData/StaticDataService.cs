using System.Linq;
using Scripts.Infrastructure.Services.Window;
using Scripts.StaticData.Window;
using UnityEngine;

namespace Scripts.Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private WindowStaticData _windowStaticData;
        private const string WindowPath = "StaticData/Window/WindowsStaticData";

        public void Load()
        {
            _windowStaticData = Resources.Load<WindowStaticData>(WindowPath);
        }

        public WindowConfig ForWindow(WindowTypeId windowTypeId) => 
            _windowStaticData.Configs.FirstOrDefault(x => x.WindowTypeId == windowTypeId);
    }
}