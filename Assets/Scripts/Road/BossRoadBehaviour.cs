using UnityEngine;
using UnityEngine.Serialization;

public class BossRoadBehaviour : RoadBehaviour
{
    [SerializeField] private BossBehaviour BossBehaviour;

    public BossBehaviour Boss => BossBehaviour;

    public override void Init()
    {
        base.Init();
        
        BossBehaviour.Init();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerBehaviour _))
            BossBehaviour.EnableBoss();
    }
}