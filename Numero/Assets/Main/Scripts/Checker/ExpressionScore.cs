using System;
using Main.Scripts.Slots;
using Unity.VisualScripting;
using UnityEngine;

namespace Main.Scripts.Checker
{
    public class ExpressionScore
    {
        private readonly SlotController _slotController;
        private readonly ChipsCreator _chipsCreator;


        private readonly ExpressionChecker _expressionChecker;
        private int _playerOneScore = 0;
        private int _playerTwoScore = 0;
        public static Action<int,int> ScoreChanged;

        public ExpressionScore(ExpressionChecker expressionChecker, SlotController slotController,
            ChipsCreator chipsCreator)
        {
            _expressionChecker = expressionChecker;
            _slotController = slotController;
            _chipsCreator = chipsCreator;
        }

        public void AddScore(int playerTurn)
        {
            int x = 0;

            foreach (GameObject slot in _slotController.GetFieldSlots())
            {
                if (slot.GetComponent<Slot>().Pinned == false)

                {
                    switch (slot.GetComponent<Slot>().GetCurrentValue())
                    {
                        case "+":
                            x += 1;
                            break;

                        case "-":
                            x += 1;

                            break;
                        case "*":
                            x += 2;
                            break;

                        case "/":
                            x += 3;
                            break;
                        case "=":
                            break;

                        default:



                            if (slot.GetComponent<Slot>().GetCurrentValue() != string.Empty)
                            {
                                Debug.Log(slot.GetComponent<Slot>().GetCurrentValue());
                                x += Convert.ToInt32(slot.GetComponent<Slot>().GetCurrentValue());
                            }

                           
                          
                            
                            break;
                    }
                    }
                }
            


            switch (playerTurn)
            {
                case 0:
                    _playerOneScore += x;
                    break;
                case 1:
                    _playerTwoScore += x;
                    break;
            }


            
            ScoreChanged(_playerOneScore, _playerTwoScore);
        }
    }
}