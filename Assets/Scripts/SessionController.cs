using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SessionController : MonoBehaviour
{
    [Header("SessionUI")]
    [SerializeField] private SessionUI gameplayUI;

    [Header("Main")]
    [SerializeField] private Deck deck;
    [SerializeField] private Hand hand;
    
    [Header("Gameplay")]
    [SerializeField] private CardStatsFactory statsFactory;
    [SerializeField] private int cardsCount = 6;

    private WaitForSeconds waitBetweenCardHealthChanges;

    //TODO Add state machine for more control
    //(dealing cards, waiting for input, dealing damage, summing up results, etc.)
    private void Awake()
    {
        waitBetweenCardHealthChanges = new WaitForSeconds(1f);
    }

    private async void Start()
    {
        hand.Initialize(cardsCount, Camera.main);

        var cards = new List<CardController>(await deck.GetRandomCards(cardsCount));
        hand.AddCards(cards);
    }

    private void OnEnable()
    {
        gameplayUI.OnTakeDamageButtonClicked += ChangeHandCardsHealth;
        gameplayUI.OnRestartButtonClicked += Restart;
    }

    private void OnDisable()
    {
        gameplayUI.OnTakeDamageButtonClicked -= ChangeHandCardsHealth;
        gameplayUI.OnRestartButtonClicked -= Restart;
    }

    private void ChangeHandCardsHealth()
    {
        gameplayUI.SetActiveTakeDamageButton(false);

        StartCoroutine(ChangeHandCardsHealthAnimation());
    }

    private IEnumerator ChangeHandCardsHealthAnimation()
    {
        var cards = new List<CardController>(hand.Cards);
        for (int i = 0; i < cards.Count; i++)
        {
            var cardStats = statsFactory.GetRandom();
            cards[i].ChangeHealth(cardStats.Health);

            yield return waitBetweenCardHealthChanges;
        }

        gameplayUI.SetActiveTakeDamageButton(true);
    }

    private void Restart()
    {
        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
