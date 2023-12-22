 using Infrastructure;
 using Infrastructure.Services;
 using Infrastructure.States;
 using UnityEngine;

 namespace Main.Scripts.Infrastructure
 {
   public class SceneController : MonoBehaviour
     {
        [SerializeField] private GameBootstrapper _gameBootstrapper;
        
  

        public void LoadScene(string sceneName)
        {
            _gameBootstrapper = DIContainer.Resolve<GameBootstrapper>();
      
            _gameBootstrapper.Game.StateMachine.Enter<LoadSceneState, string>(sceneName);
        }

       

     }
 }

