using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CardsShapes
{
    Diamonds =0 , 
    Clubs =1 ,
    Hearts =2 , 
    Spades =3 
}

public enum CardsColor
{
    Red=0,
    Black=1
}
public class CardDataModel  
{
    public CardsShapes cardShapes { get; set;  }
    public int cardNumberValue { get; set; }
    public CardsColor cardsColor { get; set; }

    public CardDataModel(CardsShapes _cardShapes, int _cardNumberValue, CardsColor _cardsColor)
    {
        cardShapes = _cardShapes;
        cardNumberValue = _cardNumberValue;
        cardsColor = _cardsColor;
    }
}
