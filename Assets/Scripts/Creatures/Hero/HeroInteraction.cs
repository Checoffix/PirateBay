using UnityEngine;
using static Unity.Cinemachine.IInputAxisOwner.AxisDescriptor;

public class HeroInteraction : MonoBehaviour
{
    [SerializeField] private CheckCircleOverlap _interactionCheck;
    public void Interact()
    {
        _interactionCheck.Check();
    }
    public void DoInteraction(GameObject _obj)
    {
        var interactable = _obj.GetComponent<InteractableComponent>();
        if (interactable != null) {
            interactable.Interact();
        }
    }
}
