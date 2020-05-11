using UnityEngine;
using Zenject;

public class MyGameInstaller : MonoInstaller
{
    public Player player;
    public ObjectPool pool;

    public override void InstallBindings()
    {
        Container.Bind<ObjectPool>().FromInstance(pool).AsSingle();
        Container.BindInterfacesAndSelfTo<Player>().FromInstance(player).AsSingle();
    }
}