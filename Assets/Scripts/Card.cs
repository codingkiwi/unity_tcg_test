using UnityEngine;
using System;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    //Base type for all cards
    //Manages the card data and common behaviour/functionality

    public string m_CardName;
    public int m_CardSeniority;
    public string[] m_CardKeywords;
    private Game_Manager m_GameManager;
    private GameObject m_GameManagerObject;
    private GameObject m_FrontPanel;
    private GameObject m_BackPanel;
    private GameObject m_CardCanvas;
    private Text m_CardTitle;
    private Text m_CardOffence;
    private Text m_CardDefence;
    public Material m_SelectedCardFrontMaterial;
    public Material m_NotSelectedCardFrontMaterial;
    public Material m_CardBackMaterial;

    private bool m_Selected;


    void Start()
    {
        //initializations
        m_GameManagerObject = GameObject.Find("GameManager");
        m_GameManager = m_GameManagerObject.GetComponent<Game_Manager>();
        m_FrontPanel = gameObject.transform.FindChild("CardFront").gameObject;
        m_BackPanel = gameObject.transform.FindChild("CardBack").gameObject;
        m_CardCanvas = gameObject.transform.FindChild("CardDataCanvas").gameObject;
        m_CardOffence = m_CardCanvas.transform.FindChild("CardOffence").gameObject.GetComponent<Text>();
        m_CardDefence = m_CardCanvas.transform.FindChild("CardDefence").gameObject.GetComponent<Text>();
        m_CardTitle = m_CardCanvas.transform.FindChild("CardTitle").gameObject.GetComponent<Text>();
        m_Selected = false;
    }

    void Update()
    {
        if (m_Selected)
        {
            m_FrontPanel.GetComponent<Renderer>().material = m_SelectedCardFrontMaterial;
        }
        else
        {
            m_FrontPanel.GetComponent<Renderer>().material = m_NotSelectedCardFrontMaterial;
        }
    }

    public void setSelected()
    {
        m_Selected = true;
    }

    public void setUnselected()
    {
        m_Selected = false;
    }
}

