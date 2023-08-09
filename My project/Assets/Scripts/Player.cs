using UnityEngine;
using System;

public class Player : MonoBehaviour, IKitchenObjectParent
{  
   public static Player Instance{ get; private set; }
   //Isso é uma PROPRIEDADE! Ou sejE! É algo que não está dentro do objeto criado para o Player. Está SOBRE ele. Está na CLASSE dele!
   //Esse get; set; significa q eu vou poder pegar o valor que tem nela (get) e vou poder mudar ele (set). Para o set, vamos usar um
   // private set, porque não queremos outras classes alterando essa valor, só lendo mesmo.

   public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
   
   public class OnSelectedCounterChangedEventArgs : EventArgs 
   {
      public BaseCounter EventSelectedCounter;
   }

    [SerializeField] float MoveSpeed = 7;
    //SerializeField significa q ele vai aparecer no nosso menu da Unity.
    [SerializeField] GameInput gameInput;
    [SerializeField] LayerMask layerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;


    private bool isWalking;
    private Vector3 LastInteractDir;
    private BaseCounter SelectedCounter = null;
    private KitchenObject KitchenObject = null;
    

    private void Awake() 
    {
      if(Instance != null)
      {
         Debug.LogError("There is more than one Player instance!");
      }
      Instance = this;   
    }

   private void Start()
   {
      gameInput.OnInteractAction += GameInput_OnInteractAction;
   }

   private void GameInput_OnInteractAction(object sender, System.EventArgs e)
   {
      if(SelectedCounter != null)
      {
         SelectedCounter.Interact(this);
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
         if(raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
         {
            if(baseCounter != SelectedCounter)
            {
               SetSelectedCounter(baseCounter);
            }
         }
         else
         {
            SetSelectedCounter(null);
         }
      }
      else
      {
         SetSelectedCounter(null);
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
            else
            {
               // Dont move at all!
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


   private void SetSelectedCounter(BaseCounter SelectedCounter)
   {
      this.SelectedCounter = SelectedCounter;

      OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs{EventSelectedCounter = SelectedCounter});
   }

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.KitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return this.KitchenObject;
    }

    public void ClearKitchenObject()
    {
        this.KitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return this.KitchenObject != null;
    }
}
