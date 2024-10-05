using Photon.Deterministic;

namespace Quantum.Amax.PhotonQuantumDemo
{
    using UnityEngine.Scripting;

    [Preserve]
    public unsafe class PlayerCharacterSystem : SystemMainThreadFilter<PlayerCharacterSystem.Filter>
    {
        public override void Update(Frame f, ref Filter filter)
        {
            var input = f.GetPlayerInput(filter.PlayerCharacter->PlayerRef);
            var movement = new FPVector3(input->Direction.X, 0, input->Direction.Y);
            var controller = filter.CharacterController;

            var controllerConfig = f.PlayerCharacterController3DConfig;
            
            controller->MaxSpeed = input->Sprint
                ? controllerConfig.MaxSpeed * f.GameConfig.playerSprintSpeedBoost
                : controllerConfig.MaxSpeed;

            controller->Move(
                f,
                filter.Entity,
                movement);
            
            if (input->Jump.WasPressed)
            {
                controller->Jump(f, false, f.GameConfig.playerCharacterJumpForce);
            }

            if (input->Direction != FPVector2.Zero)
            {
                filter.PlayerCharacter->Direction = input->Direction.Normalized;
            } 
        }

        public struct Filter
        {
            public EntityRef Entity;
            public PlayerCharacter* PlayerCharacter;
            public CharacterController3D* CharacterController;
        }
    }
}
