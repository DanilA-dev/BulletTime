using UnityEngine;

namespace View
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;

        private void Start() => DisableLine();

        public void EnalbeLine() => _lineRenderer.enabled = true;

        public void DisableLine() => _lineRenderer.enabled = false;

        public void SetArrowLine(Vector3 start,Vector3 end)
        {
            var endPoint = _lineRenderer.positionCount - 1;
            _lineRenderer.SetPosition(0, start);
            _lineRenderer.SetPosition(endPoint, end);
        }
        
    }
}