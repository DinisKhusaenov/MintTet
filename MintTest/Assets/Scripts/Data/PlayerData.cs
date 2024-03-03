using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    private List<CardData> _cards;
    private List<CardData> _favoriteCards;
    private List<string> _imagePaths;

    public PlayerData()
    {
        _cards = new List<CardData> ();
        _favoriteCards = new List<CardData> ();
        _imagePaths = new List<string> ();

        IsStartDataLoaded = false;
    }

    [JsonConstructor]
    public PlayerData(List<CardData> cards, List<CardData> favoriteCards, List<string> imagePaths, bool isStartDataLoaded)
    {
        _cards = cards;
        _favoriteCards = favoriteCards;
        _imagePaths = imagePaths;

        IsStartDataLoaded = isStartDataLoaded;
    }

    public bool IsStartDataLoaded { get; set; }

    public IEnumerable<CardData> Cards => _cards;

    public IEnumerable<CardData> FavoriteCards => _favoriteCards;

    public void AddCard(CardData card)
    {
        _cards.Add(card);
    }

    public void RemoveCard(CardData card)
    {
        if (_cards.Contains(card))
            _cards.Remove(card);
    }

    public void AddImagePath(string path)
    {
        _imagePaths.Add(path);
    }

    public string GetRandomSpritePath()
    {
        if (_imagePaths.Count == 0) return null;

        return _imagePaths[Random.Range(0, _imagePaths.Count)];
    }

    public void AddFavorite(CardData card)
    {
        _favoriteCards.Add(card);
    }

    public void RemoveFavorite(CardData card)
    {
        if (_favoriteCards.Contains(card))
            _favoriteCards.Remove(card);
    }
}
