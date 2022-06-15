using UnityEngine;

public class Slingshot_LineRenderer : MonoBehaviour
{
    [SerializeField] private GameObject[] _objects;

    [SerializeField] private LineRenderer _line;


    private void Update()
    {
        for (int i = 0; i < _objects.Length; i++)
        {
            Vector3[] position = new Vector3[2];
            position[0] = _objects[0].transform.position;
            position[1] = _objects[1].transform.position;

            _line.SetPositions(position);
        }
    }
}