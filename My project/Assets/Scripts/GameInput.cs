using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{   
    public event EventHandler OnInteractAction;
    private PlayerInputActions playerInputActions;
    private void Awake() 
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
        // A "?" serve para que, quando nenhum obj for subscriber do evento OnInterAction, ele não executa. é o mesmo q       
        // if(OninteractAction != null)
    }
   public Vector2 GetMovementVectorNormalized()
   {
        Vector2 InputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        return InputVector = InputVector.normalized; // Isso faz com que o tamanho do vetor seja sempre 1 (caso alguém ande na diagonal, a força será de 0,71 em x e z para que o tamanho do vetor movimento seja 1).
    
   }
}
