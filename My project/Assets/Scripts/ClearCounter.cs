using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
   [SerializeField] private KitchenObjectsSO KitchenObjectSO;
   [SerializeField] private Transform  CounterTopPoint;
   [SerializeField] private ClearCounter SecondClearCounter;
   [SerializeField] private bool testing;

   private KitchenObject KitchenObject = null;

   private void Update() {
      if(testing && Input.GetKeyDown(KeyCode.T))
      {
         if(KitchenObject != null)
         {
            KitchenObject.SetClearCounter(SecondClearCounter);
         }
      }
   }

   public void Interact()  
   {
      if(KitchenObject == null)
      {
        Transform KitchenObjectTransform = Instantiate(KitchenObjectSO.Prefab, CounterTopPoint);
        KitchenObjectTransform.GetComponent<KitchenObject>().SetClearCounter(this); 
        KitchenObjectTransform.transform.localPosition = Vector3.zero;

        KitchenObject = KitchenObjectTransform.GetComponent<KitchenObject>(); 

        KitchenObject.SetClearCounter(this);
      }
      else
      {
         Debug.Log(KitchenObject.GetClearCounter());
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
      return KitchenObject != null;
   }
}
