using System.Collections.Generic;
using System.Linq;
using Scripts.Infrastructure.Services.Window;
using Scripts.Logic.Hud.ScrollControls;
using Scripts.Logic.LevelGeneration.Blocks;
using Scripts.StaticData;
using Scripts.StaticData.Player;
using Scripts.StaticData.Window;
using UnityEngine;

namespace Scripts.Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string WindowPath = "StaticData/Window/WindowsStaticData";
        private const string PlayerStaticDataPath = "StaticData/Player/PlayerConfig";
        private const string GameConfigPath = "StaticData/GameConfig";
        private const string PassedBlocksDataPath = "StaticData/PassedBlockStaticData";

        private WindowStaticData _windowStaticData;
        private PlayerStaticData _playerStaticData;
        private GameConfig _gameConfig;
        private List<PassedBlockData> _passedBlockData;

        public void Load()
        {
            _windowStaticData = Resources
                .Load<WindowStaticData>(WindowPath);

            _playerStaticData = Resources
                .Load<PlayerStaticData>(PlayerStaticDataPath);

            _gameConfig = Resources
                .Load<GameConfig>(GameConfigPath);

            _passedBlockData = Resources
                .LoadAll<PassedBlockData>(PassedBlocksDataPath)
                .ToList();
        }

        public WindowConfig ForWindow(WindowTypeId windowTypeId) => 
            _windowStaticData.Configs.FirstOrDefault(x => x.WindowTypeId == windowTypeId);

        public PlayerStaticData GetPlayerStaticData() => 
            _playerStaticData;

        public GameConfig GetGameConfig() => 
            _gameConfig;

        public PassedBlockData GetBlockDataFor(DamageBlockType damageBlockType) => 
            _passedBlockData.FirstOrDefault(x => x.DamageBlockType == damageBlockType);
    }
}