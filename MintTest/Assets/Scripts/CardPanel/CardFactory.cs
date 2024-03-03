using UnityEngine;

public class CardFactory
{
    private CardView _cardView;

    public CardFactory(CardView cardView)
    {
        _cardView = cardView;
    }

    public CardView Get(CardData cardData, Transform content)
    {
        var card = Object.Instantiate(_cardView);
        card.transform.SetParent(content);

        card.Initialize(cardData);

        return card;
    } 
}
