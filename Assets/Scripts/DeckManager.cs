using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeckManager : MonoBehaviour {

	private List<GameObject> m_Cards;
    public GameObject m_DefaultCard;
    public int m_StartingCardCount;
    private int m_CurrentCardCount;

	void Start () {
        m_Cards = getPlayersCards();
        m_CurrentCardCount = m_StartingCardCount;
	}
	
	void Update () {
	
	}

    private List<GameObject> getPlayersCards()
    {
        List<GameObject> cards = new List<GameObject>();
        //placeholder function to fill deck with default cards
        float deckHeight = 10f;
        for(int i = 0; i < m_StartingCardCount; i++)
        {
            Vector3 newCardPosition = new Vector3(transform.position.x, deckHeight, transform.position.z); //find each cards new position based on the deck position and incremental height for stacking

            GameObject defaultCard = Instantiate(m_DefaultCard, newCardPosition, Quaternion.identity) as GameObject;
            defaultCard.transform.parent = gameObject.transform;
            defaultCard.transform.Rotate(new Vector3(0,0,180)); //isntantiate the card flipped
            cards.Add(defaultCard);
            deckHeight += 0.25f;
        }
        return cards;
    }

    public GameObject drawACard()
    {
        GameObject card = m_Cards[1];
        m_Cards.RemoveAt(1);
        m_CurrentCardCount--;
        return card;
    }
}
