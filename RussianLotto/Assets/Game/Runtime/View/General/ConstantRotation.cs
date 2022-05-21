using System;
using UnityEngine;

namespace RussianLotto.View
{
    public class ConstantRotation : MonoBehaviour
    {
        [SerializeField] private float _angularSpeed;
        [SerializeField] private RectTransform _rectTransform;

        private void Update()
        {
            _rectTransform.localEulerAngles += Vector3.forward * _angularSpeed * Time.deltaTime;
        }
    }
}
