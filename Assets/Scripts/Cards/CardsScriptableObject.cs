using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct CardScriptableInfo
{
    public CardInfo CardInfo;
    public Sprite CardSprite;
}

[CreateAssetMenu(fileName = "CardsSO", menuName = "Cards/Create Cards SO")]
public class CardsScriptableObject : ScriptableObject
{
    public List<CardScriptableInfo> cardInfos;

    public Sprite GetCardSprite(CardInfo cardInfo)
    {
        return cardInfos.Find(c => c.CardInfo.CardSuit == cardInfo.CardSuit && c.CardInfo.CardType == cardInfo.CardType).CardSprite;
    }
}
