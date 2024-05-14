using UnityEngine;

namespace Core.Behaviour
{
    public class TargetDetector
    {
         private float _detectRadius;
         private Transform _ownerTransform;

        public IEnemyTarget Target { get; private set; }

        public TargetDetector(Transform owner, float detectRadius)
        {
            _ownerTransform = owner;
            _detectRadius = detectRadius;
        }
        
        public bool TryFindTarget()
        {
            var colls = Physics.OverlapSphere(_ownerTransform.position, _detectRadius);
            if (colls.Length <= 0)
                return false;

            foreach (var coll in colls)
            {
                if (coll.TryGetComponent(out IEnemyTarget target))
                {
                    Target = target;
                    return true;
                }
            }

            Target = null;
            return false;
        }

        public void OnDrawGizmos()
        {
            if(_ownerTransform == null)
                return;
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_ownerTransform.position, _detectRadius);
        }
    }
}