using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] Text timeText;
    [SerializeField] GameObject nickNamePanel;
    [SerializeField] TMP_InputField nickNameInputField;

    [SerializeField] int playerCount = 3;

    [SerializeField] float timer;
    private int minute;
    private int second;
    
    private void Awake()
    {
        CreatePlayer();

        CheckNickName();
    }

    private void Start()
    {
        CheckPlayer();
    }

    private void CreatePlayer()
    {
        PhotonNetwork.Instantiate("Player", RandomPosition(5), Quaternion.identity);
    }

    public Vector3 RandomPosition(float distance)
    {
        Vector3 direction = Random.insideUnitSphere;

        direction.Normalize();

        direction *= distance;

        direction.y = 1;

        return direction;
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        PhotonNetwork.SetMasterClient(PhotonNetwork.PlayerList[0]);
    }

    public void CreateNickName()
    {
        PlayerPrefs.SetString("Nick Name", nickNameInputField.text);

        PhotonNetwork.NickName = nickNameInputField.text;

        nickNamePanel.SetActive(false);
    }

    public void CheckNickName()
    {
        string nickName = PlayerPrefs.GetString("Nick Name");

        PhotonNetwork.NickName = nickName;

        if(string.IsNullOrEmpty(nickName))
        {
            nickNamePanel.SetActive(true);
        }
        else
        {
            nickNamePanel.SetActive(false);
        }
    }

    public void CheckPlayer()
    {
        if(PhotonNetwork.PlayerList.Length >= playerCount)
        {
            photonView.RPC("RemoteTimer", RpcTarget.All);
        }
    }
    
    [PunRPC]
    public void RemoteTimer()
    {
        StartCoroutine(Timer());
    }

    public IEnumerator Timer()
    {
        minute = (int)(timer / 60f);
        second = (int)(timer % 60f);

        while (minute >= 0)
        {
            while (second > 0)
            {
                second--;

                timeText.text = minute.ToString("00") + " : " + second.ToString("00");

                yield return new WaitForSeconds(1f);

                if (minute == 0 && second == 0)
                {
                    yield break;
                }
            }

            minute--;
            second = 59;

            timeText.text = minute.ToString("00") + " : " + second.ToString("00");

            yield return new WaitForSeconds(1f);
        }
    }
}
