using Zenject;

public class DataInstaller : MonoInstaller
{
    private PersistentData _persistentData;
    private DataLocalProvider _dataProvider;

    private UsersLoader _usersLoader;

    public override void InstallBindings()
    {
        _persistentData = new PersistentData();
        _dataProvider = new DataLocalProvider(_persistentData);

        LoadDataOrInit();

        Container.BindInterfacesAndSelfTo<PersistentData>().FromInstance(_persistentData).AsSingle();
        Container.BindInterfacesAndSelfTo<DataLocalProvider>().FromInstance(_dataProvider).AsSingle();
    }

    private void LoadDataOrInit()
    {
        if (_dataProvider.TryLoad() == false)
        {
            _persistentData.PlayerData = new PlayerData();
        }
        DownloadData();
    }

    private void DownloadData()
    {
        var imageLoader = new ImageLoader(this, _persistentData);
        _usersLoader = new UsersLoader(this, _persistentData, imageLoader);

        Container.Bind<UsersLoader>().FromInstance(_usersLoader).AsSingle();
    }
}
