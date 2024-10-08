using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Card : MonoBehaviour, IInteractableCards
{
    //public bool hasBeenFound;
    public Sprite cardBack;
    public Sprite cardImage;
    public string cardName;

    bool allowCoroutine;
    bool faceUp;

    [HideInInspector] public SpriteRenderer render;

    private void Start()
    {
        render = GetComponent<SpriteRenderer>();
        render.sprite = cardBack;
        allowCoroutine = true;
        faceUp = false;
    }
    IEnumerator FlipCard()
    {
        allowCoroutine = false;
        for (float i = 0; i <= 180f; i+= 10f)
        {
            transform.rotation = Quaternion.Euler(0f, i, 0f);
            if (i == 90f)
            {
                render.sprite = faceUp ? cardBack : cardImage;
                yield return new WaitForSeconds(0.01f);
            }
        }
        allowCoroutine = true;
        faceUp = !faceUp;
    }

    public void Interact()
    {
        if (allowCoroutine)
        {
            StartCoroutine(FlipCard());
        }
    }
}
