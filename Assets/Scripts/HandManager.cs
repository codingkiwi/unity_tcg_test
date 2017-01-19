using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HandManager : MonoBehaviour {
    private List<GameObject> m_CardsInHand;
    private int m_CurrentCardCount = 0;
    private Vector3[] m_CardSlotPositions;
    private bool[] m_CardSlotsFilled;


    void Start () {
        m_CardsInHand = new List<GameObject>();

        //intialize card slots and their positions for the hand
        m_CardSlotPositions = new Vector3[5];
        m_CardSlotPositions[0] = new Vector3(transform.position.x - 100, transform.position.y, transform.position.z);
        m_CardSlotPositions[1] = new Vector3(transform.position.x - 50, transform.position.y, transform.position.z);
        m_CardSlotPositions[2] = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        m_CardSlotPositions[3] = new Vector3(transform.position.x + 50, transform.position.y, transform.position.z);
        m_CardSlotPositions[4] = new Vector3(transform.position.x + 100, transform.position.y, transform.position.z);

        m_CardSlotsFilled = new bool[5];
        for(int i = 0; i < 5; i++)
        {
            m_CardSlotsFilled[i] = false;
        }
        

}
	
	void Update () {
	
	}

    public void addCardToHand(GameObject cardToAdd)
    {
        m_CardsInHand.Add(cardToAdd);
        Debug.Log("Adding Card To " + this);

        //check for the first empty hand slot and add the new card in that position
        for(int i = 0; i < m_CardSlotsFilled.Length; i++)
        {
            if (!m_CardSlotsFilled[i])
            {
                cardToAdd.transform.position = m_CardSlotPositions[i];
                cardToAdd.transform.Rotate(new Vector3(0, 0, 180));
                m_CardSlotsFilled[i] = true;
                break;
            }
        }
        
        m_CurrentCardCount++;
    }

    public void reshuffleHand()
    {
        for (int i = 0; i < m_CardsInHand.Count; i++)
        {
            m_CardsInHand[i].transform.position = new Vector3(1000, 1000, 1000); //send cards offscreen first
        }
        for (int i = 0; i < 5; i++)
        {
            m_CardSlotsFilled[i] = false;
        }
        for (int i = 0; i < m_CardsInHand.Count; i++)
        { 
            for (int j = 0; j < m_CardSlotsFilled.Length; j++)
            {
                if (!m_CardSlotsFilled[j])
                {
                    m_CardsInHand[i].transform.position = m_CardSlotPositions[j];
                    m_CardSlotsFilled[j] = true;
                    break;
                }
            }
        }
    }

    public void removeCardFromHand(GameObject cardToRemove)
    {
        Debug.Log("removing card");
        m_CardsInHand.Remove(cardToRemove);
        m_CurrentCardCount--;
    }
}
