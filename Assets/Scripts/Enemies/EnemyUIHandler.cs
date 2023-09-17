using UnityEngine;
using UnityEngine.UI;

public class EnemyUIHandler : MonoBehaviour
{
    [SerializeField] protected Text HealthUI;

    public void UpdateHealth(int health)
    {
        HealthUI.text = health.ToString();
    }
}