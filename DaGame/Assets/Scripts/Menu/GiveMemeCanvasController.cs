using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GiveMemeCanvasController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Image _giveMemImage;
    [SerializeField] private Image _giveMemImage2;

    [Header("Settings")] 
    [SerializeField] private float _fadeInDuration = 0.25f;
    [SerializeField] private float _fadeOutDuration = 0.5f;
    [SerializeField] private float _imageDuration = 1f;
    [SerializeField] private float _transparanDuration = 0.5f;
    
    void Start()
    {
        _giveMemImage.enabled = false;
        _giveMemImage2.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Tween ActivateMeme(List<Sprite> giveMem)
    {
        
        _giveMemImage2.color = new Color(1,1,1,0);
        _giveMemImage2.enabled = true;

        _giveMemImage.sprite = giveMem[0];
        _giveMemImage.color = Color.black;
        _giveMemImage.enabled = true;

        DOVirtual.Float(0, 1, _fadeInDuration, value =>
        {
            _giveMemImage.color = Color.Lerp(new Color(0,0,0,1), new Color(1,1,1,1), value);
        });

        DOVirtual.DelayedCall(_fadeInDuration + _imageDuration, () =>
        {
            _giveMemImage2.sprite = giveMem[1];

            DOVirtual.Float(0, 1, _transparanDuration, value =>
            {
                _giveMemImage2.color = Color.Lerp( new Color(1,1,1,0f), new Color(1,1,1,1f), value);
            });

        });
        
        
        DOVirtual.DelayedCall(_imageDuration * 2 + _fadeInDuration , () =>
        {
            _giveMemImage.sprite = giveMem[2];
            
            DOVirtual.Float(0, 1, _transparanDuration, value =>
            {
                _giveMemImage2.color = Color.Lerp( new Color(1,1,1,1), new Color(1,1,1,0), value);
            });
        });

        DOVirtual.DelayedCall(_imageDuration * 3 + _fadeInDuration, () =>
        {

            DOVirtual.Float(1, 0, _fadeOutDuration, value =>
            {
                _giveMemImage.color = Color.Lerp(new Color(0,0,0,0), new Color(1,1,1,1), value);
            });
        });
        
        
        
        return DOVirtual.DelayedCall(_imageDuration * 3 +  _fadeInDuration + _fadeOutDuration, () =>
        {
            _giveMemImage.enabled = false;
            _giveMemImage2.enabled = false;

        });
    }
}
