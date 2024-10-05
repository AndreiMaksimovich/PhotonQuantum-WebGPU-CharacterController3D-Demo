using Photon.Deterministic;
using Quantum;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using Input = Quantum.Input;

namespace Amax.PhotonQuantumDemo
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerInputController : GameSingleton<PlayerInputController>, IEventBusListener<OnLocalPlayerCharacterAdded>
    {
        public Vector2 Move { get; private set; }
        public bool ButtonAttack { get; private set; }
        public bool ButtonSprint { get; private set; }
        public bool ButtonJump { get; private set; }
        public bool ButtonAction { get; private set; }

        private GameObject _playerGameObject;

        private PlayerInput _playerInput;
        private PlayerInput PlayerInput
        {
            get
            {
                _playerInput ??= GetComponent<PlayerInput>();
                return _playerInput;
            }
        }

        public GameObject PlayerGameObject
        {
            get => _playerGameObject;
            set => _playerGameObject = value;
        }

        private void Start()
        {
            QuantumCallback.Subscribe(this, (CallbackPollInput callback) => PollInput(callback));
            EventBus.AddListener(this);
        }

        private void OnDestroy()
        {
            EventBus.RemoveListener(this);
        }

        private void PollInput(CallbackPollInput callback)
        {
            if (!PlayerGameObject) return;

            var input = new Quantum.Input()
            {
                Direction = new FPVector2(FP.FromFloat_UNSAFE(Move.x), FP.FromFloat_UNSAFE(Move.y)),
                Action = ButtonAction,
                Attack = ButtonAttack,
                Jump = ButtonJump,
                Sprint = ButtonSprint,
            };
            
            callback.SetInput(input, DeterministicInputFlags.Repeatable);
        }
        
        public void OnMove(InputAction.CallbackContext context)
        {
            Move = context.ReadValue<Vector2>();
        }

        public void OnButtonSprint(InputAction.CallbackContext context)
        {
            ButtonSprint = context.ReadValue<float>() > 0;
        }

        public void OnButtonAttack(InputAction.CallbackContext context)
        {
            ButtonAttack = context.ReadValue<float>() > 0;
        }
        
        public void OnButtonAction(InputAction.CallbackContext context)
        {
            ButtonAction = context.ReadValue<float>() > 0;
        }
        
        public void OnButtonJump(InputAction.CallbackContext context)
        {
            ButtonJump = context.ReadValue<float>() > 0;
        }
        
        public void OnEvent(OnLocalPlayerCharacterAdded data)
        {
            PlayerGameObject = data.GameObject;
        }
    }

}
