using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    public event Action<CardData> FavoriteClicked;
    public event Action<CardData> CheckClicked;

    [SerializeField] private Image _icon;
    [SerializeField] private Button _favorite;
    [SerializeField] private Button _check;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _mail;
    [SerializeField] private TMP_Text _ip;

    private CardData _currentCard;

    public void Initialize(CardData card)
    {
        _icon = card.Icon;
        _name.text = card.FirstName + " " + card.LastName;
        _mail.text = card.Mail;
        _ip.text = card.Ip;

        _currentCard = card;
    }

    private void OnEnable()
    {
        _favorite.onClick.AddListener(OnFavoriteClicked);
        _check.onClick.AddListener(OnCheckClicked);
    }

    private void OnDisable()
    {
        _favorite.onClick.RemoveListener(OnFavoriteClicked);
        _check.onClick.RemoveListener(OnCheckClicked);
    }

    private void OnFavoriteClicked()
    {
        FavoriteClicked?.Invoke(_currentCard);
    }

    private void OnCheckClicked()
    {
        CheckClicked?.Invoke(_currentCard);
    }
}
