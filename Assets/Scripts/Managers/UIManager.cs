using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
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

	[SerializeField]
	private Text _currentBalanceText = null;

	[SerializeField]
	private Text _winningText = null;

	[SerializeField]
	private Button _betButton = null;

	[SerializeField]
	private CardController[] _cardControllers;

    [SerializeField]
    private CardsScriptableObject _cardsSO;

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
		}
    }

    void Start()
	{
		_betButton.onClick.AddListener(OnBetButtonPressed);
		_currentBalanceText.text = $"Balance: {GameManager.Instance.CurrentCredit} Credits";

    }

	public void SetupHand(List<CardInfo> cardInfos)
	{ 
		for (int i = 0; i < _cardControllers.Length; i++) 
		{
			_cardControllers[i].Setup(cardInfos[i], _cardsSO.GetCardSprite(cardInfos[i]));
        }
	}

	private void OnBetButtonPressed()
	{

	}
}