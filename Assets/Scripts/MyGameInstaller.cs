using UnityEngine;
using Zenject;

public class MyGameInstaller : MonoInstaller
{
    [SerializeField] private Player player;
    [SerializeField] private AudioManager audioManager;
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<Player>().FromInstance(player).AsSingle();
        Container.Bind<AudioManager>().FromInstance(audioManager).AsSingle();
    }
}