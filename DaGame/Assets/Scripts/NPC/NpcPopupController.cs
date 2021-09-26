using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NpcPopupController : MonoBehaviour
{

    [Header("Dependencies")]
    [SerializeField] private GameObject _popupCtn;
    [SerializeField] private TextMeshPro _tmpKonus;
    [SerializeField] private TextMeshPro _tmpTakas;
    [SerializeField] private TextMeshPro _tmpGorusuruz;

    [Header("Settings")] 
    [SerializeField] private string _konusText;
    [SerializeField] private string _takasText;
    [SerializeField] private string _gorusuruzText;

    private int _index;

    private string _arrowString = "»";
    private bool _isPopupWindowActive = false;
    public bool IsPopupWindowActive => _isPopupWindowActive;

    private void Start()
    {
        OnIndexChange(0);
        ClosePopup();
    }

    public void OpenPopup()
    {
        _popupCtn.SetActive(true);
        OnIndexChange(0);
        _isPopupWindowActive = true;
    }
    public void ClosePopup()
    {
        _isPopupWindowActive = false;
        _popupCtn.SetActive(false);
    }

    public void GoDown()
    {
        OnIndexChange( ((_index + 1)+3)  % 3 );
    }

    public void GoUp()
    {
        OnIndexChange( ((_index - 1)+3) % 3 );
    }

    public int GetCurrentIndex()
    {
        return _index;
    }
    

    void OnIndexChange(int newIndex)
    {
        _index = newIndex;

        switch (_index)
        {
            case 0:
                _tmpKonus.text = _arrowString + _konusText; 
                _tmpTakas.text = _takasText;
                _tmpGorusuruz.text = _gorusuruzText;
                break;
            case 1: 
                _tmpTakas.text = _arrowString + _takasText; 
                _tmpGorusuruz.text = _gorusuruzText;
                _tmpKonus.text = _konusText;
                break;
            case 2: 
                _tmpGorusuruz.text = _arrowString + _gorusuruzText; 
                _tmpKonus.text = _konusText;
                _tmpTakas.text = _takasText;
                break;
        }
    }
    
}
