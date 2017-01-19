using UnityEngine;
using System.Collections;


public class CardDragAndDrop : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;

    private Camera m_mainCamera;

    void Start()
    {
        m_mainCamera = Camera.main;
    }

    void OnMouseDown()
    {
        Debug.Log("GameObject name: " + gameObject.name);
        screenPoint = m_mainCamera.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - m_mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 cursorPosition = m_mainCamera.ScreenToWorldPoint(cursorPoint) + offset;
        transform.position = cursorPosition;
        Debug.Log(transform.position);
    }
}