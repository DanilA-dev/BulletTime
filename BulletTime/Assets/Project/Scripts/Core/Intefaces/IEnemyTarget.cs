using UnityEngine;

public interface IEnemyTarget
{
    public Transform TargetTransform { get; }
    public IDamagable Damagable { get; }
}