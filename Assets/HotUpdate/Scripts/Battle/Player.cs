using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class Player
    {
        // Player Config
        private float mHP = 0.0f;

        private float mFullHP = 0.0f;
        public float FullHP { get { return mFullHP; } }

        public float HP
        {
            get { return mHP; }
            set
            {
                mHP = Mathf.Min(value, mFullHP);
                BattleEventManager.OnPlayerHPChanged.Invoke(HP);

                if (mHP <= 0)
                    BattleEventManager.OnPlayerDead.Invoke();
            }
        }

        public Player()
        {
            mFullHP = DataManager.DataTable.TbGlobalConfig.PlayerMaxHp;
            HP = mFullHP;
        }
    }
}
