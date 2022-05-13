using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Collector : MonoBehaviour
{

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.GetComponent<SealsManager>() != null)
        {
            collision.GetComponent<SealsManager>().points++;
            GetComponent<AudioSource>().Play();
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        transform.localEulerAngles += new Vector3(0, 1, 0) * 40 * Time.deltaTime;
    }
}
