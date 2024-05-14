using System;
using UnityEngine;

namespace Core.Behaviour
{
    public class HitParticleInvoker : MonoBehaviour
    {
        public event Action<Vector3> OnHit;
        
        public void InvokeHitParticle(Vector3 pos) => OnHit?.Invoke(pos);
    }
}