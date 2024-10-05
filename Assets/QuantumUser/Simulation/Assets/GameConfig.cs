using UnityEngine.Serialization;

namespace Quantum
{
    using Photon.Deterministic;
    using UnityEngine;

    public class GameConfig : AssetObject
    {
        [Header("Player Character")] 
        public EntityPrototype playerPrototype;
        public CharacterController3DConfig playerCharacterControllerConfig;
        public FP playerSprintSpeedBoost = FP._1_50;
        public FP playerCharacterJumpForce = FP._2;
    }
}