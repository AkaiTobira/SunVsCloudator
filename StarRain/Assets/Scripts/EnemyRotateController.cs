using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotateController : BaseController
{

    public List<Vector3> wheelPoints = new List<Vector3>();
    private int wheelPointIndex = 0;

    private Vector3 wheelPoint     = new Vector3(0,0,0);
    private Vector3 wheelPointNext = new Vector3(0,0,0);

    private const int numberOfPoints = 40;
    [SerializeField] public float rotatationRadius = 50.0f;


    private Vector3 RotateVector(Vector3 v, float angle)
    {
        float _x = v.x*Mathf.Cos(angle) - v.y*Mathf.Sin(angle);
        float _y = v.x*Mathf.Sin(angle) + v.y*Mathf.Cos(angle);
        return new Vector3(_x,_y, 0);  
    }

    private void generateWheelPoints(){
        wheelPoints = new List<Vector3>();
        for( int i = 0; i < numberOfPoints; i++){


            wheelPoints.Add( transform.position + Quaternion.Euler(0, 0, (-(360.0f/(float)numberOfPoints )*(float)i) + 22.5f) * new Vector3(0,rotatationRadius,0));

            //wheelPoints.Add( transform.position + RotateVector( Vector2.up * rotatationRadius, 90.0f*(float)i  ) );
        }

    //    print( Mathf.Cos(45) );

    //    print( transform.position );
    //    for( int i = 0; i <numberOfPoints; i++){

    ///        print( i.ToString() +  wheelPoints[i].ToString());
    //    }
    }

    override protected void TeleportByWall(){

    }

    override public void Awake(){
        base.Awake();
        generateWheelPoints();
        wheelPointIndex    = Random.Range(0, numberOfPoints);
        transform.position = wheelPoints[wheelPointIndex];
    }

    override public void ChangeDirection(){
        m_direction = (wheelPoints[wheelPointIndex]-transform.position).normalized;
        float distance = Vector2.Distance( wheelPoints[wheelPointIndex], transform.position);
        if( distance - m_direction.magnitude < (m_speed*0.1) ) wheelPointIndex = (wheelPointIndex + 1)%numberOfPoints;
    }
}
