using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter, IKitchenObjectParent
{
   [SerializeField] private KitchenObjectsSO KitchenObjectSO;
   [SerializeField] private Transform  CounterTopPoint;

   private KitchenObject KitchenObject = null;


   public override void Interact(Player player)  
   {
      if(KitchenObject == null)
      {
        Transform KitchenObjectTransform = Instantiate(KitchenObjectSO.Prefab, CounterTopPoint);
        KitchenObjectTransform.GetComponent<KitchenObject>().SetkitchenObjectParent(this); 
        KitchenObjectTransform.transform.localPosition = Vector3.zero;
      }
      else
      {
            // give object to the player
            KitchenObject.SetkitchenObjectParent(player);
      }
   }
      public Transform GetKitchenObjectFollowTransform()
   {
      return CounterTopPoint;
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
