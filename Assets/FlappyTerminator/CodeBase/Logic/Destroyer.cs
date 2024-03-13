using Assets.FlappyTerminator.CodeBase.Logic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    { 
        if(collision.collider.gameObject.TryGetComponent(out ObjectSwitch objectSwitch))
            objectSwitch.DisableObject();        
    }
}