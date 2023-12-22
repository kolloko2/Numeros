using Infrastructure.AssetManagement;
using Unity.Properties;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;

        public GameFactory(IAssets assets)
        {
            _assets = assets;
        }

        public void Instantiat(string path)
        {
            _assets.Instantiate("path");
        }
        
    
        
    }
}