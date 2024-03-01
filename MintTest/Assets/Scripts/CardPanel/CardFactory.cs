using UnityEngine;
using UnityEngine.UI;

public class CardFactory
{
    private CardView _cardView;

    public CardFactory(CardView cardView)
    {
        _cardView = cardView;
    }

    public CardView Get(CardData cardData, Transform content)
    {
        var card = Object.Instantiate(_cardView, content.position, Quaternion.identity);

        card.Initialize(cardData);

        return card;
    } 
}
