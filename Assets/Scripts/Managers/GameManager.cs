using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance
	{
		get 
		{
			if (_instance == null)
			{
                _instance = FindObjectOfType<GameManager>();
			}

			return _instance;
		}
	}

	private static GameManager _instance;

	[SerializeField]
	private int _startingCredit = 100;

	public int CurrentCredit { get; private set; }

	private List<CardInfo> _cardDeck = new List<CardInfo>();

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
			return;
        }

        CurrentCredit = _startingCredit;
    }

    private void Start()
	{
		GenerateCardDeck();
		DealCards();
    }

	private void GenerateCardDeck()
    {
        _cardDeck.Clear();

        foreach (CardSuits cardSuit in Enum.GetValues(typeof(CardSuits)))
		{
			if (cardSuit == CardSuits.None)
			{
				continue;
			}

			foreach (CardTypes cardType in Enum.GetValues(typeof(CardTypes)))
			{
				if (cardType == CardTypes.None)
				{
					continue;
				}

				_cardDeck.Add(new CardInfo(cardSuit, cardType));
            }
		}
    }

	private void DealCards()
	{
		List<CardInfo> cardsToDeal = new List<CardInfo>();

		for (int i = 0; i < 5; i++)
		{
			CardInfo cardToAdd = _cardDeck[Random.Range(0, _cardDeck.Count)];
            _cardDeck.Remove(cardToAdd);
            cardsToDeal.Add(cardToAdd);
        }

		UIManager.Instance.SetupHand(cardsToDeal);
	}
}