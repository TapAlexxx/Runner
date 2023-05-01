using Scripts.Infrastructure.Services.Window;
using Scripts.Logic.Hud.ScrollControls;
using Scripts.Logic.LevelGeneration.Blocks;
using Scripts.StaticData;
using Scripts.StaticData.Level;
using Scripts.StaticData.Player;
using Scripts.StaticData.Window;

namespace Scripts.Infrastructure.Services.StaticData
{
    public interface IStaticDataService
    {
        void Load();
        WindowConfig ForWindow(WindowTypeId windowTypeId);
        PlayerStaticData GetPlayerStaticData();
        GameConfig GetGameConfig();
        PassedBlockData GetBlockDataFor(DamageBlockType damageBlockType);
        LevelStaticData GetLevelStaticData();
    }
}