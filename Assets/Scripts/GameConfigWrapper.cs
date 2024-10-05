using Quantum;
using UnityEngine;

namespace Amax.PhotonQuantumDemo
{
    public class GameConfigWrapper: GameSingleton<GameConfigWrapper>
    {
        [SerializeField] private GameConfig gameConfig;
        public static GameConfig GameConfig => Instance.gameConfig;
    }
}