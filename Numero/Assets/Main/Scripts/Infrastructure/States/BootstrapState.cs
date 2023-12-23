
using Infrastructure.Services;

using Application = UnityEngine.Device.Application;

namespace Infrastructure.States
{
    public class BootstrapState : IPayloadedState<string>
    {
        private const string Initial = "Initial";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            
            RegisterServices();
        }

        public void Enter(string sceneName)
        {
            _sceneLoader.Load(Initial, () => EnterLoadScene(sceneName));
        }

        public void Exit()
        {

        }

        private void EnterLoadScene(string sceneName)
        {
            _stateMachine.Enter<LoadSceneState, string>(sceneName);
        }

        private void RegisterServices()
        {
           

        }

       
    }
}
