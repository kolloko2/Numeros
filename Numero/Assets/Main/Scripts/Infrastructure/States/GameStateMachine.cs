using System;
using System.Collections.Generic;
using Infrastructure.Factory;
using Infrastructure.Services;
using Logic;

namespace Infrastructure.States
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain curtain, AllServices services)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                { typeof(BootstrapState), new BootstrapState(this, sceneLoader, services) },
                { typeof(LoadSceneState), new LoadSceneState(this, sceneLoader, curtain, services.Single<IGameFactory>()) },
                { typeof(GameLoopState), new GameLoopState(this) },
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            ChangeState<TState>().Enter();
        }

        

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            ChangeState<TState>().Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            TState state = GetState<TState>();
            _activeState = state;
            return state;
        }

        public TState GetState<TState>() where TState : class, IExitableState
        {
            _states.TryGetValue(typeof(TState), out IExitableState state);
            return (TState)state;
        }
    }
}
