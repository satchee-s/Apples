using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    public int gridY;
    public GameObject startScreenCanvas;
    [SerializeField] Grid grid;

    public void Easy() { gridY = 4; }

    public void Medium() { gridY = 5; }
    

    public void High() { gridY = 6; }

    public void DisableCanvas()
    {
        grid.CreateTileLayout(gridY);
        startScreenCanvas.SetActive(false);
    }

}
