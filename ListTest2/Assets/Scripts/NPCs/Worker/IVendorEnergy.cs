using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    interface IVendorEnergy : IVendor
{
    NpcEnergy energy { get; set; }

}
