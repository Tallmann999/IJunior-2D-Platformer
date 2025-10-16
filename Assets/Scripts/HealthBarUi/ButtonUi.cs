using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUi : MonoBehaviour
{
    [SerializeField] private Button _button;

    public event Action ButtonClick;

    private void OnEnable()
    {
        _button.onClick.AddListener(HandleButtonClick);
    }
    private void OnDisable()
    {
        _button.onClick.RemoveListener(HandleButtonClick);
    }

    private void HandleButtonClick()
    {
        ButtonClick?.Invoke();
    }
}
