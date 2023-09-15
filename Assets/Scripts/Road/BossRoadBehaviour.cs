using UnityEngine;

public class BossRoadBehaviour : RoadBehaviour
{
    [SerializeField] private BossBehaviour _boss;

    public BossBehaviour Boss => _boss;

    public override void Init()
    {
        base.Init();
        
        _boss.Init();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerBehaviour _))
            _boss.EnableBoss();
    }
}