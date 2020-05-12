using UnityEngine;
using Zenject;

public class PoolFactoriesInstaller : MonoInstaller
{
    public BulletFactoryPool bulletFactoryPool;

    public override void InstallBindings()
    {
        Container.BindFactory<Bullet, BulletFactoryPool.Factory<Bullet>>().FromComponentInNewPrefab(bulletFactoryPool.Prefab).AsSingle();
        Container.Bind<BulletFactoryPool>().FromInstance(bulletFactoryPool).AsSingle();
    }
}