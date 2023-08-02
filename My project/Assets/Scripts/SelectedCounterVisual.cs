using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
[SerializeField] private ClearCounter clearCounter;
[SerializeField] private GameObject visualGameObjetct;

    // Start is called before the first frame update
    void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged (object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if(e.EventSelectedCounter == clearCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }


    private void Hide()
    {
        visualGameObjetct.SetActive(false);
    }

    private void Show()
    {
        visualGameObjetct.SetActive(true);
    }
}
