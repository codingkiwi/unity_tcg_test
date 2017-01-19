using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayAreaManager : MonoBehaviour {
    private List<GameObject> m_Cards;
    private int m_CurrentCardCount = 0;
    private Vector3[] m_CardSlotPositions;
    private bool[] m_CardSlotsFilled;

    void Start () {
        m_Cards = new List<GameObject>();

        //intialize card slots and their positions for the player area
        m_CardSlotPositions = new Vector3[5];
        m_CardSlotPositions[0] = new Vector3(transform.position.x - 100, transform.position.y + 1, transform.position.z);
        m_CardSlotPositions[1] = new Vector3(transform.position.x - 50, transform.position.y + 1, transform.position.z);
        m_CardSlotPositions[2] = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        m_CardSlotPositions[3] = new Vector3(transform.position.x + 50, transform.position.y + 1, transform.position.z);
        m_CardSlotPositions[4] = new Vector3(transform.position.x + 100, transform.position.y + 1, transform.position.z);

        m_CardSlotsFilled = new bool[5];
        for (int i = 0; i < 5; i++)
        {
            m_CardSlotsFilled[i] = false;
        }
    }
	
	void Update () {
	
	}

    public void addCardToPlayArea(GameObject cardToAdd)
    {
        m_Cards.Add(cardToAdd);

        //check for the first empty hand slot and add the new card in that position
        for (int i = 0; i < m_CardSlotsFilled.Length; i++)
        {
            if (!m_CardSlotsFilled[i])
            {
                cardToAdd.transform.position = m_CardSlotPositions[i];
                cardToAdd.transform.parent = transform;
                m_CardSlotsFilled[i] = true;
                break;
            }
        }

        m_CurrentCardCount++;
    }
}
