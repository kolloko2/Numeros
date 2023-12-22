using UnityEngine;

namespace Infrastructure.AssetManagement
{
    public class AssetsProvider : IAssets
    {
        public GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }
    }
}
