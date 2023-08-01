using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   
   [SerializeField] float MoveSpeed = 7;
    //SerializeField significa q ele vai aparecer no nosso menu da Unity.
   [SerializeField] GameInput gameInput;
   [SerializeField] LayerMask layerMask;
   private bool isWalking;
   private Vector3 LastInteractDir;

   private void Start()
   {
      gameInput.OnInteractAction += GameInput_OnInteractAction;
   }

   private void GameInput_OnInteractAction(object sender, System.EventArgs e)
   {
      Vector2 InputVector = gameInput.GetMovementVectorNormalized();
      Vector3 MoveDir = new Vector3(InputVector.x, 0f, InputVector.y);

      if (MoveDir != Vector3.zero)
      {
         LastInteractDir = MoveDir;
      }

      float InteractDistance = 2f;
      if(Physics.Raycast(transform.position, LastInteractDir, out RaycastHit raycastHit, InteractDistance, layerMask))
      {
         if(raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
         {
            // Has Clear Counter!
            clearCounter.Interact(); 
         }
      }
      // raycastHit retorna o objeto q foi alvejado (e é um 'out' que significa que ele retorna esse valor da função).
   }

   private void Update()
   {
      HandleWalking();
      HandleInteractions();
   }

   public bool IsWalking()
   {
      return isWalking;
   }

   private void HandleInteractions()
   {
      Vector2 InputVector = gameInput.GetMovementVectorNormalized();
      Vector3 MoveDir = new Vector3(InputVector.x, 0f, InputVector.y);

      if (MoveDir != Vector3.zero)
      {
         LastInteractDir = MoveDir;
      }

      float InteractDistance = 2f;
      if(Physics.Raycast(transform.position, LastInteractDir, out RaycastHit raycastHit, InteractDistance, layerMask))
      {
         if(raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
         {
            // Has Clear Counter!
            //clearCounter.Interact(); 
         }
      }
      // raycastHit retorna o objeto q foi alvejado (e é um 'out' que significa que ele retorna esse valor da função).
   }


   private void HandleWalking()
   {
      Vector2 InputVector = gameInput.GetMovementVectorNormalized();
      Vector3 MoveDir = new Vector3(InputVector.x, 0f, InputVector.y);
      
      float MoveDistance = MoveSpeed * Time.deltaTime;
      float PlayerRadius = .7f;
      float PlayerHeight = 2f;
      bool CanMove = !Physics.CapsuleCast(transform.position, transform.position + (Vector3.up*PlayerHeight), PlayerRadius, MoveDir, MoveDistance);

      if(!CanMove)
      {
         //Cannot move towards dir, but can it move to x or z?
         Vector3 MoveDirX = new Vector3(MoveDir.x,0,0).normalized;
         CanMove = !Physics.CapsuleCast(transform.position, transform.position + (Vector3.up*PlayerHeight), PlayerRadius, MoveDirX, MoveDistance);
         if(CanMove)
         {
            MoveDir = MoveDirX;
         }
         else
         {
            Vector3 MoveDirZ = new Vector3(0,0,MoveDir.z).normalized;
            CanMove = !Physics.CapsuleCast(transform.position, transform.position + (Vector3.up*PlayerHeight), PlayerRadius, MoveDirZ, MoveDistance);
            if(CanMove)
            {
               MoveDir = MoveDirZ;
            }
            else{// Dont move at all!
            }
         }
      }

      if(CanMove)
      {
         transform.position += (MoveDir * MoveDistance);
      }
      

      float RotationSpeed = 10f;
      transform.forward = Vector3.Slerp(transform.forward, MoveDir, Time.deltaTime * RotationSpeed);

      isWalking = (Vector3)InputVector != Vector3.zero;
   }

}
