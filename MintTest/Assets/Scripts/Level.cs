using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    [SerializeField] private Button _employee;
    [SerializeField] private Button _favorite;
    [SerializeField] private CardsScreen _employeeScreen;
    [SerializeField] private CardsScreen _favoriteScreen;
    [SerializeField] private Sprite _hoverLeft;
    [SerializeField] private Sprite _hoverRight;
    [SerializeField] private Sprite _hoverWhite;

    private void Awake()
    {
        OnEmployeeClicked();
    }

    private void OnEnable()
    {
        _employee.onClick.AddListener(OnEmployeeClicked);
        _favorite.onClick.AddListener(OnFavoriteClicked);
    }

    private void OnDisable()
    {
        _employee.onClick.RemoveListener(OnEmployeeClicked);
        _favorite.onClick.RemoveListener(OnFavoriteClicked);
    }

    private void OnEmployeeClicked()
    {
        _employee.image.sprite = _hoverLeft;
        _favorite.image.sprite = _hoverWhite;

        _employeeScreen.ShowAllCards();

        _employeeScreen.gameObject.SetActive(true);
        _favoriteScreen.gameObject.SetActive(false);
    }

    private void OnFavoriteClicked()
    {
        _employee.image.sprite = _hoverWhite;
        _favorite.image.sprite = _hoverRight;

        _favoriteScreen.ShowFavoriteCards();

        _favoriteScreen.gameObject.SetActive(true);
        _employeeScreen.gameObject.SetActive(false);
    }
}
