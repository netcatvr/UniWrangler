using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniWrangler
{
    public class UniWranglerLUT
    {
        //_diffuseT,_metalT,_roughT,_emissionT,_normalT,_heightT,_ambientT;

        public enum PBRTYPE : int
        {
            BASE_COLOR = 0,
            METALLIC = 1,
            ROUGHNESS = 2,
            EMISSION = 3,
            NORMAL = 4,
            HEIGHT = 5,
            AO = 6
        }

        //Due to Unitys horrible naming schemes, defined as a LUT.
        public static string[] StandardLUT = {
            "_MainTex",
            "_MetallicGlossMap",
            "_SpecGlossMap",
            "_EmissionMap",
            "_BumpMap",
            "_ParallaxMap",
            "_OcclusionMap"
        };


    }
}