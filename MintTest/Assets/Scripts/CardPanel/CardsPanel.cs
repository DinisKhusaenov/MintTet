using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CardsPanel : MonoBehaviour
{
    private CardFactory _cardFactory;
    private Transform _content;

    [Inject]
    private void Construct(CardFactory cardFactory, Transform content)
    {
        _cardFactory = cardFactory;
        _content = content;
    }

    public void Show(List<CardData> cards)
    {
        foreach (CardData card in cards)
        {
            _cardFactory.Get(card, _content);
        }
    }
}
