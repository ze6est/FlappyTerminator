using UnityEngine;

namespace Assets.FlappyTerminator.CodeBase.Services.Inputs
{
    public class StandaloneInputService : InputService
    {
        public override bool IsFireButtonDown() =>
            StandaloneInput();

        public override bool IsJumpButtonDown() =>
            StandaloneInput();

        private bool StandaloneInput()
        {
            bool input = TapInput;
            input = KeyboardInput(input);

            return input;
        }

        private bool KeyboardInput(bool input)
        {
            if (input == false)
                input = Input.GetKeyDown(KeyCode.Space);
            return input;
        }
    }
}