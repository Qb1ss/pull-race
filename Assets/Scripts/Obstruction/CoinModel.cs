using UnityEngine;
using Character;
using DG.Tweening;

public class CoinModel : MonoBehaviour
{
    [SerializeField] private ParticleSystem _destroy = null;


    private void Update()
    {
        gameObject.transform.Rotate(new Vector3(10f, 0f, 0f), 2f);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Character_Movement>(out Character_Movement character))
        {
            Instantiate(_destroy, gameObject.transform.position, Quaternion.identity);

            Destroy(gameObject);
        }       
    }
}
