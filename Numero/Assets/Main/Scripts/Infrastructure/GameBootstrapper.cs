using Infrastructure.States;
using Logic;
using Main.Scripts.ChipMovementController;
using UnityEngine;
using UnityEngine.Serialization;

namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        public LoadingCurtain CurtainPrefab;

        public Game Game;
        public ChipClicker ChipClicker;

        public void Init(string sceneName)
        {
            Game = new Game(this, Instantiate(CurtainPrefab));
            Game.StateMachine.Enter<BootstrapState, string>(sceneName);
            DIContainer.Bind(this);
            DontDestroyOnLoad(this);
        }
  
    }
}