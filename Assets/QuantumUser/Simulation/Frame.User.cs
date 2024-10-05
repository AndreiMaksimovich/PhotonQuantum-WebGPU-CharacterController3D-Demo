namespace Quantum
{
    public unsafe partial class Frame
    {
#if UNITY_ENGINE

#endif

        private GameConfig _gameConfig;
        public GameConfig GameConfig
        {
            get
            {
                _gameConfig ??= FindAsset<GameConfig>(RuntimeConfig.gameConfig);
                return _gameConfig;
            }
        }
        
        public CharacterController3DConfig PlayerCharacterController3DConfig => GameConfig.playerCharacterControllerConfig;
    }
}