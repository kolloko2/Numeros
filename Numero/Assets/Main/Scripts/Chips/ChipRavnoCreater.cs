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

        CreateRavno();
    }

    private void CheckIfNeedToCreateAnotherRavno(GameObject movedChip)
    {
        if (movedChip.GetComponent<AChip>().CurrentValueString == "=")
        {
            
            CreateRavno();
        }
      
  
    }

    private void CreateRavno()
    {      
      
        GameObject.Instantiate(_ravnoChip, GameObject.FindWithTag(Constans.ChipsTag).transform);
        
    }

 
    
}