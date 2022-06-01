using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Test : MonoBehaviour
{
    public static UnityEvent OnStartGame = new UnityEvent();

    private Button _button;


    private void Start()
    {
        _button = GetComponent<Button>();

        _button.onClick.AddListener(() => OnStartGame?.Invoke());
    }
}
