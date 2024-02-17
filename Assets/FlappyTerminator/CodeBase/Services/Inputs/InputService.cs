using UnityEngine;

namespace Assets.FlappyTerminator.CodeBase.Services.Inputs
{
    public abstract class InputService : IInputService
    {
        public abstract bool IsFireButtonDown();
        public abstract bool IsJumpButtonDown();
        
        protected bool TapInput =>
            Input.GetMouseButtonDown(0);
    }
}