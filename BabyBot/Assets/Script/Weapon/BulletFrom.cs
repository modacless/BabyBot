using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFrom : MonoBehaviour
{
    PlayerInfo playerFrom;

    public void AddPlayerInfoOnBullet(PlayerInfo player)
    {
        playerFrom = player;
    }
}
