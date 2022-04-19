using UnityEngine;

public static class MapUtil
{
    public static Vector3 getCentreOfTile(int x, int y) {

        // -2, 0 (on grid) -> needs to be -1.0, -0.25 (top down)
        // -1, 0 (on grid) -> needs to be -0.5,  0    (top down)
        //  0, 0 (on grid) -> needs to be    0,  0.25 (top down) -> base offset = (0, 0.25)
        //  1, 0 (on grid) -> needs to be  0.5,  0.5  (top down) 

        //  0,-2 (on grid) -> needs to be  1.0, -0.25 (top down)
        //  0,-1 (on grid) -> needs to be  0.5,     0 (top down)
        //  0, 0 (on grid) -> needs to be    0,  0.25 (top down)
        //  0, 1 (on grid) -> needs to be -0.5,  0.50 (top down)

        Vector3 baseOffset = new Vector3( 0, 0.25f, 0);
        Vector3 xShift = new Vector3( ( 0.5f * x) , (0.25f * x) , 0 );
        Vector3 yShift = new Vector3( (-0.5f * y ), (0.25f * y) , 0 );

        return baseOffset + xShift + yShift;
    }

}