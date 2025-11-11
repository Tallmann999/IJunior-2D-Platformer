using UnityEngine;

public class SpecialForceActivator : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private VampirismAbility _vampirismForce;

    protected virtual void OnEnable()
    {
        _inputReader.SpecialForceUse += OnForceActivation;
    }

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
    }

    private void OnForceActivation(bool isPressedButton)
    {
        _vampirismForce.OnUseForce(isPressedButton);
    }

    protected virtual void OnDisable()
    {
        _inputReader.SpecialForceUse -= OnForceActivation;
    }
}
