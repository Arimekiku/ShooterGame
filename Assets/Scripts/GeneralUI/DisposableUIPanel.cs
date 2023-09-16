using UnityEngine;

public class DisposableUIPanel : MonoBehaviour
{
    public void Init()
    {
        gameObject.SetActive(true);
    }
    
    public void Dispose()
    {
        gameObject.SetActive(false);
    }
}