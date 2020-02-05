using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ViveSR
{
    namespace anipal
    {
        namespace Eye
        {
            public class MonsterRaycast : RootRaycast
            {
                private Monsters monster;
                override protected void Awake()
                {
                    base.Awake();
                }

                override protected void Update()
                {
                    base.Update();
                }

                protected override void CallWhenSee(FocusInfo FocusInfo)
                {
                    base.CallWhenSee(FocusInfo);

                    if (FocusInfo.collider.tag == "Monster")
                    {
                        Debug.Log("Start Ray");
                        monster = FocusInfo.transform.GetComponent<Monsters>();
                        monster.IsEyeon = true;
                    }
                    else
                    {
                        if (monster != null)
                        {
                            monster.IsEyeon = false;
                            monster = null;
                        }
                    }
                }
            }
        }
    }
}
