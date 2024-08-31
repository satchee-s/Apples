using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    string currentCardName;
    Card currentCard;
    private void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                currentCard = hit.transform.GetComponent<Card>();
                if (currentCard != null)
                {
                    if (currentCardName == null)
                    {
                        currentCardName = currentCard.cardName;
                    }
                    else
                    {
                        if (currentCard.name == currentCardName)
                        {
                            Debug.Log("Pair found");
                        }
                        else
                        {
                            Debug.Log("Incorrect pair");
                        }
                    }
                    currentCard.Interact();
                }
            }
        }
    }
}
