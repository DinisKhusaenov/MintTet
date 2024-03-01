using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private List<CardData> _cards;

    public PlayerData()
    {
        _cards = new List<CardData> ();

        IsStartDataLoaded = false;
    }

    [JsonConstructor]
    public PlayerData(List<CardData> cards, bool isStartDataLoaded)
    {
        _cards = cards;
        IsStartDataLoaded = isStartDataLoaded;
    }

    public bool IsStartDataLoaded { get; set; }

    public IEnumerable<CardData> Cards => _cards;

    public void AddCard(CardData card)
    {
        _cards.Add(card);
    }

    public void RemoveCard(CardData card)
    {
        if (_cards.Contains(card))
            _cards.Remove(card);
    }
}
