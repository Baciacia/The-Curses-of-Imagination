using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public GameObject mainMenuCanvas;

    public void DeactivateCanvas()
    {
        mainMenuCanvas.SetActive(false);
    }

}
