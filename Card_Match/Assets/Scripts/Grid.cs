using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;

    GameObject[] tilesInLevel;
    List<Sprite> cardFrontImages = new List<Sprite>();
    List<Card> cardsInLevel = new List<Card>();

    private void Start()
    {
        object[] loadedImages = Resources.LoadAll("TileImages", typeof(Sprite));
        for (int i = 0; i < loadedImages.Length; i++)
            cardFrontImages.Add((Sprite)loadedImages[i]);
    }

    public void CreateTileLayout(int gridSize) //position on which cards will be placed
    {
        tilesInLevel = new GameObject[gridSize * 4];
        for (int z = 0; z < gridSize; z++)
        {
            for (int x = 0; x < 4; x++)
            {
                int i = x + (z * (gridSize - 1));
                Vector3 position = transform.position + new Vector3(x * 1.5f, z * 1.5f, 0f);
                tilesInLevel[i] = Instantiate(tilePrefab, position, Quaternion.identity);
            }
        }
        //GenerateCards(tilesInLevel.Length);
    }

    void GenerateCards(int cardsNeeded)
    {
        for (int j = 0; j < cardFrontImages.Count; j++)
        {
            int k = Random.Range(j, cardFrontImages.Count);
            Sprite temp = cardFrontImages[j];
            cardFrontImages[j] = cardFrontImages[k];
            cardFrontImages[k] = temp;
        }

        for (int a = 0; a < cardsNeeded / 2; a++)
        {
            Card newCard = new Card();
            newCard.cardImage = cardFrontImages[a];
            newCard.cardName = cardFrontImages[a].name;
            cardsInLevel.Add(newCard);
        }

        cardsInLevel.AddRange(cardsInLevel);

        for (int b = 0; b < cardsNeeded; b++)
        {
            int k = Random.Range(b, cardsInLevel.Count);
            Card temp = cardsInLevel[b];
            cardsInLevel[b] = cardsInLevel[k];
            cardsInLevel[k] = temp;
        }
        PlaceCardsOnScreen(cardsNeeded);
    }

    void PlaceCardsOnScreen(int numberOfCards)
    {
        for (int i = 0; i < numberOfCards; i++) 
        {
            Vector3 pos = tilesInLevel[i].transform.position;
            Instantiate(cardsInLevel[i], pos, Quaternion.identity);
        }
    }
}