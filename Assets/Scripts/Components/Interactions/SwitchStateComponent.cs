using UnityEngine;

public class SwitchStateComponent : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Collider2D _collider2D;
    [SerializeField] private string _stateName;
    private static int IsOpening = Animator.StringToHash("is-opening");
    [SerializeField] private bool _state;
    private void Start()
    {
        IsOpening = Animator.StringToHash(_stateName);

    }

    [ContextMenu("Switch")]
    public void OnChangeState()
    {
        _state = !_state;
        _animator.SetBool(IsOpening, _state);
        _collider2D.enabled = !_state;
    }
}
