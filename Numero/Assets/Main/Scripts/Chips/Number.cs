using System;
using System.Collections;
using System.Collections.Generic;
using Main.Scripts.ChipMovementController;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Number : AChip
{
    // Start is called before the first frame update


     [Range(0, 9)] [SerializeField] public int ChipNumber = 0;
    [SerializeField] private Sprite[] ChipImage;
  
    
    void Start()
    {
        gameObject.GetComponent<Image>().sprite = ChipImage[ChipNumber];
    }

    // Update is called once per frame
    void Update()
    {
    }



    public void ChangeNumber(int number)
    {
        if (number>=0 & number<=9)
        {
            ChipNumber = number;
            CurrentValueString = Convert.ToString(ChipNumber);
            gameObject.GetComponent<Image>().sprite = ChipImage[ChipNumber];
        }
        
        
    }

    public void SetIsPlaced(bool Placed) => IsPlaced = Placed;

 
}
