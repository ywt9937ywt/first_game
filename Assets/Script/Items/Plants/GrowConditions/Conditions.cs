using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConditionName
{
    WATER = 1,
    SUN = 2,

    SOUL = 20,
    MAGIC = 21,
}
public class Conditions
{
    public static ConditionBase GetCondition(ConditionName name)
    {
        switch (name)
        {
            case ConditionName.WATER:
                return new conNeedWater();
            case ConditionName.SUN:
                return new conNeedSun();
            case ConditionName.SOUL:
                return new conNeedSoul();
            case ConditionName.MAGIC:
                return new conNeedMagic();
        }
        return null;
    }
    public class conNeedWater : ConditionBase
    {
        public override bool SatisfyOrNot()
        {
            return true;
        }

    }
    public class conNeedSun : ConditionBase
    {
        public override bool SatisfyOrNot()
        {
            return true;
        }
    }

    public class conNeedSoul : ConditionBase
    {
        public override bool SatisfyOrNot()
        {
            return true;
        }
    }

    public class conNeedMagic : ConditionBase
    {
        public override bool SatisfyOrNot()
        {
            return true;
        }
    }
}
