using System;
using System.Collections;
using System.Collections.Generic;
using Infrastructure;
using Main.Scripts.Slots;

using UnityEngine;
using UnityEngine.UI;

public class SlotFieldCreator
{
    // Start is called before the first frame update

    public static Action<GameObject[,]> FieldCreated;
    private GameObject[,] _slots;
    private int _fieldHight = 8;
    private int _fieldWidth = 8;
    private GameObject _slotControllerGameObject;


    public SlotFieldCreator(int fieldHight, int fieldWidth, GameObject slotControllerGameObject)
    {
        _fieldHight = fieldHight;
        _fieldWidth = fieldWidth;

        _slotControllerGameObject = slotControllerGameObject;
        _slots = new GameObject[_fieldHight, _fieldWidth];
        CreateField();

    }

    public void CreateField()
    {

        for (int i = 0; i < _fieldHight; i++)
        {
            for (int y = 0; y < _fieldWidth; y++)
            {
                _slots[i, y] = GameObject.Instantiate(Resources.Load<GameObject>(Constans.SlotPath),
                    _slotControllerGameObject.transform);
            }
        }

     
        FieldCreated?.Invoke(_slots);
    }

    public GameObject[,] GetSlotField()
    {
        return _slots;
    }
}