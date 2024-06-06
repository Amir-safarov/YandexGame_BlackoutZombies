using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]

public class RedShadowTextController : MonoBehaviour
{
    [SerializeField] private Text _boldText;
    [SerializeField] private Text _normalText;
    [SerializeField] private bool _isCheckOnValidate;

    private const int MaxFontSize = 118;


    private void OnValidate()
    {
        if (_boldText == null)
            _boldText = GetComponent<Text>();
        if (_normalText == null)
            _normalText = transform.GetChild(0).GetComponent<Text>();
        if (_isCheckOnValidate)
        {
            _normalText.text = _boldText.text;
            if (_boldText.fontSize > MaxFontSize)
                _boldText.fontSize = MaxFontSize;
            _normalText.fontSize = _boldText.fontSize;

        }
    }


}
