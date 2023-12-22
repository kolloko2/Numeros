using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using Main.Scripts.ChipMovementController;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Sign : AChip
{
    // Start is called before the first frame update


     public int SignNumber=0;
    [SerializeField] private Sprite[] _chipSprite;
    

    void Start()
    {
        gameObject.GetComponent<LayoutElement>().ignoreLayout = false; // КОСТЫЛЬ 
        CheckSign(SignNumber);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ChangeSign(int signNumber)
    {
        SignNumber = signNumber;
        CheckSign(SignNumber);
        
    }

    private void CheckSign(int signNumber)
    {
        switch (signNumber)
        {
            case 0:
                base.CurrentValueString = "+";
                break;
            case 1:
                CurrentValueString = "-";
                break;
            case 2:
                CurrentValueString = "*";
                break;

            case 3:
                CurrentValueString = "/";
                break;
            case 4:
                CurrentValueString = "=";
                break;
      
        }
        gameObject.GetComponent<Image>().sprite = _chipSprite[SignNumber];
    }


    public string GetCurrentSign() => CurrentValueString;




    public void SetCurrentSlot(GameObject Slot) => CurrentSlot = Slot;


    public bool GetIsPlaced() => IsPlaced;
    
    public void SetIsPlaced(bool Placed) => IsPlaced = Placed;

}