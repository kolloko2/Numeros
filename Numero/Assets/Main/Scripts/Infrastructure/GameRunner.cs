using System;
using Main.Scripts.Infrastructure;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private GameBootstrapper _bootstrapperPrefab;
        [SerializeField] private SceneController _sceneControllerPrefab;
     
        // [SerializeField] private string _sceneName;
        private void Awake()
        {
            var bootstrapper = FindObjectOfType<GameBootstrapper>();

            if (bootstrapper == null)
            {
                var sceneName = Constans.MainMenuScene;
                var instantiatedBootstrapper = Instantiate(_bootstrapperPrefab);
            
                instantiatedBootstrapper.Init(sceneName);
     
            }
        }
    }
}