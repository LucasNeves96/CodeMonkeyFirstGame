using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayer : MonoBehaviour
{   
    [SerializeField] float MoveSpeed = 7;
    //SerializeField significa q ele vai aparecer no nosso menu da Unity.

    private void Update()
    {
        Vector3 InputVector = new Vector3 (0,0,0);
        if(Input.GetKey(KeyCode.W))
        {
           InputVector.z = 1;
        }
        if(Input.GetKey(KeyCode.A))
        {
           InputVector.x = -1;
        }
        if(Input.GetKey(KeyCode.S))
        {
           InputVector.z = -1;
        }
        if(Input.GetKey(KeyCode.D))
        {
           InputVector.x = 1;
        }

        InputVector = InputVector.normalized; // Isso faz com que o tamanho do vetor seja sempre 1 (caso alguém ande na diagonal, a força será de 0,71 em x e z para que o tamanho do vetor movimento seja 1).

        transform.position += (InputVector * MoveSpeed * Time.deltaTime); 

        float RotationSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, InputVector, Time.deltaTime * RotationSpeed);

        Debug.Log(InputVector);

        
    }
}
