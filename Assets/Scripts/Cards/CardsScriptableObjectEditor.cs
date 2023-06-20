using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CardsScriptableObject))]
public class CardsScriptableObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Fill Card Data", GUILayout.Height(30), GUILayout.Width(120)))
        {
            var cardsSO = target as CardsScriptableObject;
            cardsSO.cardInfos.Clear();

            foreach (CardTypes cardType in Enum.GetValues(typeof(CardTypes)))
            {
                if (cardType == CardTypes.None)
                {
                    continue;
                }

                foreach (CardSuits cardSuit in Enum.GetValues(typeof(CardSuits)))
                {
                    if (cardSuit == CardSuits.None)
                    {
                        continue;
                    }

                    CardScriptableInfo cardToAdd = new CardScriptableInfo();
                    cardToAdd.CardInfo = new CardInfo(cardSuit, cardType);
                    cardsSO.cardInfos.Add(cardToAdd);
                }
            }
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }
}
