using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   
   [SerializeField] float MoveSpeed = 7;
    //SerializeField significa q ele vai aparecer no nosso menu da Unity.
   [SerializeField] GameInput gameInput;
   private bool isWalking;
   private Vector2 InputVector;

   private void Update()
   {
      InputVector = gameInput.GetMovementVectorNormalized();
      Vector3 MoveDir = new Vector3(InputVector.x, 0f, InputVector.y);
      transform.position += (MoveDir * MoveSpeed * Time.deltaTime);

      float RotationSpeed = 10f;
      transform.forward = Vector3.Slerp(transform.forward, MoveDir, Time.deltaTime * RotationSpeed);

      isWalking = (Vector3)InputVector != Vector3.zero;

      Debug.Log(InputVector);
   }

   public bool IsWalking()
   {
      return isWalking;
   }
}
