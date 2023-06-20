using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct CardInfo
{
    public CardSuits CardSuit;
    public CardTypes CardType;

    public CardInfo(CardSuits cardSuit, CardTypes cardType)
    {
        CardSuit = cardSuit;
        CardType = cardType;
    }
}

public class CardController : MonoBehaviour
{
    [SerializeField]
    private Image _cardImage;
    [SerializeField]
    private Button _button;
    [SerializeField]
    private GameObject _holdGO;

    public CardInfo CardInfo { get; private set; }
    public bool IsHold => _holdGO.activeSelf;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnCardButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnCardButtonClick);
    }

    private void Start()
    {
        _button.enabled = false;
        _cardImage.sprite = null;
    }

    public void Setup(CardInfo cardInfo, Sprite cardSprite) 
    {
        CardInfo = cardInfo;
        _cardImage.sprite = cardSprite;
        _button.enabled = true;
    }

    private void OnCardButtonClick()
    {
        _holdGO.SetActive(!_holdGO.activeSelf);
    }
}
