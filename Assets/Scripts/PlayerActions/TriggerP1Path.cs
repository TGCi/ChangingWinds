using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerP1Path : MonoBehaviour {
    public GameObject Player2;
    FPController PlayerInstance;

    void start()
    {
        PlayerInstance = Player2.GetComponent<FPController>();
    }
    private void onTriggerEnter()
    {

    }
}
