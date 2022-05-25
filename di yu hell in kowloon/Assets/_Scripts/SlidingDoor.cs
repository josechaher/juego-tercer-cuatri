using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    [SerializeField] List<Target> targets;
    [SerializeField] float speed = 5;
    [SerializeField] float height = 10;

    public void Open()
    {
        GetComponent<AudioSource>().Play();
        StartCoroutine(Slide());
    }

    IEnumerator Slide()
    {
        float distanceMoved = 0;
        while (distanceMoved < height)
        {
            transform.position += -Vector3.up * speed * Time.deltaTime;
            distanceMoved += speed * Time.deltaTime;
            yield return null;
        }
    }

    public void TargetDestroyed(Target target)
    {
        if (targets.Contains(target))
        {
            targets.Remove(target);
            Destroy(target.gameObject);
            if (targets.Count == 0) Open();
        }
        
    }
}
