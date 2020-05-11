using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public interface IGun
{
    Transform Barrel { get; }
    ObjectPool Pool { get; }
    void Fire();
}
