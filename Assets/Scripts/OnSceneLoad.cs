using UnityEngine;

public class OnSceneLoad : MonoBehaviour
{
    GameObject[] playerObjs;
    GameObject[] teleporterObjs;
    string targetTeleporterId;

    void Start()
    {
        playerObjs = GameObject.FindGameObjectsWithTag("Player");
        teleporterObjs = GameObject.FindGameObjectsWithTag("Teleporter");
        targetTeleporterId = GameUtil.targetTeleporterId;

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
