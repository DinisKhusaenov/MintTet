using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Profile : MonoBehaviour
{
    public event Action<CardData> FavoriteClicked;
    public event Action QuitClicked;

    [SerializeField] private Image _icon;
    [SerializeField] private Button _favorite;
    [SerializeField] private Button _quit;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _gender;
    [SerializeField] private TMP_Text _mail;
    [SerializeField] private TMP_Text _ip;
    [SerializeField] private Sprite _favoriteImage;
    [SerializeField] private Sprite _notFavoriteImage;

    private CardData _currentCard;

    private void Awake()
    {
        Hide();
    }

    public void Initialize(CardData card)
    {
        if (card.GetIcon() != null)
            _icon.sprite = card.GetIcon();

        _name.text = card.FirstName + " " + card.LastName;
        _mail.text = card.Mail;
        _ip.text = card.Ip;
        _gender.text = card.Gender;

        _currentCard = card;

        ChangeFavoriteIcon();

        _favorite.onClick.AddListener(OnFavoriteClicked);
        _quit.onClick.AddListener(Hide);
    }

    private void OnDisable()
    {
        _favorite.onClick.RemoveListener(OnFavoriteClicked);
        _quit.onClick.RemoveListener(Hide);
    }

    public void Show() => gameObject.SetActive(true);

    private void Hide()
    {
        QuitClicked?.Invoke();
        gameObject.SetActive(false);
    }

    private void OnFavoriteClicked()
    {
        FavoriteClicked?.Invoke(_currentCard);
        ChangeFavoriteIcon();
    }

    private void ChangeFavoriteIcon()
    {
        if (_currentCard.IsFavorite)
        {
            _favorite.image.sprite = _favoriteImage;
        }
        else
        {
            _favorite.image.sprite = _notFavoriteImage;
        }
    }
}
