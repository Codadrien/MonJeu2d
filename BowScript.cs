using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowScript : MonoBehaviour
{
    public GameObject arrow;
    public float launchForce;
    public Transform shotPoint;

    public GameObject point;
    public GameObject[] points;
    public int numberOfPoints;
    public float spaceBetweenPoints;
    Vector2 direction;

    public Transform targetTransform;

    public BowJoystick bowjoystick;

    public Vector2 origin;

    public LayerMask mouseAimMask;

    public Camera mainCamera;

    public int shottime;
    public bool canShoot;

    private void Start()
    {

        points = new GameObject[numberOfPoints];
        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i] = Instantiate(point, shotPoint.position, Quaternion.identity);
          
        }
    }

    // Update is called once per frame
    void Update()
    {
        direction = new Vector2(bowjoystick.bowDirectionH, bowjoystick.bowDirectionV);
        transform.right = direction;

        Ray ray = mainCamera.ScreenPointToRay(direction*1000);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mouseAimMask))
        {
            targetTransform.position = hit.point;
            Debug.Log("Le raycast touche un objet");
            Debug.Log(hit.point);
            Debug.DrawRay(mainCamera.transform.position, direction*1000, Color.red);
        }


        if (bowjoystick.bowDirectionH <= -0.1f || bowjoystick.bowDirectionH >= 0.1f)
        {
            
            if(canShoot == true)
            {
                StartCoroutine(ShootDelay()); // when we hit space key we want to launch an arrow
                canShoot = false;
               
            }
        }
        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i].transform.position = PointPosition(i * spaceBetweenPoints);
        }

        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i].SetActive(true);
            Debug.Log("activer");
        }

        if (bowjoystick.bowDirectionH == 0f)
        {
            canShoot = true;
        }
    }

    void Shoot()
    {
        GameObject newArrow = Instantiate(arrow, shotPoint.position, shotPoint.rotation);

        //apply force to the arrow we just created
        newArrow.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce;
    }

    Vector2 PointPosition(float t)
    {

        Vector2 position = (Vector2)shotPoint.position + (direction.normalized * launchForce * t) + 0.5f * Physics2D.gravity * (t * t);
        return position;
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(1);
        canShoot = true;
        Shoot();
    }

    public void OnDisable()
    {
        for (int i = 0; i < numberOfPoints; i++)
        {
          points[i].SetActive(false);
            Debug.Log("déactiver");
        }
    }
}
