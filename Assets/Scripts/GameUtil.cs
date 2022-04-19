using UnityEngine;
using UnityEngine.SceneManagement;
 
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

        SceneManager.sceneLoaded += DoWhenSceneLoads;     
    }

    private void DoWhenSceneLoads( Scene scene, LoadSceneMode mode)
    {
        Debug.Log("new scene: ");
        Debug.Log(scene);
        Debug.Log("loading mode: ");
        Debug.Log(mode);

    }
}