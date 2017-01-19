using UnityEngine;
using System.Collections;

public class Game_Manager : MonoBehaviour {
    private DeckManager m_PlayerDeck;
    private DeckManager m_OpponentDeck;
    private HandManager m_PlayerHand;
    private HandManager m_OpponentHand;
    private PlayAreaManager m_PlayerPlayArea;
    private PlayAreaManager m_OpponentPlayArea;

    private int m_RoundNumber;
    private string m_GameState;
    private GameObject m_selectedCard;

    void Start()
    {
        m_PlayerDeck = GameObject.Find("Deck_Player").GetComponent<DeckManager>();
        m_OpponentDeck = GameObject.Find("Deck_Opponent").GetComponent<DeckManager>();
        m_PlayerHand = GameObject.Find("Hand_Player").GetComponent<HandManager>();
        m_OpponentHand = GameObject.Find("Hand_Opponent").GetComponent<HandManager>();
        m_PlayerPlayArea = GameObject.Find("PlayerPlayArea").GetComponent<PlayAreaManager>();
        m_OpponentPlayArea = GameObject.Find("OpponentPlayArea").GetComponent<PlayAreaManager>();
        m_RoundNumber = 0;
        m_GameState = "GameStarted";

        m_selectedCard = null;
    }

    void Update () {
        //Main Game Loop State Machine
        //Game Started

        if (m_GameState.Equals("GameStarted"))
        {
            for(int i = 0; i < 2; i++)
            {
                GameObject drawnCard = m_PlayerDeck.drawACard(); //remove card from in-memory list of cards in the deck
                m_PlayerHand.addCardToHand(drawnCard); //add card to in-memory lest of cards in the hand, also physically move the card to an available slot
                drawnCard.transform.parent = GameObject.Find("Hand_Player").transform; //move the game object in the heirachy to be a child of the hand
            }
            for (int i = 0; i < 2; i++)
            {
                GameObject drawnCard = m_OpponentDeck.drawACard(); //remove card from in-memory list of cards in the deck
                m_OpponentHand.addCardToHand(drawnCard); //add card to in-memory lest of cards in the hand, also physically move the card to an available slot
                drawnCard.transform.parent = GameObject.Find("Hand_Opponent").transform; //move the game object in the heirachy to be a child of the hand
            }

            m_GameState = "PlayersTurnStart";
        }
        else if (m_GameState.Equals("PlayersTurnStart"))           
        {
            m_RoundNumber++;
            GameObject drawnCard = m_PlayerDeck.drawACard();
            m_PlayerHand.addCardToHand(drawnCard);
            drawnCard.transform.parent = GameObject.Find("Hand_Player").transform;
            m_GameState = "PlayersTurnMain";
        }
        else if (m_GameState.Equals("PlayersTurnMain"))
        {


            //handle click events during players turn
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "Card")
                    {
                        GameObject clickedCard = hit.transform.gameObject;

                        //logic for cards being selected
                        if (clickedCard.transform.parent.tag.Equals("PlayerHand") || clickedCard.transform.parent.tag.Equals("PlayerPlayArea"))
                        {
                            if (m_selectedCard != null)
                            {
                                m_selectedCard.GetComponent<Card>().setUnselected();
                            }
                            m_selectedCard = hit.transform.gameObject;
                            m_selectedCard.GetComponent<Card>().setSelected();
                            Debug.Log("Card Clicked" + m_selectedCard);
                        }
                        //logic for opponents cards being selected
                        else if (clickedCard.transform.parent.tag.Equals("OpponentPlayArea"))
                        {
                            //players card on board attacks opponents card on board
                            if (m_selectedCard != null)
                            {
                                if (m_selectedCard.transform.parent.tag.Equals("PlayerPlayArea"))
                                {
                                    clickedCard.GetComponent<CardColleague>().takeDamage(1);                                
                                }
                            }
                        }
                    }
                    //logic for cards being played to the board from hand
                    else if(hit.collider.tag == "PlayerPlayArea")
                    {
                        Debug.Log("Player play area clicked");
                        if(m_selectedCard!= null)
                        {
                            if (!m_selectedCard.transform.parent.tag.Equals("PlayerPlayArea"))
                            {
                                m_PlayerPlayArea.addCardToPlayArea(m_selectedCard); //physically move card
                                m_PlayerHand.removeCardFromHand(m_selectedCard);
                                //move card in memory
                                Debug.Log("Adding " + m_selectedCard + "to player play area");
                                m_PlayerHand.reshuffleHand();
                                resetCardSelection();
                            }
                        }
                    }
                }
            }
                //end of state triggered by end turn button press
        }
        else if (m_GameState.Equals("PlayersTurnEnd"))
        {
            //ensure card selection is reset
            resetCardSelection();
            m_GameState = "OpponentsTurnStart";
        }
        else if (m_GameState.Equals("OpponentsTurnStart"))
        {
            GameObject drawnCard = m_OpponentDeck.drawACard();
            m_OpponentHand.addCardToHand(drawnCard);
            drawnCard.transform.parent = GameObject.Find("Hand_Opponent").transform;
            m_GameState = "OpponentsTurnMain";
        }
        else if (m_GameState.Equals("OpponentsTurnMain"))
        {
            //handle click events during players turn
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "Card")
                    {
                        GameObject clickedCard = hit.transform.gameObject;

                        //check whether card is under control of the player who's turn it is
                        if (clickedCard.transform.parent.tag.Equals("OpponentHand"))
                        {
                            if(m_selectedCard != null)
                            {
                                m_selectedCard.GetComponent<Card>().setUnselected();
                            }
                            m_selectedCard = hit.transform.gameObject;
                            m_selectedCard.GetComponent<Card>().setSelected();
                            Debug.Log("Card Clicked" + m_selectedCard);
                        }

                    }
                    else if (hit.collider.tag == "OpponentPlayArea")
                    {
                        Debug.Log("Opponent play area clicked");
                        if (m_selectedCard != null)
                        {
                            m_OpponentPlayArea.addCardToPlayArea(m_selectedCard); //physically move card
                            m_OpponentHand.removeCardFromHand(m_selectedCard); //move card in memory
                            Debug.Log("Adding " + m_selectedCard + "to player play area");
                            m_OpponentHand.reshuffleHand();
                            resetCardSelection();
                        }
                    }
                }
            }
        }
        else if (m_GameState.Equals("OpponentsTurnEnd"))
        {
            //ensure card selection is reset
            resetCardSelection();
            m_GameState = "PlayersTurnStart";
        }
    }

    void FixedUpdate()
    {

    }

    public string GetCurrentGameState()
    {
        return m_GameState;
    }

    public void SetCurrentGameState(string newGameState)
    {
        m_GameState = newGameState;
    }


    //called by the end turn button press
    public void EndTurn()
    {
        Debug.Log("End Turn Button Clicked");
        if (m_GameState.Equals("PlayersTurnMain"))
        {
            m_GameState = "PlayersTurnEnd";
        }
        else if (m_GameState.Equals("OpponentsTurnMain"))
        {
            m_GameState = "OpponentsTurnEnd";
        }
    }

    private void resetCardSelection()
    {
        if(m_selectedCard != null)
        {
        m_selectedCard.GetComponent<Card>().setUnselected();
        m_selectedCard = null;
        }
    }
}
