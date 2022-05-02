using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAnimation : MonoBehaviour
{
    public string[] staticDirections = { "Idle_N", "Idle_NW", "Idle_W", "Idle_SW", "Idle_S", "Idle_SE", "Idle_E", "Idle_NE" };
    public string[] runDirections = { "Run_N", "Run_NW", "Run_W", "Run_SW", "Run_S", "Run_SE", "Run_E", "Run_NE" }; 

    private Animator anim;

    int lastDirection; 
    // Start is called before the first frame update

    private void Awake()
    {
        anim = GetComponent<Animator>();
        float result1 = Vector2.SignedAngle(Vector2.up, Vector2.right);
        float result2 = Vector2.SignedAngle(Vector2.up, Vector2.left); 
        float result3 = Vector2.SignedAngle(Vector2.up, Vector2.down);
   
    }

    public void SetDirection(Vector2 _direction) {
        string[] directionArray = null;
        if (_direction.magnitude < 0.01)//MARKER character is static. His velocity is close to zero
        { 
            directionArray = staticDirections;
        }
        else {
            directionArray = runDirections;
            lastDirection = DirectionToIndex(_direction); //MARKER Get the index of the slice from the direction vector
        }


        anim.Play(directionArray[lastDirection]);
    }

    private int DirectionToIndex(Vector2 _direction) {
        Vector2 norDir = _direction.normalized;

        float step = 360 / 8; // circle devided into 45 degres slices numbering 8 
        float offfset = step / 2; // 22.5 degeres

        float angle = Vector2.SignedAngle(Vector2.up, norDir); // returns the signed angle in degrees

        angle += offfset;

        if (angle < 0) {
            angle += 360; 
        }

        float stepCount = angle / step;
        int flooredInt = Mathf.FloorToInt(stepCount);
        return flooredInt; 
    }
}
