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
        CardinalDirection targetTeleporterLocation = GameUtil.TargetTeleporterLocation;

        if (targetTeleporterLocation != null) { //TODO OSW CHECK LOGIC, use some unset enum entry?
            for (int i = 0; i < teleporterObjs.Length; i++) {
                Teleporter t = teleporterObjs[i].GetComponent<Teleporter>();
                if (t.Location == targetTeleporterLocation) {
                    playerObjs[0].transform.localPosition = t.transform.position;
                    break;
                }
            }
        }
    }
}
