﻿using UnityEngine;

namespace Turret
{
    public abstract class Weapons
    {
        protected TurretData _turretData = null;
        protected Transform _spawnPoint = null;
        protected ObjectPoolManager _objectPoolManager = null;
        protected float _currentAmmo = 0.0f;

        public Weapons(TurretData setupData)
        {
            _turretData = setupData;
        }

        public abstract void SetUp(Transform spawnPoint);

        public abstract void SetUp(Transform spawnPoint, LineRenderer laserBeam);

        public abstract void SetFire();

        public abstract void Reload();
    }

}

