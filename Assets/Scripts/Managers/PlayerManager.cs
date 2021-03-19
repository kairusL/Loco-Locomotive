﻿using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerSpawnPod = null;
    [SerializeField] private TrainData _trainData = null;

    private RespawnPod _respawnPod = null;
    private uint _playerCount = 0;

    public void PlayerRespawnPod(PlayerInput input)
    {
        //if (++_playerCount > 1)
        //{
        //    _trainData.PlayerCount++;
        //}
        _trainData.PlayerCount = input.playerIndex;
        PlayerV1 player = input.GetComponent<PlayerV1>();
        player.transform.SetParent(_trainData.TrainTransform);
        player.transform.position = _playerSpawnPod.transform.position;
        player.RespawnPoint = _playerSpawnPod.transform.position;

        if (!_respawnPod)
        {
            _respawnPod = _playerSpawnPod.GetComponent<RespawnPod>();
        }

        _respawnPod.AnimationRespawnPod();
    }

}
