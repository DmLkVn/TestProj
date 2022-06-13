using UnityEngine;
using UnityEngine.Events;

public class DudeController : MonoBehaviour
{
    new Transform transform;
    Vector3 nextPoint;
    Animator animator;

    void Start()
    {
        Coin.scoreUp.AddListener(Jump);
        animator = GetComponentInChildren<Animator>();
        transform = GetComponent<Transform>();
        NewPoint(out nextPoint);
    }

    private void Jump()
    {
        animator.Play("jump");
    }
    private void NewPoint(out Vector3 newPoint)
    {
        newPoint.y = 0;
        newPoint.x = Random.Range(-50,50);
        newPoint.z = Random.Range(0, 100);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (transform.position == nextPoint)
        {
            NewPoint(out nextPoint);
        }
        transform.LookAt(nextPoint);
        transform.position = Vector3.MoveTowards(transform.position, nextPoint, 0.5f);
    }
}
