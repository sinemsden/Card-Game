using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using DG.Tweening;
using Photon.Pun;
using Sirenix.OdinInspector;
public class GameManager : Singleton<GameManager>
{
    public static UnityEvent OnGameStarted = new UnityEvent();
    public static UnityEvent OnGameEnded = new UnityEvent();
    public static UnityEvent OnTurnStarted = new UnityEvent();
    public static UnityEvent OnTurnEnded = new UnityEvent();
    public static CardSide turnOwner;

    public PlayerInstance player;
    public PlayerInstance opponent;
    
    [Header("Lights")]
    public Light playerLight;
    public Light opponentLight;

    [Header("Dealer")]
    public int dealCardCountFirstTime = 3;
    public float dealTimer = 0;

    [Header("Network")]
    private PhotonView view;
    public static int LocalCount = 100;
    public static int RemoteCount = 100;
    public static bool isLocal;

    private void Start()
    {
        view = Photon.Pun.PhotonView.Get(this);
        DeckManager.Instance.GetCards();
    }
    private void Update()
    {
        if (dealCardCountFirstTime > 0)
        {
            if (dealTimer < 1.0f)
            {
                dealTimer += Time.deltaTime * 10;
            }
            else
            {
                DealCard(CardSide.Player);
                DealCard(CardSide.Opponent);
                dealCardCountFirstTime--;   
                dealTimer = 0;
            }
        }
    }

    public void DealCard(CardSide side)
    {
        if (side == CardSide.Player)
        {
            HandManager.Instance.AddCardToHand(CardSide.Player, DeckManager.Instance.playerCards[0]);
            DeckManager.Instance.RemoveCard(CardSide.Player, DeckManager.Instance.playerCards[0]);
        }
        else
        {
            HandManager.Instance.AddCardToHand(CardSide.Opponent, DeckManager.Instance.opponentCards[0]);
            DeckManager.Instance.RemoveCard(CardSide.Opponent, DeckManager.Instance.opponentCards[0]);
        }
    }

    [Button("EndTurn")]
    public void EndTurn()
    {
        MaxMana();
        if (turnOwner == CardSide.Player)
        {
            SetTurn(CardSide.Opponent);
            TMProScreenMessage.Instance.AppearMessage("Opponents Turn", 1, Color.white, 2);
        }
        else
        {
            SetTurn(CardSide.Player);
            TMProScreenMessage.Instance.AppearMessage("Your Turn", 1, Color.white, 2);
        }
    }

    public void SetTurn(CardSide targetTurnOwner)
    {
        turnOwner = targetTurnOwner;
        OnTurnStarted.Invoke();
        if (turnOwner == CardSide.Player)
        {
            playerLight.DOIntensity(3.0f, 0.5f);
            opponentLight.DOIntensity(1.5f, 0.5f);

            DealCard(CardSide.Player);
        }
        else
        {
            playerLight.DOIntensity(1.5f, 0.5f);
            opponentLight.DOIntensity(3.0f, 0.5f);

            DealCard(CardSide.Opponent);
        }
    }

    public void DecreaseMana(int value)
    {
        if (turnOwner == CardSide.Player)
        {
            player.DecreaseMana(value);
        }
        else
        {
            opponent.DecreaseMana(value);
        }
    }
    public void MaxMana()
    {
        if (turnOwner == CardSide.Player)
        {
            player.MaxMana();
        }
        else
        {
            opponent.MaxMana();
        }
    }

    public int GetCurrentMana(CardSide side)
    {
        int currentMana;

        if (side == CardSide.Player)
        {
            currentMana = player.currentMana;
        }
        else
        {
            currentMana = opponent.currentMana;
        }
        return currentMana;
    }

    public void FinishTheMatch(CardSide loser)
    {
        if (loser == CardSide.Player)
        {
            TMProScreenMessage.Instance.AppearMessage("You Lose!", 1, Color.red, 2);
        }
        else
        {
            TMProScreenMessage.Instance.AppearMessage("You Win!", 1, Color.green, 2);
        }
        Invoke(nameof(ReturnToMainMenu), 1.5f);
    }

    public void ReturnToMainMenu()
    {
        DOTween.Clear();
        SceneManager.LoadScene(0);
    }

    public void Network_DrawACard(bool isPlayer)
    {
        view.RPC("Receive_DrawACard", Photon.Pun.RpcTarget.Others, isPlayer);
    }
    public void Network_PlayACard(bool isPlayer)
    {
        view.RPC("Receive_PlayACard", Photon.Pun.RpcTarget.Others, isPlayer);
    }

    [Photon.Pun.PunRPC]
    public void Receive_DrawACard(bool isPlayer)
    {
        isLocal = isPlayer;
        //DrawACard(!isPlayer, true);
    }
    [Photon.Pun.PunRPC]
    public void Receive_PlayACard(bool isPlayer)
    {
        isLocal = isPlayer;
        //PlayACard(!isPlayer, true);
    }
    
    public void Server_UpdateCardCount(Card card, bool increase = false)
    {
        card.GetComponent<Photon.Pun.PhotonView>().ViewID = GameManager.isLocal ? GameManager.LocalCount : GameManager.RemoteCount;
        if (GameManager.isLocal == true)
        {
            GameManager.LocalCount++;
        }
        else
        {
            GameManager.RemoteCount++;
        }
    }
}