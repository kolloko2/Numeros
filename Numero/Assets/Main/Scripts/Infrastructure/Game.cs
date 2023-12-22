using Infrastructure.Services;
using Infrastructure.States;
using Logic;
using UnityEngine;

namespace Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain, AllServices.Container);
          
        }

    
    }
}