using UnityEngine;

public class OnSceneLoad : MonoBehaviour
{
    void Start()
    {
        if(GameUtil.IsPlayerTeleported)
        {
            MovePlayerToTargetTeleporter();
            GameUtil.IsPlayerTeleported = false;
        }        
    }

    private void MovePlayerToTargetTeleporter()
    {
        GameObject[] playerObjs = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] teleporterObjs = GameObject.FindGameObjectsWithTag("Teleporter");
        CardinalDirection targetTeleporterLocation = GameUtil.TargetTeleporterLocation;

        for (int i = 0; i < teleporterObjs.Length; i++) {
            Teleporter t = teleporterObjs[i].GetComponent<Teleporter>();
            if (t.Location == targetTeleporterLocation) {
                playerObjs[0].transform.localPosition = t.transform.position;
                break;
            }
        }
    }
}
