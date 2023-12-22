using System;
using Infrastructure;
using Unity.VisualScripting;
using UnityEngine;

namespace Main.Scripts.ChipMovementController
{
    public class ChipMover
    {
        private ChipPicker _chipPicker;
        public static Action<GameObject> ChipMoved;
        public static Action ChipRavnoDeleted;

        public ChipMover(ChipPicker chipPicker)
        {
            ChipClicker.RayCastHitted += MoveChip;
            _chipPicker = chipPicker;
        }


        private void MoveChip(GameObject slot)
        {
            if (slot.GetComponent<Slot>() != null)
            {
                if (_chipPicker.GetChipInHands() != null & !slot.GetComponent<Slot>().Pinned)
                {
                    _chipPicker.GetChipInHands().transform.position = slot.transform.position;
                    _chipPicker.GetChipInHands().GetComponent<AChip>().CurrentSlot = slot;

                    slot.GetComponent<Slot>()
                        .SetCurrentValue(_chipPicker.GetChipInHands().GetComponent<AChip>().CurrentValueString,
                            _chipPicker.GetChipInHands());
                    ChipMoved?.Invoke(_chipPicker.GetChipInHands());

                    _chipPicker.ChipPlaced();

                }
            }
            else if (slot.CompareTag(Constans.BackgroundTag) & _chipPicker.GetChipInHands() != null)
            {
                if (_chipPicker.GetChipInHands().GetComponent<AChip>().CurrentValueString == "=")
                {
                    ChipRavnoDeleted?.Invoke();
                }
          
                _chipPicker.ChipBack();
            
            }
        }
    }
}