using Infrastructure;
using Main.Scripts.ChipMovementController;
using Main.Scripts.Slots;
using UnityEngine;

namespace Main.Scripts.Checker
{
    public class ExpressionPinner
    {
        private ExpressionChecker _expressionChecker;
        private readonly SlotController _slotController;
        private GameObject[,] _fieldSlots;
        ChipsCreator _chipsCreator;
        private ExpressionScore _expressionScore;

        public ExpressionPinner(ExpressionChecker expressionChecker, SlotController slotController,
            ChipsCreator chipsCreator)
        {
            _expressionChecker = expressionChecker;
            _slotController = slotController;
            _chipsCreator = chipsCreator;
            _expressionScore = new ExpressionScore(expressionChecker, slotController, chipsCreator);
        }

        public void PinSlots(bool Ok)
        {
            if (Ok)
            {
                PinSlotsTrue();
            }
            else
            {
                PinSlotsFalse();
            }
        }

        private void PinSlotsFalse()
        {
            _fieldSlots = _slotController.GetFieldSlots();
            foreach (var slot in _fieldSlots)
            {
                slot.GetComponent<Slot>().CheckTimes = 0;
                slot.GetComponent<Slot>().Checked = false;
            }

            Debug.Log("А вот кривенько");
        }

        private void PinSlotsTrue()
        {
            _fieldSlots = _slotController.GetFieldSlots();
            Debug.Log("Все выражения верны");

            _expressionScore.AddScore(_chipsCreator.GetCurrentPlayerTurn());


            foreach (var slot in _fieldSlots)
            {
                if (slot.GetComponent<Slot>().Checked & slot.GetComponent<Slot>().Pinned == false)
                {
                    GameObject tempSlot =
                        GameObject.Instantiate(slot.GetComponent<Slot>().GetChipOnSlot(), slot.transform);
                    tempSlot.transform.position = slot.transform.position;
                    tempSlot.GetComponent<Collider2D>().enabled = false;
                    GameObject.Destroy(slot.GetComponent<Slot>().GetChipOnSlot());
                    slot.GetComponent<Slot>().Pinned = true;
                    slot.GetComponent<Slot>().CheckTimes = 0;
                    slot.GetComponent<Slot>().Checked = false;
                }
            }

            _chipsCreator.PlayerTurn();
        }
    }
}