using System;
using Main.Scripts.Checker;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.ChipMovementController
{
    public class ChipPicker
    {
        private GameObject _chipInHands;
        private TextMeshProUGUI  _firstplayerscore;
        private TextMeshProUGUI _secondplayerscore;
        private int _intfirstplayerscore;
        private int _intsecondplayerscore;
        public static Action<GameObject> ChipMoved;

        public ChipPicker(TextMeshProUGUI  tmp1, TextMeshProUGUI  tmp2)
        {
            ChipClicker.RayCastHitted += PickChip;
            _firstplayerscore = tmp1;
            _secondplayerscore = tmp2;
            ExpressionScore.ScoreChanged += ScoreChanged;
        }

       
        private void PickChip(GameObject chip)
        {
            if (chip.GetComponent<AChip>() != null)
            {
                if (_chipInHands == null)
                {
                    _chipInHands = chip;
                    ChipMoved?.Invoke(chip);
                }
                else
                {
                    if (chip.GetComponent<Number>() != null) // Это номер
                    {
                        if (_chipInHands.GetComponent<Number>() != null) // номер на номер
                        {
                            (chip.GetComponent<Number>().IsPlaced, _chipInHands.GetComponent<Number>().IsPlaced) = (
                                _chipInHands.GetComponent<Number>().IsPlaced, chip.GetComponent<Number>().IsPlaced);
                            (chip.GetComponent<Number>().CurrentSlot, _chipInHands.GetComponent<Number>().CurrentSlot) =
                                (_chipInHands.GetComponent<Number>().CurrentSlot,
                                    chip.GetComponent<Number>().CurrentSlot);
                        }
                        else // номер на знак
                        {
                            (chip.GetComponent<Number>().IsPlaced, _chipInHands.GetComponent<Sign>().IsPlaced) = (
                                _chipInHands.GetComponent<Sign>().IsPlaced, chip.GetComponent<Number>().IsPlaced);
                            (chip.GetComponent<Number>().CurrentSlot, _chipInHands.GetComponent<Sign>().CurrentSlot) = (
                                _chipInHands.GetComponent<Sign>().CurrentSlot, chip.GetComponent<Number>().CurrentSlot);
                        }
                    }
                    else // Это не номер
                    {
                        if (_chipInHands.GetComponent<Number>() != null) // знак на номер
                        {
                            (chip.GetComponent<Sign>().IsPlaced, _chipInHands.GetComponent<Number>().IsPlaced) = (
                                _chipInHands.GetComponent<Number>().IsPlaced, chip.GetComponent<Sign>().IsPlaced);

                            (chip.GetComponent<Sign>().CurrentSlot, _chipInHands.GetComponent<Number>().CurrentSlot) = (
                                _chipInHands.GetComponent<Number>().CurrentSlot, chip.GetComponent<Sign>().CurrentSlot);
                        }
                        else // знак на знак
                        {
                            (chip.GetComponent<Sign>().IsPlaced, _chipInHands.GetComponent<Sign>().IsPlaced) = (
                                _chipInHands.GetComponent<Sign>().IsPlaced, chip.GetComponent<Sign>().IsPlaced);
                            (chip.GetComponent<Sign>().CurrentSlot, _chipInHands.GetComponent<Sign>().CurrentSlot) = (
                                _chipInHands.GetComponent<Sign>().CurrentSlot, chip.GetComponent<Sign>().CurrentSlot);
                        }
                    }

                    ChipMoved?.Invoke(chip);
                    ChipMoved?.Invoke(_chipInHands);
                    (chip.transform.position, _chipInHands.transform.position) =
                        (_chipInHands.transform.position, chip.transform.position);
                    _chipInHands = null;
                
                }
            }
        }

        public void ScoreChanged(int playerOneScore, int playerTwoScore)
        {
            
            
            _firstplayerscore.text = ($"{playerOneScore}");
            _secondplayerscore.text = ($"{playerTwoScore}");


            _intfirstplayerscore = playerOneScore;
            _intsecondplayerscore = playerTwoScore;
        }

        public void EndGame(TextMeshProUGUI finishTMP, GameObject panel)
        {
            panel.SetActive(true);
            if (_intfirstplayerscore > _intsecondplayerscore)
            {
                finishTMP.text = ("Выиграл игрок 1" + "УРА УРА УРА");
                

            }
            else if (_intfirstplayerscore < _intsecondplayerscore)
            {
                finishTMP.text = ("Выиграл игрок 1" + "УРА УРА УРА");
                

            }
            else if (_intfirstplayerscore == _intsecondplayerscore)
            {
                finishTMP.text = ("Выиграла дружба");
                

            }
        }
        
        
        
        
        
        public void ChipPlaced()
        {
            _chipInHands.GetComponent<LayoutElement>().ignoreLayout = true;
            _chipInHands.GetComponent<Number>()?.SetIsPlaced(true);
            _chipInHands.GetComponent<Sign>()?.SetIsPlaced(true);
            _chipInHands = null;
        }

        public GameObject GetChipInHands() => _chipInHands;

        public void ChipBack()
        {      
            _chipInHands.GetComponent<AChip>().CurrentSlot = null;
            ChipMoved?.Invoke(_chipInHands);
            _chipInHands.GetComponent<LayoutElement>().ignoreLayout = false;
            

            if (_chipInHands.GetComponent<AChip>().CurrentValueString == "=")
            {
                GameObject.Destroy(_chipInHands);
            }
            ClearChipInHands();
            
        }

        public void ClearChipInHands() => _chipInHands = null;
    }
}