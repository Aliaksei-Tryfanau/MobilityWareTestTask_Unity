using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    #region Singleton
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
    #endregion

    [SerializeField]
	private int _startingCredit = 100;

	[SerializeField]
	private HandCombinationsRewardsScriptable _combinationsRewardsSO;

	public int CurrentCredit { get; private set; }
	public List<CardInfo> CurrentHand { get; private set; } = new List<CardInfo>();

	private List<CardInfo> _cardDeck = new List<CardInfo>();
	private int _currentBet = 0;

    private void OnEnable()
    {
		UIManager.Instance.EventBetPressed += OnBetPressed;
    }

    private void OnDisable()
    {
        UIManager.Instance.EventBetPressed -= OnBetPressed;
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            CurrentCredit = _startingCredit;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
	{
		GenerateCardDeck();
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

	private void DrawCards()
	{
        CurrentHand.Clear();

        for (int i = 0; i < 5; i++)
		{
			CardInfo cardToAdd = _cardDeck[Random.Range(0, _cardDeck.Count)];
            _cardDeck.Remove(cardToAdd);
            CurrentHand.Add(cardToAdd);
        }

		UIManager.Instance.SetupHand(CurrentHand);
	}

	private void OnBetPressed()
	{
		_currentBet++;
		CurrentCredit--;

		if (_currentBet == 5)
		{
            OnDrawPressed();
		}
    }

	private void OnDrawPressed()
	{
		if (CurrentHand.Count == 0)
		{
			DrawCards();
		}
		else if (true) //check if have hold cards
		{

		}
		else
		{
			//check win
		}
	}
}