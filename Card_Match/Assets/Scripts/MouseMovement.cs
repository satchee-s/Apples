using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    Vector2 ray;
    RaycastHit2D hit;
    Card pressedCard;
    Card pressedCard2;
    bool firstCardPressed;

    [SerializeField] Color highlightedColor;

    private void Update()
    {
        ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            hit = Physics2D.Raycast(ray, Vector2.zero);
            if (hit.collider.GetComponent<Card>() != null)
            {
                if (!firstCardPressed)
                {
                    pressedCard = hit.collider.GetComponent<Card>();
                    pressedCard.render.color = highlightedColor;
                    firstCardPressed = true;
                }
                else
                {
                    pressedCard2 = hit.collider.GetComponent<Card>();
                    pressedCard.Interact();
                    pressedCard2.Interact();
                    pressedCard.render.color = Color.white;
                    StartCoroutine(WaitUntilChecking());
                    firstCardPressed = false;
                }
            }
        }
    }

    IEnumerator WaitUntilChecking()
    {
        yield return new WaitForSeconds(1);
        if (pressedCard.cardName != pressedCard2.cardName)
        {
            pressedCard.Interact();
            pressedCard2.Interact();
            yield break;
        }
        else if (pressedCard.cardName == pressedCard2.cardName)
        {
            pressedCard.gameObject.SetActive(false);
            pressedCard2.gameObject.SetActive(false);
            pressedCard = null;
            pressedCard2 = null;            
        }
    }
}
