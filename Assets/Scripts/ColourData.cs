using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarterAssets
{
    [System.Serializable]
    public struct ColourData
    {
        public string Name;
        public Color Colour;

        public ColourData(string name, Color _colour)
        {
            Name = name;
            Colour = _colour;
        }
        public void SetColour(Color _colour)
        {
            Colour = _colour;
        }
        public void SetName(string _name)
        {
            Name = _name;
        }

    }
}
