namespace Quantum.Amax.PhotonQuantumDemo
{
    using UnityEngine.Scripting;

    [Preserve]
    public unsafe class PlayerCharacterSpawnSystem : SystemSignalsOnly,
        ISignalOnPlayerAdded, 
        ISignalOnPlayerRemoved
    {
        public void OnPlayerAdded(Frame f, PlayerRef player, bool firstTime)
        {
            var playerPrototype = f.GameConfig.playerPrototype;
            var playerEntity = f.Create(playerPrototype);
            f.Set(playerEntity, new PlayerCharacter() {PlayerRef = player});
            
            var characterController = f.Get<CharacterController3D>(playerEntity);
            characterController.SetConfig(f, f.GameConfig.playerCharacterControllerConfig);
            f.Set(playerEntity, characterController);
        }

        public void OnPlayerRemoved(Frame f, PlayerRef player)
        {
            var filter = f.Filter<PlayerCharacter>();
            while (filter.Next(out var entityRef, out var playerEntityCharacter))
            {
                if (playerEntityCharacter.PlayerRef == player)
                {
                    f.Destroy(entityRef);
                }
            }
        }
    }
}
