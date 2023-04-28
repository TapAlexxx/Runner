using System.Linq;
using Scripts.Infrastructure.Services.Window;
using Scripts.StaticData.Player;
using Scripts.StaticData.Window;
using UnityEngine;

namespace Scripts.Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string WindowPath = "StaticData/Window/WindowsStaticData";
        private const string PlayerStaticDataPath = "StaticData/Player/PlayerConfig";

        private WindowStaticData _windowStaticData;
        private PlayerStaticData _playerStaticData;

        public void Load()
        {
            _windowStaticData = Resources
                .Load<WindowStaticData>(WindowPath);

            _playerStaticData = Resources
                .Load<PlayerStaticData>(PlayerStaticDataPath);
        }

        public WindowConfig ForWindow(WindowTypeId windowTypeId) => 
            _windowStaticData.Configs.FirstOrDefault(x => x.WindowTypeId == windowTypeId);

        public PlayerStaticData GetPlayerStaticData() => 
            _playerStaticData;
    }
}