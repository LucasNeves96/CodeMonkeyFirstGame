using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
[SerializeField] private BaseCounter baseCounter;
[SerializeField] private GameObject[] visualGameObjetctArray;

    // Start is called before the first frame update
    void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged (object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if(e.EventSelectedCounter == baseCounter)
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
        foreach (GameObject visualGameObjetct in visualGameObjetctArray)
        { 
            visualGameObjetct.SetActive(false);
        }
    }

    private void Show()
    {
        foreach (GameObject visualGameObjetct in visualGameObjetctArray)
        {
            visualGameObjetct.SetActive(true);
        }
    }
}
