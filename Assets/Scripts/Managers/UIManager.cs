using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager Instance 
	{ 
		get 
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<UIManager>();
			}

			return _instance;
		} 
	}

	private static UIManager _instance;
    #endregion

    public event Action EventBetPressed;
	public event Action EventCancelPressed;
	public event Action EventDrawPressed;

	[SerializeField]
	private Text _currentBalanceText;
	[SerializeField]
	private Text _winningText;
	[SerializeField]
	private Button _betButton;
	[SerializeField]
	private Button _cancelButton;
	[SerializeField]
	private Button _drawButton;
	[SerializeField]
	private Text _betText;
	[SerializeField]
	private CardController[] _cardControllers;
    [SerializeField]
    private CardsScriptableObject _cardsSO;

    private void OnEnable()
    {
        _betButton.onClick.AddListener(OnBetPressed);
		_cancelButton.onClick.AddListener(OnCancelPressed);
        _drawButton.onClick.AddListener(OnDrawPressed);
		GameManager.Instance.EventBetChanged += OnBetChanged;
		GameManager.Instance.EventHandResolved += OnHandResolved;
    }

    private void OnDisable()
    {
        _betButton.onClick.RemoveListener(OnBetPressed);
        _cancelButton.onClick.RemoveListener(OnCancelPressed);
        _drawButton.onClick.RemoveListener(OnDrawPressed);

        if (GameManager.Instance != null)
		{
            GameManager.Instance.EventBetChanged -= OnBetChanged;
            GameManager.Instance.EventHandResolved -= OnHandResolved;
        }
    }

    void Start()
	{
		_winningText.text = string.Empty;
        _currentBalanceText.text = $"Balance: {GameManager.Instance.CurrentCredit} Credits";
    }

	public void SetupHand(List<CardInfo> cardInfos)
	{
        _betButton.enabled = false;
        _cancelButton.enabled = false;

        for (int i = 0; i < _cardControllers.Length; i++) 
		{
			_cardControllers[i].Setup(_cardsSO.GetCardSprite(cardInfos[i]));
        }
	}

	public List<int> GetHoldCardsIndexes()
	{
		List<int> indexes = new List<int>();

        for (int i = 0; i < _cardControllers.Length; i++)
		{
			if (_cardControllers[i].IsHold)
			{
				indexes.Add(i);
			}
        }

		return indexes;
	}

	private void OnBetPressed()
	{
		EventBetPressed?.Invoke();
    }

	private void OnCancelPressed() 
	{
		EventCancelPressed?.Invoke();
	}

    private void OnDrawPressed()
    {
        EventDrawPressed?.Invoke();
    }

    private void OnBetChanged(int newBetAmount, int newCredits)
	{
		_betText.text = $"Current bet: {newBetAmount}";
        _currentBalanceText.text = $"Balance: {newCredits} Credits";
    }

	private void OnHandResolved(int rewardValue, string rewardText)
	{
		foreach(CardController cardController in _cardControllers) 
		{
            cardController.Reset();

        }

        _winningText.text = rewardValue == 0 ? $"{rewardText}" : $"{rewardText}! You won {rewardValue} credits";
        _betText.text = "Current bet: 0";
        _currentBalanceText.text = $"Balance: {GameManager.Instance.CurrentCredit} Credits";
        _betButton.enabled = true;
        _cancelButton.enabled = true;
    }
}