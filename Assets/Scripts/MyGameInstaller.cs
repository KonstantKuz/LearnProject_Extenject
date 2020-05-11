using UnityEngine;
using Zenject;

public class MyGameInstaller : MonoInstaller
{
    public ObjectPool pool;

    public override void InstallBindings()
    {
        Container.Bind<ObjectPool>().FromInstance(pool).AsSingle();
    }
}