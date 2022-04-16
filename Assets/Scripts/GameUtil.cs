using UnityEngine;
 
public class GameUtil : MonoBehaviour 
{
    public static GameUtil GU;     
    public static string targetTeleporterId;
     
    void Awake()
    {
        if(GU != null)
            GameObject.Destroy(GU);
        else
            GU = this;
        DontDestroyOnLoad(this);     
    }
}