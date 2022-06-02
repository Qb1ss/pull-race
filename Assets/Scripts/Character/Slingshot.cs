using UnityEngine;

namespace Character.Slingshot
{
    public class Slingshot : MonoBehaviour
    {
        [SerializeField] private LineRenderer _line;

        [SerializeField] private Transform _slingshot;
        [SerializeField] private Transform _border;


        private void Start()
        {
            _line.positionCount = 0;
        }


        private void Update()
        {
            RenderLine();
        }

        private void RenderLine()
        {
            _line.positionCount = 2;

            Vector3[] point = new Vector3[2];

            point[0] = _slingshot.position;
            point[1] = _border.position;

            _line.SetPositions(point);
        }
    }
}