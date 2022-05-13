using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fading_platform : MonoBehaviour
{
    [SerializeField] string playerTag = "Player";
    [SerializeField] float disappearTime = 3;

    Animator myAnim;

    [SerializeField] bool canReset;
    [SerializeField] float resetTime;

    void Start()
    {
        myAnim = GetComponent<Animator>();
        myAnim.SetFloat("DisappearTime", 1 / disappearTime); // esto controla la velociad con la que la plataforma desaparece
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == playerTag)
        {
            Debug.Log("la plataforma colisiono con el jugador");
            myAnim.SetBool("Trigger", true);
        }
    }

    public void TriggerReset() //  yo le di el nombre triggerreset. No esta en el monobehaviour
    {
        if (canReset)
        {
            StartCoroutine(Reset());
        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(resetTime);//reset time es el tiempo de la animacion (1) dividido disappear time
        myAnim.SetBool("Trigger", false);
    }
}
