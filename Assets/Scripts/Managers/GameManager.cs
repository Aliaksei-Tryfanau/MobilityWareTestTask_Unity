using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public struct CardInfo
{
    public CardSuites CardSuit;
    public CardRanks CardRank;

    public CardInfo(CardSuites cardSuit, CardRanks cardRank)
    {
        CardSuit = cardSuit;
        CardRank = cardRank;
    }
}
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

    public event Action<int, int> EventBetChanged;
	public event Action<int, string> EventHandResolved;

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
		UIManager.Instance.EventCancelPressed += OnCancelPressed;
		UIManager.Instance.EventDrawPressed += OnDrawPressed;
    }

    private void OnDisable()
    {
		if (UIManager.Instance != null)
		{
            UIManager.Instance.EventBetPressed -= OnBetPressed;
            UIManager.Instance.EventCancelPressed -= OnCancelPressed;
            UIManager.Instance.EventDrawPressed -= OnDrawPressed;
        }
    }

    private void Awake()
    {
        CurrentCredit = _startingCredit;
    }

    private void Start()
	{
		GenerateCardDeck();
    }

	private void GenerateCardDeck()
    {
        _cardDeck.Clear();

        foreach (CardSuites cardSuit in Enum.GetValues(typeof(CardSuites)))
		{
			if (cardSuit == CardSuites.None)
			{
				continue;
			}

			foreach (CardRanks cardRank in Enum.GetValues(typeof(CardRanks)))
			{
				if (cardRank == CardRanks.None)
				{
					continue;
				}

				_cardDeck.Add(new CardInfo(cardSuit, cardRank));
            }
		}
    }

	private void OnBetPressed()
	{
        if (_currentBet == 5)
        {
			return;
        }

        _currentBet++;
		CurrentCredit--;
		EventBetChanged.Invoke(_currentBet, CurrentCredit);
    }

	private void OnCancelPressed()
	{
		if (_currentBet == 0)
		{
			return;
		}

		CurrentCredit += _currentBet;
		_currentBet = 0;
        EventBetChanged.Invoke(_currentBet, CurrentCredit);
    }

	private void OnDrawPressed()
	{
		if (_currentBet == 0)
		{
			return;
		}

		if (CurrentHand.Count == 0)
		{
			DrawCards();
		}
		else
		{
			ReDrawCards();
        }
	}

    private void DrawCards()
    {
        for (int i = 0; i < 5; i++)
        {
            CardInfo cardToAdd = _cardDeck[Random.Range(0, _cardDeck.Count)];
            _cardDeck.Remove(cardToAdd);
            CurrentHand.Add(cardToAdd);
        }

        UIManager.Instance.SetupHand(CurrentHand);
    }

	private void ReDrawCards()
	{
		List<int> reDrawIndexes = UIManager.Instance.GetHoldCardsIndexes();

		if (reDrawIndexes.Count != 0)
		{
			foreach (int index in reDrawIndexes)
			{
                CardInfo cardToReDraw = _cardDeck[Random.Range(0, _cardDeck.Count)];
                _cardDeck.Remove(cardToReDraw);
                CurrentHand[index] = cardToReDraw;
            }

            UIManager.Instance.SetupHand(CurrentHand);
        }

		HandTypes handCombination = GetHandCombination();
		int reward = 0;

		if (handCombination != HandTypes.None)
		{
			reward = _combinationsRewardsSO.GetRewardValue(handCombination) * _currentBet;
            CurrentCredit += reward;
            EventHandResolved.Invoke(reward, _combinationsRewardsSO.GetRewardText(handCombination));
        }
		else
		{
			EventHandResolved.Invoke(0, "No combination");
        }

        _currentBet = 0;
        CurrentHand.Clear();
        GenerateCardDeck();
    }

	private HandTypes GetHandCombination()
	{

		return HandTypes.None;
	}
}