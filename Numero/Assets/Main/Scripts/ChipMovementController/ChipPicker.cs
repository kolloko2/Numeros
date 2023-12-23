using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Infrastructure;
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
        private TextMeshProUGUI _firstplayerscore;
        private TextMeshProUGUI _secondplayerscore;
        private int _intfirstplayerscore;
        private int _intsecondplayerscore;
        private GameBootstrapper _gameBootstrapper;
        public static Action<GameObject> ChipMoved;

        public ChipPicker(TextMeshProUGUI tmp1, TextMeshProUGUI tmp2)
        {
            _gameBootstrapper = DIContainer.Resolve<GameBootstrapper>();
            ChipClicker.RayCastHitted += PickChip;
            _firstplayerscore = tmp1;
            _secondplayerscore = tmp2;
            ExpressionScore.ScoreChanged += ScoreChanged;
            Debug.Log(123);
        }


        private void PickChip(GameObject chip)
        {
            if (chip.GetComponent<AChip>() != null)
            {
                if (_chipInHands == null)
                {
                    _chipInHands = chip;
                    _chipInHands.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
                    ChipMoved?.Invoke(chip);
                }
                else
                {
                    if (_chipInHands.GetComponent<AChip>().CurrentValueString == "=" | chip.GetComponent<AChip>().CurrentValueString=="=") 
                    {
                    }
                    
                    else
                    {
                        _chipInHands.transform.localScale = new Vector3(1f, 1f, 1f);

                        if (chip.GetComponent<Number>() != null) // Это номер
                        {
                            if (_chipInHands.GetComponent<Number>() != null) // номер на номер
                            {
                                (chip.GetComponent<Number>().IsPlaced, _chipInHands.GetComponent<Number>().IsPlaced) = (
                                    _chipInHands.GetComponent<Number>().IsPlaced, chip.GetComponent<Number>().IsPlaced);
                                (chip.GetComponent<Number>().CurrentSlot,
                                        _chipInHands.GetComponent<Number>().CurrentSlot) =
                                    (_chipInHands.GetComponent<Number>().CurrentSlot,
                                        chip.GetComponent<Number>().CurrentSlot);
                            }
                            else // номер на знак
                            {
                                (chip.GetComponent<Number>().IsPlaced, _chipInHands.GetComponent<Sign>().IsPlaced) = (
                                    _chipInHands.GetComponent<Sign>().IsPlaced, chip.GetComponent<Number>().IsPlaced);
                                (chip.GetComponent<Number>().CurrentSlot,
                                    _chipInHands.GetComponent<Sign>().CurrentSlot) = (
                                    _chipInHands.GetComponent<Sign>().CurrentSlot,
                                    chip.GetComponent<Number>().CurrentSlot);
                            }
                        }
                        else // Это не номер
                        {
                            if (_chipInHands.GetComponent<Number>() != null) // знак на номер
                            {
                                (chip.GetComponent<Sign>().IsPlaced, _chipInHands.GetComponent<Number>().IsPlaced) = (
                                    _chipInHands.GetComponent<Number>().IsPlaced, chip.GetComponent<Sign>().IsPlaced);

                                (chip.GetComponent<Sign>().CurrentSlot,
                                    _chipInHands.GetComponent<Number>().CurrentSlot) = (
                                    _chipInHands.GetComponent<Number>().CurrentSlot,
                                    chip.GetComponent<Sign>().CurrentSlot);
                            }
                            else // знак на знак
                            {
                                (chip.GetComponent<Sign>().IsPlaced, _chipInHands.GetComponent<Sign>().IsPlaced) = (
                                    _chipInHands.GetComponent<Sign>().IsPlaced, chip.GetComponent<Sign>().IsPlaced);
                                (chip.GetComponent<Sign>().CurrentSlot, _chipInHands.GetComponent<Sign>().CurrentSlot) =
                                (
                                    _chipInHands.GetComponent<Sign>().CurrentSlot,
                                    chip.GetComponent<Sign>().CurrentSlot);
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
                finishTMP.text = ("Выиграл игрок 1");
            }
            else if (_intfirstplayerscore < _intsecondplayerscore)
            {
                finishTMP.text = ("Выиграл игрок 2");
            }
            else if (_intfirstplayerscore == _intsecondplayerscore)
            {
                finishTMP.text = ("Победила дружба");
            }
        }


        public void ChipPlaced()
        {
            _chipInHands.GetComponent<LayoutElement>().ignoreLayout = true;
            _chipInHands.GetComponent<Number>()?.SetIsPlaced(true);
            _chipInHands.GetComponent<Sign>()?.SetIsPlaced(true);
            _chipInHands.transform.localScale = new Vector3(1f, 1f, 1f);

            _chipInHands = null;
        }

        public GameObject GetChipInHands() => _chipInHands;

        public void ChipBack()
        {
            _gameBootstrapper.StartCoroutine(ChipBackCoroutine());
        }

        IEnumerator ChipBackCoroutine()
        {
            _chipInHands.GetComponent<Image>().DOFade(0, 0.5f);
           
            if (_chipInHands.GetComponent<AChip>().CurrentValueString == "=" &
                _chipInHands.GetComponent<AChip>().IsPlaced == true)
            {
                GameObject.Destroy(_chipInHands);
                yield break;
            }

            yield return new WaitForSeconds(0.5f);
            _chipInHands.GetComponent<AChip>().CurrentSlot.GetComponent<Slot>().Busy = false;
            _chipInHands.GetComponent<AChip>().CurrentSlot = null;
            ChipMoved?.Invoke(_chipInHands);
            _chipInHands.GetComponent<LayoutElement>().ignoreLayout = false;


            _chipInHands.GetComponent<Image>().DOFade(255, 0.5f);

            _chipInHands.transform.localScale = new Vector3(1f, 1f, 1f);

            ClearChipInHands();
        }

        public void ClearChipInHands() => _chipInHands = null;
    }
}