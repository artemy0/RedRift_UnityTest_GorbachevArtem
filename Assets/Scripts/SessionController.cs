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
    [SerializeField] private Drop drop;
    
    [Header("Gameplay")]
    [SerializeField] private CardStatsFactory statsFactory;
    [SerializeField] private int cardsCount = 6;

    private WaitForSeconds waitBetweenCardHealthChanges;
    private WaitForSeconds waitAfterCardHealthChanges;

    //TODO Add state machine for more control
    //(dealing cards, waiting for input, dealing damage, summing up results, etc.)
    private void Awake()
    {
        waitBetweenCardHealthChanges = new WaitForSeconds(1f);
        waitAfterCardHealthChanges = new WaitForSeconds(0.5f);
    }

    private async void Start()
    {
        hand.Initialize(cardsCount, Camera.main);
        drop.Initialize();

        var cards = new List<CardController>(await deck.GetRandomCards(cardsCount));
        AddCardsToHand(cards);
    }

    private void AddCardsToHand(List<CardController> cards)
    {
        hand.AddCards(cards);

        //TODO IMPORTENT as an alternative, you can:
        //subscribe to this event every time a card is added to the hand
        //and unsubscribe every time it is removed from the hand
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].OnDroped += drop.AddCard;
        }
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
        cards.AddRange(drop.Cards);

        for (int i = 0; i < cards.Count; i++)
        {
            var cardStats = statsFactory.GetRandom();
            cards[i].ChangeHealth(cardStats.Health);

            yield return waitBetweenCardHealthChanges;
        }

        yield return waitAfterCardHealthChanges;

        gameplayUI.SetActiveTakeDamageButton(true);
    }

    private void Restart()
    {
        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
