using SDC.Tools.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDC.Tools.Factories
{
    public class PartsFactory
    {
        public static (int, int) GetParts(PartsEnum parts)
        {
            switch (parts)
            {
                case PartsEnum.train_30_test_70:
                    return (30, 70);
                case PartsEnum.train_40_test_60:
                    return (40, 60);
                case PartsEnum.train_50_test_50:
                    return (50, 50);
                case PartsEnum.train_60_test_40:
                    return (60, 40);
                case PartsEnum.train_70_test_30:
                    return (70, 30);
                default:
                    throw new Exception("Unexpected value");
            }
        }
    }
}
