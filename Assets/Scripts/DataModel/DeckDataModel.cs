using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Turn
{
    Player = 0,
    Enemy = 1
}

public class DeckDataModel
{
    public int deckTotal { get; set; }
    public Turn playerTurn { get; set; }
    public CardDataModel[] cardsArray;

    public DeckDataModel(int _deckTotal, Turn _playerTurn)
    {
        deckTotal = _deckTotal ;
        playerTurn = _playerTurn;
        cardsArray = new CardDataModel[deckTotal ];
        for (int i = 0; i < deckTotal ; i++)
        {
            //generate cards New cards(); 
            cardsArray[i] = new CardDataModel((CardsShapes) Random.Range(0, 3), Random.Range(1, 13),
                (CardsColor) Mathf.RoundToInt(Random.value));
        }
    }
}