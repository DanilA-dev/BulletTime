using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Model
{
    public class Weapon
    {
        private CountdownTimer _fireTimer;
        private CountdownTimer _reloadTimer;
        private List<BaseTimer> _timers;

        public event Action OnFireAction;
        
        public int MaxAmmo { get; private set; }
        public int CurrentAmmo { get; private set; }
        public float Damage { get; private set; }
        public float RateOfFire { get; private set; }
        public float ReloadTime { get; private set; }
        public WeaponSpread Spread { get; private set; }

        private bool IsReloading => _reloadTimer.IsRunning;
        private bool IsShooting => _fireTimer.IsRunning;

        
        public Weapon(int maxAmmo, float damage, float reloadTime,
            float rateOfFire, WeaponSpread weaponSpread, Action fireAction)
        {
            MaxAmmo = maxAmmo;
            Damage = damage;
            RateOfFire = rateOfFire;
            ReloadTime = reloadTime;
            Spread = weaponSpread;
            OnFireAction = fireAction;
            
            _fireTimer = new CountdownTimer(RateOfFire);
            _reloadTimer = new CountdownTimer(ReloadTime);
            _fireTimer.OnTimerEnd += Fire;
            _reloadTimer.OnTimerEnd += Reload;
            
            _timers = new List<BaseTimer>
            {
                _fireTimer,
                _reloadTimer
            };

        }

        public void StartFire()
        {
            if(!IsShooting)
                _fireTimer.Start();
        }
        
        private void Fire()
        {
            if(IsReloading)
                return;
            
            if (CurrentAmmo <= 0)
            {
                
                _reloadTimer.Start();
                return;   
            }
            
            CurrentAmmo--;
            OnFireAction?.Invoke();
        }

        public void UpdateTimers() => _timers.ForEach(t => t.Tick(Time.deltaTime));

        private void Reload() => CurrentAmmo = MaxAmmo;
    }
    
    [System.Serializable]
    public struct WeaponSpread
    {
        public Vector2 X;
        public Vector2 Y;

        public Vector3 GetSpread()
        {
            var x = Random.Range(X.x, X.y);
            var y = Random.Range(Y.x, Y.y);
            return new Vector3(x, y);
        }
    }
}