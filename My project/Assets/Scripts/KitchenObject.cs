using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
   [SerializeField] private KitchenObjectsSO kitchenObjectSO;

   private IKitchenObjectParent kitchenObjectParent;

   public KitchenObjectsSO GetKitchenObjectsSO()
   {
        return kitchenObjectSO;
   }

   public void SetkitchenObjectParent (IKitchenObjectParent kitchenObjectParent)
   {
        if (this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }

        this.kitchenObjectParent = kitchenObjectParent;

        if(kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("IKitchenObjectParent already has a Kitchen Object!");
        }
        
        kitchenObjectParent.SetKitchenObject(this);

        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
   }

   public IKitchenObjectParent GetkitchenObjectParent()
   {
        return kitchenObjectParent;
   }
}
