using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

[System.Serializable]
public class SerializableList<T>
{
    public List<T> list;
}
public class SaveAndLoad : MonoBehaviour
{
    [SerializeField] private SerializableList<string> cardsNotFound;
    [SerializeField] Grid grid;
    [SerializeField] GameObject canvas;
    string saveFilePath;
    int gridDimensions;

    public void SaveCardsToList()
    {
        saveFilePath = Application.persistentDataPath + "/PlayerData.json";
        File.Delete(saveFilePath);
        for (int i = 0; i < Grid.cardsInLevel.Count; i++)
        {
            if (Grid.cardsInLevel[i].activeSelf == true)
            {
                cardsNotFound.list.Add(Grid.cardsInLevel[i].GetComponent<Card>().cardName);
            }
        }
        string json = JsonUtility.ToJson(cardsNotFound);
        File.WriteAllText(saveFilePath, json);
        Application.Quit();
    }

    public void LoadCards()
    {
        saveFilePath = Application.persistentDataPath + "/PlayerData.json";
        if (File.Exists(saveFilePath))
        {
            string loadData = File.ReadAllText(saveFilePath);
            cardsNotFound = JsonUtility.FromJson<SerializableList<string>>(loadData);
            File.Delete(saveFilePath);
            PlaceCards();
        }
        else
        {
            Debug.Log("No save file found");
        }
    }

    void PlaceCards()
    {
        gridDimensions = cardsNotFound.list.Count;
        while (gridDimensions%4 != 0)
        {
            gridDimensions++;
        }
        grid.CreateTileLayout(gridDimensions/4);
        grid.GenerateCardsFromLoadedData(cardsNotFound.list);
        grid.PlaceCardsOnScreen(cardsNotFound.list.Count);
        canvas.SetActive(false);
    }
}
