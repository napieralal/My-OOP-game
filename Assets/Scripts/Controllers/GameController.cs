using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    public CardManager cardManager;
    public List<CardSO> cardDataList;
    public List<Transform> spawnPoints;

    public EnemyController enemyManager;
    public List<EnemySO> enemyDataList;
    public List<Transform> enemySpawnPoints;

    public Transform playerSpawnPoint;
    
    protected void Awake()
    {
        base.Awake();
    }
    void Start()
    {
            // create room
            
            //something with deck
            
            //and with player hand
        
            InitializeDeck();
            DrawStartingHand();
            
            enemyManager.CreateEnemy(enemyDataList[0], enemySpawnPoints[0]);
            enemyManager.CreateEnemy(enemyDataList[0], enemySpawnPoints[1]);
            enemyManager.CreateEnemy(enemyDataList[0], enemySpawnPoints[2]);
    }

    void Update()
    {
        //Moze do player'a
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
        
            if (Physics.Raycast(ray, out hit))
            {
                IClickable clickable = hit.collider.GetComponent<IClickable>();
                if (clickable != null)
                {
                    clickable.OnClick();
                }
            }
        }
        
    }
    
    void InitializeDeck()
    {
        for (int i = 0; i < 10 && i < cardDataList.Count; i++)
        {
            cardManager.deck.Add(cardDataList[i]);
        }

        cardManager.ShuffleDeck();
    }

    void DrawStartingHand()
    {
        cardManager.DrawCards(5);
        cardManager.SpawnHand(spawnPoints);
    }

    public void EndTurn()
    {
        cardManager.DiscardHand();
        cardManager.DrawCards(5);
        cardManager.UpdateHandDisplay();
        //cardManager.DrawCards(5);
        //cardManager.UpdateHandDisplay();
        //cardManager.SpawnHand(spawnPoints);
        //cardManager.DrawCards(5);
    }
    
    //select enemy
    //player 
    //game turn
    //card effects
    //room change/management
    //boss room
    //adding new cards to deck
    //visuals
    
    // scene 0 - Menu
    // scene 1 - Base/City/Camp
    // scene 2 - dungeon(with many rooms)
    //scene manager (base and dungeons)
    
    // there is one big dungeon which contains of many rooms, ending with 
   //boss, reward, or some mystery or quest. 
    //room manager
    
    //enemy manager
    
    //card manager
    //card deck, discard pile, 5 cards in hand
    //cards from deck are going to their given positions animation + flip
    //moze cos ze mozesz odrzucic karte i cos dostac, albo dobrac dodatkowa karte i cos stracic
    //pytanie czy nie dodac jakiegos specjalnego czegos typu punkty mocy
    //moze jakies zywioly kart lub kategorie (typu walka lub od ras)
    //moze specjalne karty
    //moze synergie kart łączace sie efekty
    //potions, equipment, weapons/armor, something to improve your deck / cards / cards capabilities / synergies
    //lochy / jaskinie / zamczyska / więzienia
    //klikasz w drzwi, dostajesz prompt czy idziesz dalej, dajesz tak, otwieraja sie ekran zaczyna sie przygaszac,
    //postac idzie w kierunku drzwi robi sie czarny ekran, tepujesz gracza do nastepnego lochu ustawiasz wrogow, loot
    //pierwszy loch np. daje jakieś przedmioty na ten jeden run jakieś potki itp. po runie są usuwane, moze jakies buff
    // runy czy cos takiego, znajdujesz w lochach i cie wzmacniaja na bossa itp.
    //w miescie / hubie wybierasz gdzie chcesz isc do jakiego dungeona, wybierasz trudność potencjalnych wrogow
    //i nagrody, jakies np. slabosci wrogow albo ze atakuja ogniem, poziomy trudnosci lochow
    // endless mode z 1 zyciem grasz tak dlugo jak wytrzymasz, itemy zdobywasz podczas gry
    // resource management
    //lvl postaci i dostajesz karty do wyboru co lvl
    //upgrade cards (slash - bloody slash - bloody double slash)
    
    
    // Osobne funkcje np. generuj loch, generuj wroga/ow, najlepiej w innych plikach 
}
