using System;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerPanel : MonoBehaviour
    {
        [SerializeField] private Image _healthbar;
        [SerializeField] private Image _dashIcon;
        [SerializeField] private Image _dashFillImage;
        [SerializeField] private Image _portrait;
        private Image _playerPortrait;

        private PlayerController _playerController;

        
        
        public void SetupUi(PlayerController player)
        {
            _playerController = player;
            _portrait.sprite = player.CharacterSprite;
            player.playerHealth.OnHpChanged += PlayerHealthOnHpChanged;
            player.OnDashUsed += PlayerOnDashUsed;
            player.OnDashCd += PlayerOnDashCd;
            player.OnDashReady += PlayerOnDashReady;
        }

        private void PlayerOnDashUsed(object sender, EventArgs e)
        {
            _dashIcon.enabled = false;
        }
        
        private void PlayerOnDashCd(object sender, float fillValue)
        {
            _dashFillImage.fillAmount = fillValue;
        }

        private void PlayerOnDashReady(object sender, EventArgs e)
        {
            _dashIcon.enabled = true;
        }

        private void PlayerHealthOnHpChanged(object sender, float fillValue)
        {
            _healthbar.fillAmount = fillValue;
        }

        private void OnDestroy()
        {
            _playerController.playerHealth.OnHpChanged -= PlayerHealthOnHpChanged;
            _playerController.OnDashUsed -= PlayerOnDashUsed;
            _playerController.OnDashCd -= PlayerOnDashCd;
            _playerController.OnDashReady -= PlayerOnDashReady;
        }
    }
}
