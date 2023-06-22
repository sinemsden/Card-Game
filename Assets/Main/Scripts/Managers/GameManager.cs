using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using DG.Tweening;
using Photon;
using Photon.Realtime;
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
    public bool dealCardToMaster = true;
    public float dealTimer = 0;

    [Header("Network")]
    public static PhotonView view;
    public static int CardCount = 100;
    public static int PlayerCount = 400;

    private void Start()
    {
        view = Photon.Pun.PhotonView.Get(this);

        //Get Player Card IDS

        DeckManager.Instance.GetCards();

        UpdatePlayerNames();
        
        if (PhotonNetwork.IsMasterClient == false)
        {
            turnOwner = CardSide.Opponent;

            playerLight.DOIntensity(1.5f, 0.5f);
            opponentLight.DOIntensity(3.0f, 0.5f);

            TMProScreenMessage.Instance.AppearMessage("Opponents Turn", 1, Color.white, 2);
        }
    }
    private void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (dealCardCountFirstTime > 0)
            {
                if (dealTimer < 1.0f)
                {
                    dealTimer += Time.deltaTime * 10;
                }
                else
                {
                    if (dealCardToMaster == true)
                    {
                        view.RPC("DealCard", Photon.Pun.RpcTarget.MasterClient, CardSide.Opponent);
                        view.RPC("DealCard", Photon.Pun.RpcTarget.Others, CardSide.Player);
                        dealCardToMaster = false;
                    }
                    else
                    {
                        view.RPC("DealCard", Photon.Pun.RpcTarget.MasterClient, CardSide.Player);
                        view.RPC("DealCard", Photon.Pun.RpcTarget.Others, CardSide.Opponent);
                        dealCardToMaster = true;
                    }

                    dealCardCountFirstTime--;   
                    dealTimer = 0;
                }
            }
        }
    }
    public void UpdatePlayerNames()
    {
        player.playerName.text = PhotonNetwork.LocalPlayer.NickName;
        Player[] players = PhotonNetwork.PlayerListOthers;
        if (players.Length > 0)
        {
            opponent.playerName.text = players[0].NickName;
        }
    }

    [PunRPC]
    public void EffectToPlayer(int effectorId, bool isPlayer)
    {
        Card card = CardManager.FetchCardById(effectorId);

        if (card.side == player.side)
        {
            player.view.RPC("WasEffected", RpcTarget.Others, effectorId, 1);
            opponent.view.RPC("WasEffected", RpcTarget.MasterClient, effectorId, 1);
            Debug.Log("a");
        }
        else
        {
            player.view.RPC("WasEffected", RpcTarget.MasterClient, effectorId, 1);
            opponent.view.RPC("WasEffected", RpcTarget.Others, effectorId, 1);
            Debug.Log("b");
        }
    }

    [PunRPC]
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
        if (turnOwner == CardSide.Player)
        {
            view.RPC("Network_EndTurn", Photon.Pun.RpcTarget.All);
        }
    }

    [PunRPC]
    public void Network_EndTurn()
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

    [PunRPC]
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
        PickACard.Instance.Invoke("Appear", 1.5f);
    }

    public void ReturnToMainMenu()
    {
        DOTween.Clear();
        PhotonNetwork.LeaveLobby();
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

    public void Server_UpdateCardCount(Card card, bool increase = false)
    {
        card.GetComponent<Photon.Pun.PhotonView>().ViewID = CardCount;
        card.id = CardCount;
        card.IdNumber.text = card.id.ToString();
        card.IdNumber2.text = card.id.ToString();

        CardManager.activeCards.Add(card);
        if (increase == true)
        {
            CardCount++;
        }
    }
    public void Server_InitPlayer(PlayerInstance playerInstance)
    {
        playerInstance.GetComponent<Photon.Pun.PhotonView>().ViewID = PlayerCount;
        playerInstance.id = PlayerCount;

        CardManager.playerInstances.Add(playerInstance);
        PlayerCount++;
    }
}