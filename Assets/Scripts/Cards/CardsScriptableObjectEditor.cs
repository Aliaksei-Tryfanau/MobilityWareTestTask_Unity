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

                    CardScriptableInfo cardToAdd = new CardScriptableInfo();
                    cardToAdd.CardInfo = new CardInfo(cardSuit, cardRank);
                    cardsSO.cardInfos.Add(cardToAdd);
                }
            }
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }
}
