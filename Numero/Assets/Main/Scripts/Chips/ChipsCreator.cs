using System;
using System.Collections;
using System.Collections.Generic;
using Infrastructure;

using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ChipsCreator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject[,] _chipsGameObjects;
    [SerializeField] private int _chipsCount = 10;
    [SerializeField] private int _signsCount = 4;
    private int _playerTurn = 0;


    void Start()
    {
        _chipsGameObjects = new GameObject[2, _chipsCount + _signsCount];
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < _chipsCount; j++)
            {
                _chipsGameObjects[i, j] = Instantiate(Resources.Load<GameObject>(Constans.ChipPath), transform);
                _chipsGameObjects[i, j].GetComponent<Number>().ChangeNumber(Random.Range(0, 10));
                _chipsGameObjects[i, j].SetActive(false);
            }

            for (int j = _chipsCount - 1; j < _signsCount + _chipsCount; j++)
            {
                _chipsGameObjects[i, j] = Instantiate(Resources.Load<GameObject>(Constans.SignPath), transform);
                _chipsGameObjects[i, j].GetComponent<Sign>().ChangeSign(Random.Range(0, 4));
                _chipsGameObjects[i, j].SetActive(false);
            }
        }

        PlayerTurn();
    }

    public void RefilChips(int player)
    {
        for (int j = 0; j < _chipsCount; j++)
        {
            if (_chipsGameObjects[player, j] == null)
            {
                _chipsGameObjects[player, j] = Instantiate(Resources.Load<GameObject>(Constans.ChipPath), transform);
                _chipsGameObjects[player, j].GetComponent<Number>().ChangeNumber(Random.Range(0, 10));
                _chipsGameObjects[player, j].SetActive(false);
            }
        }

        for (int j = _chipsCount - 1; j < _signsCount + _chipsCount; j++)
        {
            if (_chipsGameObjects[player, j] == null)
            {
                _chipsGameObjects[player, j] = Instantiate(Resources.Load<GameObject>(Constans.SignPath), transform);
                _chipsGameObjects[player, j].GetComponent<Sign>().ChangeSign(Random.Range(0, 4));
                _chipsGameObjects[player, j].SetActive(false);
            }
        }
    }


    public void PlayerTurn()
    {
        switch (_playerTurn)
        {
            case 0:
                RefilChips(0);
                PlaceChipsForPlayer(0);
                DeleteChipsForPlayer(1);
               
                _playerTurn = 1;
                break;
            case 1:
                RefilChips(1);
                PlaceChipsForPlayer(1);
                DeleteChipsForPlayer(0);
              
                _playerTurn = 0;
                break;
        }
    }

    public void PlaceChipsForPlayer(int player)
    {
        for (int i = 0; i < _chipsCount + _signsCount - 1; i++)
        {
            _chipsGameObjects[player, i].SetActive(true);
           
        }
    }

    public void DeleteChipsForPlayer(int player)
    {
        for (int i = 0; i < _chipsCount + _signsCount - 1; i++)
        {
            _chipsGameObjects[player, i].SetActive(false);
        }
    }





    public int GetCurrentPlayerTurn()
    {
        switch (_playerTurn)
        {
            case 0:
                return 1;
                break;
            case 1:
                return 0;
                break;
        }

        return 0;
    }
}