using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{   
    private PlayerInputActions playerInputActions;
    private void Awake() 
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }

   public Vector2 GetMovementVectorNormalized()
   {
        Vector2 InputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        return InputVector = InputVector.normalized; // Isso faz com que o tamanho do vetor seja sempre 1 (caso alguém ande na diagonal, a força será de 0,71 em x e z para que o tamanho do vetor movimento seja 1).
    
   }
}
