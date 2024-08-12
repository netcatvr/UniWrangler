using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniWrangler.Editor
{
    public class UniWranglerConstants
    {
        public static string VERSION = "1.0.0";
        public static string PREFIX = "<color=lightblue>[UniWrangler]</color>";


        public static void SetSearchMode(int sMode) => PlayerPrefs.SetInt("UniWrangler_SearchMode", sMode);

        public static int GetSearchMode()
          => PlayerPrefs.HasKey("UniWrangler_SearchMode")
              ? PlayerPrefs.GetInt("UniWrangler_SearchMode")
              : 0;


        public static void SetAutomaticTextureTypeSetup(int sMode) 
            => PlayerPrefs.SetInt("UniWrangler_AutoSetupTypes", sMode);

        public static int GetAutomaticTextureTypeSetup()
          => PlayerPrefs.HasKey("UniWrangler_AutoSetupTypes")
              ? PlayerPrefs.GetInt("UniWrangler_AutoSetupTypes")
              : 0;

        public static void SetResolution(int resolution) 
            => PlayerPrefs.SetInt("UniWrangler_Resolution", resolution);

        public static int GetResolution()
            => PlayerPrefs.HasKey("UniWrangler_Resolution") 
            ? PlayerPrefs.GetInt("UniWrangler_Resolution") 
            : 1024;

        public static void SetCrunch(int state)
            => PlayerPrefs.SetInt("UniWrangler_IsCrunch", state);

        public static int GetCruch()
            => PlayerPrefs.HasKey("UniWrangler_IsCrunch")
            ? PlayerPrefs.GetInt("UniWrangler_IsCrunch")
            : 0;

        public static void SetCompressorValue(int val)
            => PlayerPrefs.SetInt("UniWrangler_CompressorValue", val);

        public static int GetCompressorValue()
            => PlayerPrefs.HasKey("UniWrangler_CompressorValue")
            ? PlayerPrefs.GetInt("UniWrangler_CompressorValue")
            : 50;

        public static void SetNormalType(string val)
            => PlayerPrefs.SetString("UniWrangler_Normal",val);

        public static string GetNormalType()
            => PlayerPrefs.HasKey("UniWrangler_Normal")
            ? PlayerPrefs.GetString("UniWrangler_Normal")
            : "OpenGL";

        public static void SetNormalDetection(int val)
            => PlayerPrefs.SetInt("UniWrangler_NormalDetection", val);

        public static int GetNormalDetection()
            => PlayerPrefs.HasKey("UniWrangler_NormalDetection")
            ? PlayerPrefs.GetInt("UniWrangler_NormalDetection")
            : 0;


        //GetHeightmap
        public static void SetIsHeightmapAllowed(int val)
            => PlayerPrefs.SetInt("UniWrangler_AllowHeightmap", val);

        public static int GetIsHeightmapAllowed()
            => PlayerPrefs.HasKey("UniWrangler_AllowHeightmap")
            ? PlayerPrefs.GetInt("UniWrangler_AllowHeightmap")
            : 0;


        public static string GetShaderPath()
        {
            if (PlayerPrefs.HasKey("UniWrangler_Shader"))
            {
                return PlayerPrefs.GetString("UniWrangler_Shader");
            }
            else
            {
                return "Standard";
            }
        }

        public static void SetShaderPath(string shaderPath)
        {
            PlayerPrefs.SetString("UniWrangler_Shader", shaderPath);
        }

        


    }
}