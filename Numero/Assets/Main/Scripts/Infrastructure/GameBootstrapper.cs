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
        private AudioSource _audioSource;

        public void Init(string sceneName)
        {
            Game = new Game(this, Instantiate(CurtainPrefab));
            Game.StateMachine.Enter<BootstrapState, string>(sceneName);
            DIContainer.Bind(this);
            _audioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(this);
        }

        public void ChangeVolume(float volume)
        {
            _audioSource.volume = volume;
        }
  
    }
}