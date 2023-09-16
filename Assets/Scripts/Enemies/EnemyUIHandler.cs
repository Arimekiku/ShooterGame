using UnityEngine;
using UnityEngine.UI;

public class EnemyUIHandler : MonoBehaviour
{
    [SerializeField] protected Text _healthUI;

    public void UpdateHealth(int health)
    {
        _healthUI.text = health.ToString();
    }
}