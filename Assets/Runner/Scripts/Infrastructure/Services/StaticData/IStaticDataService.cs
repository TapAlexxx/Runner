using Scripts.Infrastructure.Services.Window;
using Scripts.StaticData;
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
    }
}