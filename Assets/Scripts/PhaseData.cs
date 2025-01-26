using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PhaseData;

public static class PhaseData
{
    public struct Phase
    {
        public List<MailType.Colors> MailSlots;
        public List<PhaseTruck> TruckSlots;

        public Phase
            (
                List<MailType.Colors> mailSlots,
                List<PhaseTruck> truckSlots
            )
        {
            MailSlots = mailSlots;
            TruckSlots = truckSlots;
        }
    }

    public struct PhaseTruck
    {
        public MailType.Colors Color;
        public int MailNeeded;
        public float TimeAllowed;

        public PhaseTruck(MailType.Colors color, int mailNeeded, float timeAllowed)
        {
            Color = color;
            MailNeeded = mailNeeded;
            TimeAllowed = timeAllowed;
        }

        public PhaseTruck(MailType.Colors color)
        {
            Color = color;
            MailNeeded = 0;
            TimeAllowed = 0;
        }
    }


    //,
    //    new Phase( // 0
    //        new List<MailType.Colors>
    //        {
    //            MailType.Colors.None,
    //            MailType.Colors.None,
    //            MailType.Colors.None,
    //        },
    //        new List<PhaseTruck>
    //        {
    //            new PhaseTruck(MailType.Colors.None),
    //            new PhaseTruck(MailType.Colors.None),
    //            new PhaseTruck(MailType.Colors.None),
    //            new PhaseTruck(MailType.Colors.None)
    //        }
    //    )



    public static List<Phase> Phases = new List<Phase>()
    {
        new Phase( // 1
            new List<MailType.Colors>
            {
                MailType.Colors.Yellow,
                MailType.Colors.Blue,
                MailType.Colors.Red,
            },
            new List<PhaseTruck>
            {
                new PhaseTruck(MailType.Colors.None),
                new PhaseTruck(MailType.Colors.None),
                new PhaseTruck(MailType.Colors.None),
                new PhaseTruck(MailType.Colors.Yellow, 5, 10f)
            }
        ),
        new Phase( // 2
            new List<MailType.Colors>
            {
                MailType.Colors.None,
                MailType.Colors.Yellow,
                MailType.Colors.None,
            },
            new List<PhaseTruck>
            {
                new PhaseTruck(MailType.Colors.Yellow, 5, 10f),
                new PhaseTruck(MailType.Colors.None),
                new PhaseTruck(MailType.Colors.None),
                new PhaseTruck(MailType.Colors.None)
            }
        ),
        new Phase( // 3
            new List<MailType.Colors>
            {
                MailType.Colors.Red,
                MailType.Colors.None,
                MailType.Colors.None,
            },
            new List<PhaseTruck>
            {
                new PhaseTruck(MailType.Colors.None),
                new PhaseTruck(MailType.Colors.None),
                new PhaseTruck(MailType.Colors.Red, 5, 10f),
                new PhaseTruck(MailType.Colors.None)
            }
        ),
        new Phase( // 4
            new List<MailType.Colors>
            {
                MailType.Colors.None,
                MailType.Colors.None,
                MailType.Colors.Blue,
            },
            new List<PhaseTruck>
            {
                new PhaseTruck(MailType.Colors.None),
                new PhaseTruck(MailType.Colors.None),
                new PhaseTruck(MailType.Colors.Blue, 5, 10f),
                new PhaseTruck(MailType.Colors.None)
            }
        ),
        new Phase( // 5
            new List<MailType.Colors>
            {
                MailType.Colors.Yellow,
                MailType.Colors.None,
                MailType.Colors.Red,
            },
            new List<PhaseTruck>
            {
                new PhaseTruck(MailType.Colors.Yellow, 5, 10f),
                new PhaseTruck(MailType.Colors.None),
                new PhaseTruck(MailType.Colors.None),
                new PhaseTruck(MailType.Colors.Red, 5, 20f)
            }
        ),
        new Phase( // 6
            new List<MailType.Colors>
            {
                MailType.Colors.None,
                MailType.Colors.Blue,
                MailType.Colors.None,
            },
            new List<PhaseTruck>
            {
                new PhaseTruck(MailType.Colors.None),
                new PhaseTruck(MailType.Colors.Blue, 5, 20f),
                new PhaseTruck(MailType.Colors.None),
                new PhaseTruck(MailType.Colors.Blue, 5, 20f)
            }
        ),
        new Phase( // 7
            new List<MailType.Colors>
            {
                MailType.Colors.Red,
                MailType.Colors.None,
                MailType.Colors.None,
            },
            new List<PhaseTruck>
            {
                new PhaseTruck(MailType.Colors.None),
                new PhaseTruck(MailType.Colors.None),
                new PhaseTruck(MailType.Colors.Red, 10, 20f),
                new PhaseTruck(MailType.Colors.Red, 10, 20f)
            }
        ),
        new Phase( // 8
            new List<MailType.Colors>
            {
                MailType.Colors.Yellow,
                MailType.Colors.None,
                MailType.Colors.Blue,
            },
            new List<PhaseTruck>
            {
                new PhaseTruck(MailType.Colors.Yellow, 5, 10f),
                new PhaseTruck(MailType.Colors.Yellow, 5, 15f),
                new PhaseTruck(MailType.Colors.Blue, 5, 20f),
                new PhaseTruck(MailType.Colors.None)
            }
        ),
        new Phase( // 9
            new List<MailType.Colors>
            {
                MailType.Colors.Red,
                MailType.Colors.Blue,
                MailType.Colors.Yellow,
            },
            new List<PhaseTruck>
            {
                new PhaseTruck(MailType.Colors.Red, 5, 15f),
                new PhaseTruck(MailType.Colors.Blue, 5, 20f),
                new PhaseTruck(MailType.Colors.None),
                new PhaseTruck(MailType.Colors.Yellow, 5, 15f)
            }
        ),
        new Phase( // 10
            new List<MailType.Colors>
            {
                MailType.Colors.Blue,
                MailType.Colors.Red,
                MailType.Colors.Yellow,
            },
            new List<PhaseTruck>
            {
                new PhaseTruck(MailType.Colors.Yellow, 10, 20f),
                new PhaseTruck(MailType.Colors.Red, 10, 20f),
                new PhaseTruck(MailType.Colors.Blue, 10, 20f),
                new PhaseTruck(MailType.Colors.Yellow, 10, 20f)
            }
        )
    };
}