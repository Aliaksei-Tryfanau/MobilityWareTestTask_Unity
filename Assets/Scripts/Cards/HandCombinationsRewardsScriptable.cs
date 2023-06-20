using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RewardsSO", menuName = "Cards/Create Rewards SO")]
public class HandCombinationsRewardsScriptable : ScriptableObject
{
    [Serializable]
    public struct RewardInfo
    {
        public HandTypes HandType;
        public int Reward;
        public string RewardText;
    }

    public List<RewardInfo> Rewards = new List<RewardInfo>();

    public int GetRewardValue(HandTypes handType)
    {
        return Rewards.Find(r => r.HandType == handType).Reward;
    }

    public string GetRewardText(HandTypes handType) 
    {
        return Rewards.Find(r => r.HandType == handType).RewardText;
    }
}
