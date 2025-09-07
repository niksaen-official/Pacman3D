using UnityEngine;

public abstract class Window: MonoBehaviour
{
    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}
