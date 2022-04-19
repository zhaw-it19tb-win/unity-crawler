using System;
using UnityEngine;

public class OnSceneLoad : MonoBehaviour
{
    void Start()
    {
        MovePlayerToTargetTeleporter();
    }

    private void MovePlayerToTargetTeleporter()
    {
        GameObject[] playerObjs = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] teleporterObjs = GameObject.FindGameObjectsWithTag("Teleporter");
        string targetTeleporterId = GameUtil.targetTeleporterId;

        if (targetTeleporterId != null) {
            for (int i = 0; i < teleporterObjs.Length; i++) {
                Teleporter t = teleporterObjs[i].GetComponent<Teleporter>();
                if (t.teleporterId == targetTeleporterId) {
                    playerObjs[0].transform.localPosition = t.transform.position;
                    break;
                }
            }
        }
    }
}
