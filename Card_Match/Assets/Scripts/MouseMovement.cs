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
    RuntimePlatform platform;

    [SerializeField] TMP_Text scoreText;
    [SerializeField] GameObject endGame;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip cardFlip;
    [SerializeField] AudioClip rightMatch;
    [SerializeField] AudioClip wrongMatch;

    private void Start()
    {
        platform = Application.platform;
    }

    private void Update()
    {
        if (platform == RuntimePlatform.Android)
        {
            if (Input.touchCount > 0 && Input.touchCount < 2)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    checkTouch(Input.GetTouch(0).position);
                }
            }
        }
        else if (platform == RuntimePlatform.WindowsEditor || platform == RuntimePlatform.WindowsPlayer)
        {
            if (Input.GetMouseButtonDown(0))
            {
                checkTouch(Input.mousePosition);
            }
        }

        if (score == (Grid.cardsInLevel.Count) && score > 0)
        {
            endGame.SetActive(true);
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

    private void checkTouch(Vector3 pos)
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
