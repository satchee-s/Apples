using System.Collections;
using TMPro;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    Vector2 ray;
    RaycastHit2D hit;
    Card pressedCard;
    Card pressedCard2;
    bool firstCardPressed;
    int score;

    [SerializeField] TMP_Text scoreText;
    [SerializeField] GameObject endGame;
    [SerializeField] GameObject scoreScreen;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip cardFlip;
    [SerializeField] AudioClip rightMatch;
    [SerializeField] AudioClip wrongMatch;


    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            CheckTouch(Input.mousePosition);
        }


        if (score == (Grid.cardsInLevel.Count) && score > 0)
        {
            endGame.SetActive(true);
            scoreScreen.SetActive(false);
        }
    }

    IEnumerator WaitUntilChecking()
    {
        yield return new WaitForSeconds(0.2f);
        if (pressedCard.cardName != pressedCard2.cardName)
        {
            pressedCard.Interact();
            pressedCard2.Interact();
            PlayAudio(wrongMatch);
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
            PlayAudio(rightMatch);
        }
    }

    void PlayAudio(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    private void CheckTouch(Vector3 pos)
    {
        ray = Camera.main.ScreenToWorldPoint(pos);
        hit = Physics2D.Raycast(ray, Vector2.zero);
        if (hit.collider.GetComponent<Card>() != null)
        {
            CheckIfCardsPressed();

        }
    }

    void CheckIfCardsPressed()
    {
        if (!firstCardPressed)
        {
            pressedCard = hit.collider.GetComponent<Card>();
            pressedCard.Interact();
            firstCardPressed = true;
            PlayAudio(cardFlip);
        }
        else
        {
            pressedCard2 = hit.collider.GetComponent<Card>();
            pressedCard2.Interact();
            firstCardPressed = false;
            StartCoroutine(WaitUntilChecking());
            PlayAudio(cardFlip);
        }
    }
}
