using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
        
        if (cardManager.IsHandEmpty())
        {
            EndTurn();
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
        enemyManager.DealDamageToPlayer();
        StartTurn();
    }

    public void StartTurn()
    {
        cardManager.DrawCards(5);
        cardManager.UpdateHandDisplay();
        //EventSystem.current.SetSelectedGameObject (null);
    }
    
    //damageovertime 
    
    //player 
    //game turn
    //card effects
    //room change/management
    //boss room
    //adding new cards to deck
    //visuals
}
