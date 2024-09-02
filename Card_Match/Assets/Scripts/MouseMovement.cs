using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MouseMovement : MonoBehaviour
{
    Vector2 ray;
    RaycastHit2D hit;
    Card pressedCard;
    Card pressedCard2;
    bool firstCardPressed;
    int score;

    [SerializeField] Color highlightedColor;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] GameObject endGame;

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
                    firstCardPressed = false;
                    StartCoroutine(WaitUntilChecking());
                }
            }
        }

        if (score == (LevelSelector.gridY * 4) && score > 0)
        {
            endGame.SetActive(true);
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
            score += 2;
            scoreText.text = score.ToString();
        }
    }
}
