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
    [SerializeField] private Sprite _favoriteImage;
    [SerializeField] private Sprite _notFavoriteImage;

    public CardData CurrentCard { get; private set;}

    public void Initialize(CardData card)
    {
        if (card.GetIcon() != null)
            _icon.sprite = card.GetIcon();

        _name.text = card.FirstName + " " + card.LastName;
        _mail.text = card.Mail;
        _ip.text = card.Ip;

        CurrentCard = card;

        ChangeFavoriteIcon();
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
        FavoriteClicked?.Invoke(CurrentCard);

        ChangeFavoriteIcon();
    }

    private void OnCheckClicked()
    {
        CheckClicked?.Invoke(CurrentCard);
    }

    private void ChangeFavoriteIcon()
    {
        if (CurrentCard.IsFavorite)
        {
            _favorite.image.sprite = _favoriteImage;
        }
        else
        {
            _favorite.image.sprite = _notFavoriteImage;
        }
    }
}
