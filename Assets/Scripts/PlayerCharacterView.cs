using System.Collections;
using Photon.Deterministic;
using Quantum;
using UnityEngine;

namespace Amax.PhotonQuantumDemo
{
    public class PlayerCharacterView : QuantumEntityViewComponent
    {
        public Transform lookAtTarget;
        
        [Header("Animation")]
        [SerializeField] private Animator characterAnimator;
        [SerializeField] private float characterAnimationMotionSpeed = 0.75f;
        [SerializeField] private float characterSpeedScale = 0.15f;
        [SerializeField] private Transform modelRoot;

        private const string AnimatorParameterNameSpeed = "Speed";
        private const string AnimatorParameterNameJump = "Jump";
        private const string AnimatorParameterNameGrounded = "Grounded";
        private const string AnimatorParameterNameMotionSpeed = "MotionSpeed";
        
        private static readonly int AnimatorParameterSpeed = Animator.StringToHash(AnimatorParameterNameSpeed);
        private static readonly int AnimatorParameterJump = Animator.StringToHash(AnimatorParameterNameJump);
        private static readonly int AnimatorParameterGrounded = Animator.StringToHash(AnimatorParameterNameGrounded);
        private static readonly int AnimatorParameterMotionSpeed = Animator.StringToHash(AnimatorParameterNameMotionSpeed);
        
        private PlayerCharacter GetPlayerCharacter(Frame f) => f.Get<PlayerCharacter>(EntityRef);
        private CharacterController3D GetCharacterController(Frame f) => f.Get<CharacterController3D>(EntityRef);
        
        private PlayerRef PlayerRef { get; set; }
        
        public override void OnActivate(Frame frame)
        {
            var playerCharacter = GetPlayerCharacter(frame);
            PlayerRef = playerCharacter.PlayerRef;
            if (frame.IsPlayerVerifiedOrLocal(PlayerRef))
            {
                OnLocalPlayerCharacterAdded.Raise(PlayerRef, EntityRef, gameObject);
            }
            OnPlayerCharacterAdded.Raise(PlayerRef, EntityRef, gameObject);

            ConfigureCharacterAnimator();
        }

        private void OnDestroy()
        {
            if (VerifiedFrame.IsPlayerVerifiedOrLocal(PlayerRef))
            {
                OnLocalPlayerCharacterRemoved.Raise(PlayerRef, EntityRef, gameObject);
            }
            OnPlayerCharacterRemoved.Raise(PlayerRef, EntityRef, gameObject);
        }

        private void ConfigureCharacterAnimator()
        {
            characterAnimator.fireEvents = false;
            characterAnimator.SetFloat(AnimatorParameterMotionSpeed, characterAnimationMotionSpeed);
        }

        public override void OnUpdateView()
        {
            var controller = GetCharacterController(PredictedFrame);
            var playerCharacter = GetPlayerCharacter(PredictedFrame);
            
            var speed = (new Vector2(controller.Velocity.X.AsFloat, controller.Velocity.Z.AsFloat)).sqrMagnitude * characterSpeedScale;
            
            characterAnimator.SetFloat(AnimatorParameterSpeed, speed);
            if (controller.Jumped && !_isJumping)
            {
                StartCoroutine(JumpCoroutine());
            }
            characterAnimator.SetBool(AnimatorParameterGrounded, controller.Grounded);

            if (playerCharacter.Direction != FPVector2.Zero)
            {
                modelRoot.LookAt(modelRoot.position + new Vector3(playerCharacter.Direction.X.AsFloat, 0, playerCharacter.Direction.Y.AsFloat), Vector3.up);
            }
        }
        
        private bool _isJumping = false;
        private IEnumerator JumpCoroutine()
        {
            _isJumping = true;
            characterAnimator.SetBool(AnimatorParameterJump, true);
            yield return new WaitForSeconds(0.25f);
            characterAnimator.SetBool(AnimatorParameterJump, false);
            _isJumping = false;
        }
    }
}
