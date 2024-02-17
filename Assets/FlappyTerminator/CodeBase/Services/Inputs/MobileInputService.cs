namespace Assets.FlappyTerminator.CodeBase.Services.Inputs
{
    public class MobileInputService : InputService
    {
        public override bool IsFireButtonDown() => 
            TapInput;

        public override bool IsJumpButtonDown() =>
            TapInput;
    }
}