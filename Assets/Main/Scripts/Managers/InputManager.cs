using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class InputManager : MonoBehaviour
{
    public static UnityEvent<Card> OnCardSelected = new UnityEvent<Card>();
    public static UnityEvent<Card> OnCardDeselected = new UnityEvent<Card>();
    public static UnityEvent<Card> OnCardMouseOver = new UnityEvent<Card>();
    public static UnityEvent<Card> OnCardMouseExit = new UnityEvent<Card>();

    public static Card selectedCard;
    public static Vector3 mousePosition;

    private void OnEnable()
    {
        OnCardSelected.AddListener(SetSelectedCard);
        OnCardDeselected.AddListener(SetSelectedCard);
    }

    private void SetSelectedCard(Card selectedCard)
    {
        InputManager.selectedCard = selectedCard;
    }

    private void Update()
    {
        mousePosition = GetMousePosition();

        SelectedCardFollowPointer();
        ExecuteTrajectory();
    }

    private Vector3 GetMousePosition()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = -1f;
        return mousePos;
    }

    private void SelectedCardFollowPointer()
    {
        if (selectedCard == null) { return; }
        if (selectedCard.state == CardState.Used) { return; }

        selectedCard.transform.position = mousePosition;
        selectedCard.CheckIsUsable();
    }
    private void ExecuteTrajectory()
    {
        if (selectedCard == null)
        {
            Trajectory.Instance.Calculate(Vector3.zero, Vector3.zero);
            return; 
        }
        if (selectedCard.state != CardState.Used) { return; }

        if (selectedCard.side == CardSide.Player)
        {
            Trajectory.Instance.Calculate(selectedCard.transform.position, mousePosition);
        }
        else
        {
            Trajectory.Instance.Calculate(Vector2.zero, Vector2.zero); 
        }
    }
}