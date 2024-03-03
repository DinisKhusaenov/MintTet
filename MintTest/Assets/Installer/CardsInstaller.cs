using UnityEngine;
using Zenject;

public class CardsInstaller : MonoInstaller
{
    [SerializeField] private CardView _cardView;
    [SerializeField] private CardsPanel _cardsPanel;
    [SerializeField] private Profile _profile;

    public override void InstallBindings()
    {
        BindFactory();
        BindCardsPanel();
        BindProfile();
    }

    private void BindProfile()
    {
        Container.Bind<Profile>().FromInstance(_profile).AsSingle();
    }

    private void BindCardsPanel()
    {
        Container.Bind<CardsPanel>().FromInstance(_cardsPanel).AsSingle().NonLazy();
    }

    private void BindFactory()
    {
        Container.Bind<CardFactory>().AsSingle().WithArguments(_cardView);
    }
}
