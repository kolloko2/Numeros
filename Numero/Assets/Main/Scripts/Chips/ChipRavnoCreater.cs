using System;
using System.Collections;
using System.Collections.Generic;
using Infrastructure;
using Main.Scripts.ChipMovementController;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ChipRavnoCreater : MonoBehaviour
{
    // Start is called before the first frame update]
    [SerializeField] private GameObject _ravnoChip;


    private void Start()
    {
        _ravnoChip = new GameObject();
        _ravnoChip = Resources.Load<GameObject>(Constans.SignPath);
        _ravnoChip.GetComponent<Sign>().SignNumber = 4;
        ChipMover.ChipMoved += CheckIfNeedToCreateAnotherRavno;
        ChipMover.ChipRavnoDeleted += CreateRavnoAfterDelete;
        CreateRavno();
    }

    private void CheckIfNeedToCreateAnotherRavno(GameObject movedChip)
    {
        bool tmpBool = true; 
        
        foreach ( AChip chip in GameObject.FindWithTag(Constans.ChipsTag).GetComponentsInChildren<AChip>())
        {
            if (chip.CurrentValueString == "=" & chip.GameObject().activeSelf & !chip.IsPlaced)
            {
                tmpBool &= false;
            }
        }
        
        
        if (tmpBool)
        {
            CreateRavno();
        }
    }

    private void CreateRavno()
    {      
      
        GameObject.Instantiate(_ravnoChip, GameObject.FindWithTag(Constans.ChipsTag).transform);
        
    }

    private void CreateRavnoAfterDelete()
    {
        bool tmpBool=true;
        foreach ( AChip chip in GameObject.FindWithTag(Constans.ChipsTag).GetComponentsInChildren<AChip>())
        {
            if (chip.CurrentValueString == "=" & chip.GameObject().activeSelf & !chip.IsPlaced)
            {
                tmpBool = false;
            }
        }

        if (tmpBool)
        {
            CreateRavno();
        }
    }
    
    
}