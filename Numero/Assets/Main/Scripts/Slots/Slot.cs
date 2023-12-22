using System;
using System.Collections;
using System.Collections.Generic;
using Main.Scripts.ChipMovementController;
using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField] private string _currentValue ;
    [SerializeField] private GameObject _chipOnSlot;

    public bool Checked;

    public int CheckTimes = 0;
    public bool Pinned;

    // Start is called before the first frame update

    private void OnEnable() => ChipPicker.ChipMoved += ChipMoved;

    private void OnDisable()
    {
        ChipPicker.ChipMoved -= ChipMoved;
    }

    
    public GameObject GetChipOnSlot() => _chipOnSlot;

    public void SetCurrentValue(string value, GameObject chip)
    {
        if (value.Length == 1)
        {
            _currentValue = value;
            _chipOnSlot = chip;
        }
    }

    public void ChipMoved(GameObject chip)
    {
        if (_chipOnSlot == chip)
        {
            _chipOnSlot = null;
            _currentValue = string.Empty;
        }
        else if (chip.GetComponent<AChip>().CurrentSlot == gameObject)
        {
            _chipOnSlot = chip;
            _currentValue = chip.GetComponent<AChip>().CurrentValueString;
        }
    }


    public string GetCurrentValue() => _currentValue;
    
    
    
}