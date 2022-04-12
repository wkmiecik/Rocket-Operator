using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    public GameObject inputManagerPrefab;
    public GameObject guiManager;
    public GameObject levelManager;
    public GameObject adsManager;

    public override void InstallBindings()
    {
        Container.Bind<InputManager>().FromComponentInNewPrefab(inputManagerPrefab).AsSingle();

        Container.Bind<GuiManager>().FromComponentInNewPrefab(guiManager).AsSingle();

        Container.Bind<LevelManager>().FromComponentInNewPrefab(levelManager).AsSingle();

        Container.Bind<AdsManager>().FromComponentInNewPrefab(adsManager).AsSingle();
    }
}