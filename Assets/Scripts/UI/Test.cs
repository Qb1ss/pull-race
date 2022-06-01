using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    Button _button;


    private void Start()
    {
        _button = GetComponent<Button>();

        _button.onClick.AddListener(() => Debug.Log("Work!"));
    }
}
