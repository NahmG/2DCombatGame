using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallLaunch : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject fireBallFrefab;
    public float existTime;

    public void FireBall()
    {
        bool direction = gameObject.GetComponent<Wizard>()._isFacingLeft;
        GameObject fireBall = Instantiate(fireBallFrefab, launchPoint.position, Quaternion.identity);
        fireBall.GetComponent<Projectile>().facingLeft = direction;

        Destroy(fireBall, existTime);
    }
}
