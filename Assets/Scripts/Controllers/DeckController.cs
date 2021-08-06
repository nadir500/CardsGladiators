using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckController : MonoBehaviour
{
    // Start is called before the first frame update
    private DeckDataModel _deckDataModel;
    [SerializeField] private Text _cardsTextContainer;
    [SerializeField] private Text _playerHealth;
    [SerializeField] private Text _enemyHealth;

    void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        _deckDataModel = new DeckDataModel(13, Turn.Player);
        for (int i = 0; i < _deckDataModel.cardsArray.Length; i++)
        {
            Debug.Log("Cards Array " + _deckDataModel.cardsArray[i].cardShapes.ToString() + " " +
                      _deckDataModel.cardsArray[i].cardNumberValue + " " + _deckDataModel.cardsArray[i].cardsColor);
        }

        _cardsTextContainer.text = _deckDataModel.deckTotal.ToString();
        _playerHealth.text = "100";
        _enemyHealth.text = "100";
    }

    public CardDataModel DrawCard()
    {
        int cardIndex = _deckDataModel.deckTotal;

        _deckDataModel.deckTotal--;
        return _deckDataModel.cardsArray[cardIndex];
    }

    public void ExecuteAttack(CardDataModel[] cards, Turn turn)
    {
        int result = 0;
        int currentHealth = 0;
        for (int i = 0; i < cards.Length; i++)
        {
            result += cards[i].cardNumberValue;
        }

        if (turn == Turn.Enemy)
        {
            //player lose health
            currentHealth = Int32.Parse(_playerHealth.text);
            currentHealth -= result;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
            }

            _playerHealth.text = currentHealth.ToString();
        }
        else
        {
            currentHealth = Int32.Parse(_enemyHealth.text);
            currentHealth -= result;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
            }

            _enemyHealth.text = currentHealth.ToString();
        }
    }

    public void EndTurn(ref Turn turn)
    {
        //check game status 
        int playerCurrentHealth = 0;
        int enemyCurrentHealth = 0;
        playerCurrentHealth = Int32.Parse(_playerHealth.text);
        enemyCurrentHealth = Int32.Parse(_enemyHealth.text);
        if (playerCurrentHealth == 0 && enemyCurrentHealth != 0)
        {
            _cardsTextContainer.text = "enemy won!";
        }
        else
        {
            if (playerCurrentHealth != 0 && enemyCurrentHealth == 0)
            {
                _cardsTextContainer.text = "player won!";
            }
        }

        if (turn == Turn.Enemy)
        {
            turn = Turn.Player;
        }

        if (turn == Turn.Player)
        {
            turn = Turn.Enemy;
        }
    }

    public void AttackTurn(List<CardDataModel> cardDataModelArray, Turn turn)
    {
        int attackResult = 0;
        int currentHealth = 0;
        for (int i = 0; i < cardDataModelArray.Count; i++)
        {
            attackResult += cardDataModelArray[i].cardNumberValue;
        }

        if (turn == Turn.Player)
        {
            currentHealth = int.Parse(_enemyHealth.text);
            currentHealth -= attackResult;
            _enemyHealth.text = currentHealth.ToString();
        }
        else
        {
            currentHealth = int.Parse(_playerHealth.text);
            currentHealth -= attackResult;

            _playerHealth.text = currentHealth.ToString();
        }
    }
}