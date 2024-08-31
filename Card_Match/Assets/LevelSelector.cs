using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    [HideInInspector] public int gridY;
    public GameObject startScreenCanvas;
    [SerializeField] Grid grid;

    public void Easy() { gridY = 3; }

    public void Medium() { gridY = 4; }

    public void Hard() { gridY = 5; }

    public void DisableCanvas()
    {
        grid.CreateTileLayout(gridY);
        startScreenCanvas.SetActive(false);
    }

}
