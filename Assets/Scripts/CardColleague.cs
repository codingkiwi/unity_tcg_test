using UnityEngine;
using System.Collections;

public class CardColleague : Card {
    //Colleague Card Type
    //Manages the card data and behaviour specific to colleague cards

    public int m_CardBaseOffence;
    public int m_CardBaseDefence;
    public int m_CardBaseInfluence;
    [HideInInspector]public int m_CardCurrentOffence;
    [HideInInspector]public int m_CardCurrentDefence;
    [HideInInspector]public int m_CardCurrentInfluence;
    private string m_CardType = "colleague";

    public void takeDamage(int damage)
    {
        Debug.Log("Card Interaction Ocurred");
    }

    public int getOffence()
    {
        return m_CardBaseOffence;
    }

}
