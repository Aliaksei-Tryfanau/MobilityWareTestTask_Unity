using System;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    public event Action<CardController> EventCardButtonClick;

    [SerializeField]
    private Image _cardImage;
    [SerializeField]
    private Button _button;
    [SerializeField]
    private GameObject _holdGO;

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

    public void Setup(Sprite cardSprite) 
    {
        _cardImage.sprite = cardSprite;
        _button.enabled = true;
    }

    public void Reset()
    {
        _holdGO.SetActive(false);
        _button.enabled = false;
    }

    private void OnCardButtonClick()
    {
        _holdGO.SetActive(!_holdGO.activeSelf);
        EventCardButtonClick?.Invoke(this);
    }
}
