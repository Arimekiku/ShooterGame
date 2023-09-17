using UnityEngine;
using UnityEngine.Serialization;

public class BossRoadBehaviour : RoadBehaviour
{
    [SerializeField] private BossBehaviour BossBehaviour;

    public BossBehaviour Boss => BossBehaviour;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerBehaviour player))
        {
            BossBehaviour.EnableBoss();
            player.DisableForwardMovement();
        }
    }
}