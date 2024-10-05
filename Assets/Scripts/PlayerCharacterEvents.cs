using Quantum;
using UnityEngine;

namespace Amax.PhotonQuantumDemo
{
    public abstract class PlayerCharacterBaseEvent: EventBusEventBase {
        public readonly PlayerRef Player;
        public readonly EntityRef PlayerCharacter;
        public readonly GameObject GameObject;

        protected PlayerCharacterBaseEvent(PlayerRef player, EntityRef playerCharacter, GameObject gameObject)
        {
            Player = player;
            PlayerCharacter = playerCharacter;
            GameObject = gameObject;
        }
    }
    
    public class OnLocalPlayerCharacterAdded : PlayerCharacterBaseEvent
    {
        private OnLocalPlayerCharacterAdded(PlayerRef player, EntityRef playerCharacter, GameObject gameObject): base(player, playerCharacter, gameObject) { }
        public static void Raise(PlayerRef player, EntityRef playerCharacter, GameObject gameObject)
        {
            EventBus.Raise(new OnLocalPlayerCharacterAdded(player, playerCharacter, gameObject));
        }
    }
    
    public class OnLocalPlayerCharacterRemoved : PlayerCharacterBaseEvent
    {
        private OnLocalPlayerCharacterRemoved(PlayerRef player, EntityRef playerCharacter, GameObject gameObject): base(player, playerCharacter, gameObject) { }
        public static void Raise(PlayerRef player, EntityRef playerCharacter, GameObject gameObject)
        {
            EventBus.Raise(new OnLocalPlayerCharacterRemoved(player, playerCharacter, gameObject));
        }
    }
    
    public class OnPlayerCharacterAdded : PlayerCharacterBaseEvent
    {
        private OnPlayerCharacterAdded(PlayerRef player, EntityRef playerCharacter, GameObject gameObject): base(player, playerCharacter, gameObject) { }
        public static void Raise(PlayerRef player, EntityRef playerCharacter, GameObject gameObject)
        {
            EventBus.Raise(new OnPlayerCharacterAdded(player, playerCharacter, gameObject));
        }
    }

    public class OnPlayerCharacterRemoved : PlayerCharacterBaseEvent
    {
        private OnPlayerCharacterRemoved(PlayerRef player, EntityRef playerCharacter, GameObject gameObject): base(player, playerCharacter, gameObject) { }
        public static void Raise(PlayerRef player, EntityRef playerCharacter, GameObject gameObject)
        {
            EventBus.Raise(new OnPlayerCharacterRemoved(player, playerCharacter, gameObject));
        }
    }
}