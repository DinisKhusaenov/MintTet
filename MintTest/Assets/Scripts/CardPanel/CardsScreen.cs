using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CardsScreen : MonoBehaviour
{
    [SerializeField] private CardsPanel _cardsPanel;

    private IDataProvider _dataProvider;
    private IPersistentData _persistentData;
    private UsersLoader _usersLoader;

    [Inject]
    private void Construct(IDataProvider dataProvider, IPersistentData persistentData, UsersLoader usersLoader)
    {
        _dataProvider = dataProvider;
        _persistentData = persistentData;
        _usersLoader = usersLoader;

        if (_persistentData.PlayerData.IsStartDataLoaded)
            ShowAllCards();

        _usersLoader.DataLoaded += ShowAllCards;
        _persistentData.PlayerData.IsStartDataLoaded = true;
    }

    private void OnDisable()
    {
        _usersLoader.DataLoaded -= ShowAllCards;

        _dataProvider.Save();
    }

    public void ShowAllCards()
    {
        _cardsPanel.Show((List<CardData>)_persistentData.PlayerData.Cards);
    }

    public void ShowFavoriteCards()
    {
        _cardsPanel.Show((List<CardData>)_persistentData.PlayerData.FavoriteCards);
    }
}