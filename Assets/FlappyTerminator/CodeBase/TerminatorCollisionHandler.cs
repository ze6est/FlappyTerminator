using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Terminator))]
public class TerminatorCollisionHandler : MonoBehaviour
{
    public event UnityAction<IInteractable> CollisionDetected;

    private void OnValidate()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out IInteractable interactable))
            CollisionDetected?.Invoke(interactable);
    }
}