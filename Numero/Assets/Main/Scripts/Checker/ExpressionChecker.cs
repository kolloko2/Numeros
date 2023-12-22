using System;
using System.Collections;
using System.Collections.Generic;
using Main.Scripts.Checker;
using Main.Scripts.Slots;
using UnityEngine;
using org.mariuszgromada.math.mxparser;
using Unity.Mathematics;
using Unity.VisualScripting;

public class ExpressionChecker
{
    private SlotController _slotController;


    private string _mathstring;
    private GameObject[,] _fieldSlots;
    private List<GameObject> _currentExpression = new List<GameObject>();
    public ExpressionPinner expressionPinner;
    public ExpressionPinner expressionScore;
    private ChipsCreator _chipsCreator;


    public ExpressionChecker(SlotController slotController, ChipsCreator chipsCreator)
    {
        _slotController = slotController;
        SlotFieldCreator.FieldCreated += FieldSlotRefil;
        _chipsCreator = chipsCreator;
        expressionPinner = new ExpressionPinner(this, _slotController, chipsCreator);
       
    }

    void FieldSlotRefil(GameObject[,] fieldSlots)
    {
        _fieldSlots = fieldSlots;
    }


    public void CheckField(GameObject[,] FieldSlots)
    {
        _fieldSlots = FieldSlots;


        bool alltrue = true;
        for (int i = 0; i < Math.Sqrt(_fieldSlots.Length); i++)
        {
            for (int y = 0; y < Math.Sqrt(_fieldSlots.Length); y++)
            {
                _mathstring += _fieldSlots[i, y].GetComponent<Slot>().GetCurrentValue();
                if (_fieldSlots[i, y].GetComponent<Slot>().GetCurrentValue() == string.Empty)
                {
                    alltrue = alltrue & Validating();
                }
                else if (y == Math.Sqrt(_fieldSlots.Length))
                {
                    _currentExpression.Add(_fieldSlots[i, y]);
                    alltrue = alltrue & Validating();
                }
                else
                {
                    _currentExpression.Add(_fieldSlots[i, y]);
                }
            }
        }


        for (int i = 0; i < Math.Sqrt(_fieldSlots.Length); i++)
        {
            for (int y = 0; y < Math.Sqrt(_fieldSlots.Length); y++)
            {
                _mathstring += _fieldSlots[y, i].GetComponent<Slot>().GetCurrentValue();

                if (_fieldSlots[y, i].GetComponent<Slot>().GetCurrentValue() == string.Empty)
                {
                    alltrue = alltrue & Validating();
                }
                else if (y == Math.Sqrt(_fieldSlots.Length))
                {
                    _currentExpression.Add(_fieldSlots[y, i]);
                    alltrue = alltrue & Validating();
                }
                else
                {
                    _currentExpression.Add(_fieldSlots[y, i]);
                }
            }
        }

    Debug.Log(alltrue);

        expressionPinner.PinSlots(alltrue);
    }


    bool Validating()
    {
        bool x = false;
        int checkTrueTime = 0;

        if (_mathstring.Contains("=") & _mathstring.Length != 1)
        {
            if (_mathstring.Split('=')[1] != string.Empty)
            {
                Expression leftPart = new Expression(_mathstring.Split('=')[0]);
                Expression rightPart = new Expression(_mathstring.Split('=')[1]);
                double v = leftPart.calculate();
                double c = rightPart.calculate();
                Debug.Log(_mathstring);
                Debug.Log($"left {v} right {c}");
                if (v == c)
                {
                    x = true;
                }
               
                else
                {
                    x = false;
                }
              
            }
            else
            {
                x = false;
            }
     
        }
        else if (_mathstring == string.Empty)
        {
            x = true;
        }
    
   
        else if (_currentExpression.Count == 1 & _currentExpression[0].GetComponent<Slot>().Checked)
        {
            x = true;
        }
        else if (_currentExpression.Count == 1 & _currentExpression[0].GetComponent<Slot>().CheckTimes == 0)
        {
            checkTrueTime = 1;
            x = true;
        }
        else
        {
            x = false;
        }
       

        foreach (var gameObject in _currentExpression)
        {
            if (x & checkTrueTime != 1)
            {Debug.Log(gameObject.GetComponent<Slot>().GetCurrentValue());
                Debug.Log(gameObject);
                gameObject.GetComponent<Slot>().Checked = true;
            }

            gameObject.GetComponent<Slot>().CheckTimes += 1;
        }

        _mathstring = string.Empty;
        _currentExpression.Clear();
        return x;
    }
}