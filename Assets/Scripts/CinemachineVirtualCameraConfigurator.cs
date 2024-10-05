using Unity.Cinemachine;
using UnityEngine;

namespace Amax.PhotonQuantumDemo
{

    [RequireComponent(typeof(CinemachineCamera))]
    public class CinemachineVirtualCameraConfigurator : MonoBehaviour, IEventBusListener<OnLocalPlayerCharacterAdded>
    {
        private void Start()
        {
            EventBus.AddListener(this);
        }

        private void OnDestroy()
        {
            EventBus.RemoveListener(this);
        }

        public void OnEvent(OnLocalPlayerCharacterAdded data)
        {
            GetComponent<CinemachineCamera>().Follow =
                data.GameObject.GetComponent<PlayerCharacterView>().lookAtTarget;
        }
    }

}
