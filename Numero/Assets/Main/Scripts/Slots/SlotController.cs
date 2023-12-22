using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Main.Scripts.Slots
{
    public class SlotController : MonoBehaviour
    {
        [SerializeField] private GameObject[,] _fieldSlots;
        [SerializeField] private int fieldHight = 8;
        [SerializeField] private int fieldWidth = 8;
        private SlotFieldCreator _slotFieldCreator;
        private ExpressionChecker _expressionChecker;
        [SerializeField] private ChipsCreator _chipsCreator;

        public void Awake()
        {
            _slotFieldCreator = new SlotFieldCreator(fieldHight, fieldWidth, gameObject);
            gameObject.GetComponent<GridLayoutGroup>().constraintCount = fieldHight;
            _fieldSlots = _slotFieldCreator.GetSlotField();
            _expressionChecker = new ExpressionChecker(this, _chipsCreator);
        }

      

        public void ExpressionCheck() => _expressionChecker.CheckField(_fieldSlots);
        public void EndGame() => _expressionChecker.CheckField(_fieldSlots);

        public GameObject[,] GetFieldSlots() => _fieldSlots;
    }
}