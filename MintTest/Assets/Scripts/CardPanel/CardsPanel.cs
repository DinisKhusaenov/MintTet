using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class CardsPanel : MonoBehaviour
{
    public const int MaxCards = 10;

    [SerializeField] private Transform _content;

    private List<CardView> _cards;
    private List<CardView> _savedCards;
    private CardFactory _cardFactory;
    private Profile _profile;
    private IPersistentData _persistentData;

    [Inject]
    private void Construct(CardFactory cardFactory, IPersistentData persistentData, Profile profile)
    {
        _cardFactory = cardFactory;
        _persistentData = persistentData;
        _profile = profile;
        _cards = new List<CardView>();
        _savedCards = new List<CardView>();
    }

    public void Show(List<CardData> cards)
    {
        Clear();

        int count = 0;

        if (cards.Count > MaxCards)
            count = MaxCards;
        else
            count = cards.Count;

        for (int i = 0; i < count; i++)
        {
            var cardView = _cardFactory.Get(cards[i], _content);
            _cards.Add(cardView);

            cardView.FavoriteClicked += AddOrRemoveFavorite;
            cardView.CheckClicked += ShowProfile;
        }
    }

    private void Clear()
    {
        foreach (var cardView in _cards)
        {
            cardView.FavoriteClicked -= AddOrRemoveFavorite;
            cardView.CheckClicked -= ShowProfile;
            Destroy(cardView.gameObject);
        }

        _cards.Clear(); 
    }

    private void AddOrRemoveFavorite(CardData card)
    {
        if (!card.IsFavorite)
        {
            _persistentData.PlayerData.AddFavorite(card);
            card.IsFavorite = true;
        }
        else
        {
            _persistentData.PlayerData.RemoveFavorite(card);
            card.IsFavorite = false;
        }
    }

    private void ShowProfile(CardData card)
    {
        _profile.Show();
        _profile.Initialize(card);
        _profile.FavoriteClicked += AddOrRemoveFavorite;
        _profile.QuitClicked += OnProfileQuitted;

        _savedCards.Clear();
    }

    private void OnProfileQuitted()
    {
        _savedCards.AddRange(_cards);
        Clear();

        Show(_savedCards.Select(_savedCards => _savedCards.CurrentCard).ToList());

        _profile.FavoriteClicked -= AddOrRemoveFavorite;
        _profile.QuitClicked -= OnProfileQuitted;
    }
}
