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
    }

    public List<RewardInfo> Rewards = new List<RewardInfo>();
}
