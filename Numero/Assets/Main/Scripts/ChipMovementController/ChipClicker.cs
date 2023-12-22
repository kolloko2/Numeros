using System;
using System.Reflection;
using Infrastructure;
using Infrastructure.States;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Main.Scripts.ChipMovementController
{
    public class ChipClicker : MonoBehaviour
    {
        public GameObject ClickedGameObject { get; private set; }
        public ChipPicker chipPicker;
        public ChipMover chipMover;
        public static Action<GameObject> RayCastHitted;
  
        [SerializeField]  private TextMeshProUGUI  _firstplayerscore;
        [SerializeField]  private TextMeshProUGUI  _secondplayerscore;
        [SerializeField]  private GameObject  _finishpanel;
        [SerializeField] private TextMeshProUGUI _finishTMP;
        private void Start()
        {
            chipPicker = new ChipPicker(_firstplayerscore,_secondplayerscore);
            chipMover = new ChipMover(chipPicker);
        }


        private void Update()

        {
            if (Input.GetMouseButtonUp(0))
            {
                Vector2 CurMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                RaycastHit2D hit = Physics2D.Raycast(CurMousePos, Vector2.zero);


                if (hit.collider != null)
                {
                    ClickedGameObject = hit.collider.gameObject;
                    RayCastHitted?.Invoke(ClickedGameObject);
                }
            }
        }
        
        public void EndGame()
        {
            chipPicker.EndGame(_finishTMP,_finishpanel);
            
        }

        public void ToMainScreen()
        {
           GameBootstrapper gameBootstrapper = DIContainer.Resolve<GameBootstrapper>();
           gameBootstrapper.Game.StateMachine.Enter<LoadSceneState, string>(Constans.MainMenuScene);
           chipPicker.ClearChipInHands();
           
        }

        
        
    }
    
    

    
}