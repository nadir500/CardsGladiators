 using System.Collections;
using System.Collections.Generic;
 using UnityEngine;
 using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private DeckController _deckController;

    [Header("Buttons Events")] [SerializeField]
    private Button[] _playerInteractButtons;

    private Turn _turn;
    private PlayerDataModel _playerCards;
    private PlayerDataModel _enemyCards;
    private bool _isInit = true;

    [Header("Card Prefab UI")] [SerializeField]
    private GameObject _cardPrefab;

    [SerializeField] private GameObject _playerCardsPanel;
    [SerializeField] private GameObject _enemyCardsPanel;

    private void Start()
    {
        _playerCards = new PlayerDataModel();
        _enemyCards = new PlayerDataModel();
        _playerCards.heldCards = new List<CardDataModel>();
        _enemyCards.heldCards = new List<CardDataModel>();
        for (int i = 0; i < _playerInteractButtons.Length; i++)
        {
            _playerInteractButtons[i].interactable = false;
        }
        _playerInteractButtons[0].onClick.AddListener(EndTurnButton);
        _playerInteractButtons[1].onClick.AddListener(AttackButton);
        _playerInteractButtons[2].onClick.AddListener(DrawCardButton);
        
        Initialize();
    }

    private void Initialize()
    {
        Debug.Log("Init cards draw request");
        
        _playerCards.heldCards.Add(_deckController.DrawCard());
        InstanceCard(_playerCards.heldCards[0], Turn.Player);
        
        Debug.Log("card player drawn ");
        _enemyCards.heldCards.Add(_deckController.DrawCard());
        InstanceCard(_enemyCards.heldCards[0], Turn.Enemy);

        if (_playerCards.heldCards[0].cardNumberValue > _enemyCards.heldCards[0].cardNumberValue)
        {
            _turn = Turn.Player;
        }
        else
        {
            _turn = Turn.Enemy;
        }
        //add button listeners 

        //TODO: solve equality bug
        //start playing 
        PlayTurn(_turn);
    }

    private void PlayTurn(Turn turn)
    {
        CardDataModel drawnCard = _deckController.DrawCard();
        if (turn == Turn.Player)
        {
            //player logic 
            _playerInteractButtons[0].interactable = true; //end turn
            _playerInteractButtons[1].interactable = true; //attack 
            _playerInteractButtons[2].interactable = true; //draw card 
            
            
        }
        else
        {
            //enemy logic 
            _playerInteractButtons[0].interactable = false; //end turn
            _playerInteractButtons[1].interactable = false;
            _playerInteractButtons[2].interactable = false;
            
            // _enemyCards.heldCards.Add(drawnCard);

            //start competing 
            
        }
    }

    private void InstanceCard(CardDataModel cardDataModel, Turn turn)
    {
        GameObject copyGameObject;
        string reformatString;
        reformatString = cardDataModel.cardShapes.ToString() + " , " + cardDataModel.cardsColor.ToString() + " , "
                         + cardDataModel.cardNumberValue;
        if (turn == Turn.Enemy)
        {
            copyGameObject = Instantiate(_cardPrefab, _enemyCardsPanel.transform);
        }
        else
        {
            copyGameObject = Instantiate(_cardPrefab, _playerCardsPanel.transform);
        }

        copyGameObject.transform.GetComponentInChildren<Text>().text = reformatString;
    }

    private void EnemyMove()
    {
        //draw 
        int makeMove = Random.Range(0, 1); //draw or not draw 
        if (makeMove == 1 )
        {
            StartCoroutine(DrawCard(_enemyCards,Turn.Enemy));
            
        }
        else
        {
        //attack
            
        }

        //end turn 
    }

    IEnumerator Attack(PlayerDataModel playerDataModel, Turn turn)
    {
        yield return new WaitForSeconds(2.0f);
        _deckController.AttackTurn(playerDataModel.heldCards, turn);
    }

    IEnumerator DrawCard(PlayerDataModel playerDataModel, Turn turn)
    {
        yield return new WaitForSeconds(3.0f);
        
        CardDataModel cardData = _deckController.DrawCard();
        _enemyCards.heldCards.Add(cardData);
        InstanceCard(cardData,Turn.Player);
        //check if the draw card same color or another color 
        //if another color the cards will be ditched and he's ending his turn [player]
        if(_enemyCards.heldCards[_enemyCards.heldCards.Count -2 ].cardsColor ==  _enemyCards.heldCards[_enemyCards.heldCards.Count -1].cardsColor)
        {
            
        }
        else
        {
            //flush the cards into the graveyard 
            
            yield return null;
        }
    }
    private void EndTurnButton()
    {
        _turn = Turn.Enemy;
        for (int i = 0; i < _playerInteractButtons.Length; i++)
        {
            _playerInteractButtons[i].interactable = false; 
        }
        PlayTurn(_turn);
    }

    private void AttackButton()
    {
        StartCoroutine(Attack(_playerCards, Turn.Player));
        _playerInteractButtons[1].interactable = false; 

    }

    private void DrawCardButton()
    {
        CardDataModel cardData = _deckController.DrawCard();
        _playerCards.heldCards.Add(cardData);
        InstanceCard(cardData,Turn.Player);
        //check if the draw card same color or another color 
        //if another color the cards will be ditched and he's ending his turn [player]
        if(_playerCards.heldCards[_playerCards.heldCards.Count -2 ].cardsColor ==  _playerCards.heldCards[_playerCards.heldCards.Count -1].cardsColor)
        {
            _playerInteractButtons[2].interactable = true;
        }
        else
        {
            _playerInteractButtons[1].interactable = false;
            _playerInteractButtons[2].interactable = false;
        }
    }

    private void FlushCards(PlayerDataModel playerDataModel)
    {
        
    }
}