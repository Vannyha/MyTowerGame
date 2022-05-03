using System;
using UnityEngine;
using UnityEngine.UI;

namespace UIManagers.UIHelpers
{
    public class PicNameSlotUI : MonoBehaviour
    {
        [SerializeField] private Image currentImage;
        [SerializeField] private Button currentButton;
        [SerializeField] private Text currentText;
        [SerializeField] private Image lockPicture;

        public void SetButtonAction(Action action)
        {
            currentButton.onClick.AddListener(action.Invoke);
        }

        public void SetImage(Sprite source)
        {
            currentImage.sprite = source;
        }

        public void SetText(string source)
        {
            currentText.text = source;
        }

        public void SetInteractableStatus(bool value)
        {
            currentButton.interactable = value;
            lockPicture.gameObject.SetActive(!value);
        }
    }
}