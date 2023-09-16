using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class EnemyUIHandler : MonoBehaviour
{
    [FormerlySerializedAs("_healthUI")] [SerializeField] protected Text HealthUI;

    public void UpdateHealth(int health)
    {
        HealthUI.text = health.ToString();
    }
}