using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;
    [SerializeField] GameObject cardBase;

    List<GameObject> tilesInLevel = new List<GameObject>();
    List<GameObject> cardsInLevel = new List<GameObject>();
    List<Sprite> cardFrontImages = new List<Sprite>();

    private void Start()
    {
        object[] loadedImages = Resources.LoadAll("TileImages", typeof(Sprite));
        for (int i = 0; i < loadedImages.Length; i++)
            cardFrontImages.Add((Sprite)loadedImages[i]);
        ShuffleCardOrder(tilesInLevel.Count);
    }

    public void CreateTileLayout(int gridSize) 
    {
        //tilesInLevel = new GameObject[gridSize * 4];
        for (int z = 0; z < gridSize; z++)
        {
            for (int x = 0; x < 4; x++)
            {
                int i = x + (z * (gridSize - 1));
                Vector3 position = transform.position + new Vector3(x * 1.5f, z * 1.5f, 0f);
                tilesInLevel.Add( Instantiate(tilePrefab, position, Quaternion.identity));
            }
        }
        AssignCardValues(tilesInLevel.Count);
    }

    void ShuffleCardOrder(int cardsNeeded)
    {
        for (int j = 0; j < cardFrontImages.Count; j++)
        {
            int k = Random.Range(j, cardFrontImages.Count);
            Sprite temp = cardFrontImages[j];
            cardFrontImages[j] = cardFrontImages[k];
            cardFrontImages[k] = temp;
        }
    }

    void AssignCardValues(int cardsNeeded)
    {
        for (int a = 0; a < cardsNeeded/2; a++)
        {
            cardsInLevel.Add(Instantiate(cardBase, transform.position, Quaternion.identity));
            cardsInLevel[a].GetComponent<Card>().cardImage = cardFrontImages[a];
            cardsInLevel[a].GetComponent<Card>().cardName = cardFrontImages[a].name;
        }

        for (int a = 0; a < cardsNeeded/2; a++)
        {
            cardsInLevel.Add(Instantiate(cardsInLevel[a]));
        }

        for (int c = 0; c < cardsInLevel.Count; c++)
        {
            int k = Random.Range(c, cardsInLevel.Count);
            GameObject temp = cardsInLevel[c];
            cardsInLevel[c] = cardsInLevel[k];
            cardsInLevel[k] = temp;
        }
        PlaceCardsOnScreen(cardsNeeded);
    }

    void PlaceCardsOnScreen(int numberOfCards)
    {
        for (int i = 0; i < numberOfCards; i++) 
        {
            Vector3 pos = tilesInLevel[i].transform.position;
            cardsInLevel[i].transform.position = pos;
        }
    }
}