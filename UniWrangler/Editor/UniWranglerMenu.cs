using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UWE = UniWrangler.Editor;
using LUT = UniWrangler.UniWranglerLUT;
using System;
using Object = UnityEngine.Object;

public class UniWranglerMenu : Editor
{
    private static bool DEBUG = false;
    public static Texture2D _diffuseT, _metalT, _roughT, _emissionT, _normalT, _heightT, _ambientT;
    private static bool _errorFlag = false;
    private static bool foundFirstNormalFlag = false;
    private static int ProcessedFileLength = 0;
    private static int SuccessfullyProcessedFileCount = 0;

    [MenuItem("Assets/Create UniWrangler Material", false, int.MaxValue)]
    private static void CustomContextMenuEntry()
    {
        foundFirstNormalFlag = false;
        _errorFlag = false;

        ProcessedFileLength = Selection.objects.Length;
        SuccessfullyProcessedFileCount = 0;
        ResetVars();

        foreach (Object o in Selection.objects)
        {
            if (o.GetType() == typeof(Texture2D))
            {
                UniMsgDebug("Currently selected: " + o.name);
                PatternMatch(o);
            }
            else
            {
                UniMsgDebug("Erorr Flag Hit");
                _errorFlag = true;
            }
        }

        if (_errorFlag)
        {
            EditorUtility.DisplayDialog("UniWrangler Error", "Could not create material: no valid assets selected.", "Okay");
            _errorFlag = false;
        }
    }

    public static void PatternMatch(UnityEngine.Object match)
    {
        string m_name = match.name.ToLower();
        string m_fullyQualifiedName = match.name;

        bool _crunch = Convert.ToBoolean(UWE.UniWranglerConstants.GetCruch());
        int _resolution = UWE.UniWranglerConstants.GetResolution();

        if (m_name.Contains("_basecolor") || m_name.Contains("_diffuse") || m_name.Contains("_albedo") || m_name.Contains("_base_color") || m_name.Contains("_color"))
        {
            _diffuseT = (Texture2D)match;
            if (_diffuseT != null)
            {
                string path = AssetDatabase.GetAssetPath(_diffuseT);
               
                ProcessTexture(path, true);
            } else
            {
                UniMsg("Error, Could not find valid Diffuse/Basecolor map");
            }
        }

        if (m_name.Contains("_metallic") || m_name.Contains("_metal"))
        {
            _metalT = (Texture2D)match;

            if (_metalT != null)
            {
                string path = AssetDatabase.GetAssetPath(_metalT);
                ProcessTexture(path, false);
            } else
            {
                UniMsg("Error, Could not find valid Metallic map");
                return;
            }
        }

        if (m_name.Contains("_roughness") || m_name.Contains("_rough"))
        {
            _roughT = (Texture2D)match;

            if (_roughT != null)
            {
                string path = AssetDatabase.GetAssetPath(_roughT);
                ProcessTexture(path, false);
            } else
            {
                UniMsg("Error, Could not find valid Diffuse/Basecolor map this is probably not what you want.");
            }
        }

        if (m_name.Contains("_emission") || m_name.Contains("_emissive"))
        {
            _emissionT = (Texture2D)match;

            if (_emissionT != null)
            {
                string path = AssetDatabase.GetAssetPath(_emissionT);
                ProcessTexture(path, true);
            }
            else
            {
                UniMsg("Error, Could not find valid Emission map, skipping.");
            }
        }

        if (m_name.Contains("_bump") || m_name.Contains("_normal") && foundFirstNormalFlag == false)
        {
            //Get Normal Mode
            string NormalMode = UWE.UniWranglerConstants.GetNormalType();
            UniMsgDebug(NormalMode);

            if (UWE.UniWranglerConstants.GetNormalDetection() == 1)
            {
                if(NormalMode == "OpenGL")
                {
                    if (m_name.Contains("_normal") || m_name.Contains("_normal_opengl"))
                    {
                        _normalT = (Texture2D)match;
                        if (_normalT != null)
                        {
                            foundFirstNormalFlag = true;
                            string path = AssetDatabase.GetAssetPath(_normalT);
                            ProcessTexture(path, false, true);
                        }
                    }
                } else if(NormalMode == "DirectX")
                {
                    if (m_name.Contains("_normal") && m_name.Contains("_directx"))
                    {
                        _normalT = (Texture2D)match;
                        if (_normalT != null)
                        {
                            foundFirstNormalFlag = true;
                            string path = AssetDatabase.GetAssetPath(_normalT);
                            ProcessTexture(path, false, true);
                        }
                    }
                } else
                {
                    UniMsg("Could not find Normalmap in specific format. Try disabling Auto Normal Detection!");
                }

            } else
            {
                //Just assume the first normal map that starts with _normal is the right one.
                _normalT = (Texture2D)match;

                if (_normalT != null)
                {
                    foundFirstNormalFlag = true;
                    string path = AssetDatabase.GetAssetPath(_normalT);
                    ProcessTexture(path, false, true);
                }
                else
                {
                    UniMsg("Could not find normal map.");
                }
            }
        }

        if (m_name.Contains("_height") && UWE.UniWranglerConstants.GetIsHeightmapAllowed() == 1)
        {
            _heightT = (Texture2D)match;

            if (_heightT != null)
            {
                string path = AssetDatabase.GetAssetPath(_heightT);
                //Automatically convert to Height Map with no Colordata (RGBA(0-1) => R(0-1))
                ProcessTexture(path, false);
            } else
            {
                UniMsg("Could not find Heightmap, Skipping.");
            }
        }

        if (m_name.Contains("_ao") || m_name.Contains("_mixed_ao") || m_name.Contains("_ambientocclusion"))
        {
            _ambientT = (Texture2D)match;

            if (_ambientT != null)
            {
                string path = AssetDatabase.GetAssetPath(_ambientT);
                ProcessTexture(path, false);
            } else
            {
                UniMsg("Could not find AO Map, Skipping!");
            }
        }

        SuccessfullyProcessedFileCount++;
        UniMsg($"Successfully processed: {SuccessfullyProcessedFileCount} / {ProcessedFileLength} Items.");

        //Ensure all maps are properly processed, before creating the material.
        if (SuccessfullyProcessedFileCount >= ProcessedFileLength)
        {
            string[] ResolvedSuffix = m_fullyQualifiedName.Split('_');
            CreateMaterial(ResolvedSuffix[0]);
        }
    }

    public static void UniMsg(string Message)
    {
        Debug.Log($"{UWE.UniWranglerConstants.PREFIX} {Message}");
    }

    public static void UniMsgDebug(string Message)
    {
        if(DEBUG) Debug.Log($"{UWE.UniWranglerConstants.PREFIX} {Message}");

    }

    public static void DebugDump(string Map, UnityEngine.Object o)
    {
        if (DEBUG)
        {
            UniMsg($"Assigned {Map} to {o.name}");
        }
    }

    public static void ResetVars()
    {
        _diffuseT = null;
        _metalT = null;
        _roughT = null;
        _emissionT = null;
        _normalT = null;
        _heightT = null;
        _ambientT = null;
    }

    public static void ProcessTexture(string path, bool isSRGB, bool isNormalMap = false)
    {
        if (UWE.UniWranglerConstants.GetAutomaticTextureTypeSetup() == 1)
        {
            bool _crunch = Convert.ToBoolean(UWE.UniWranglerConstants.GetCruch());
            int _resolution = UWE.UniWranglerConstants.GetResolution();

            TextureImporter importer = (TextureImporter)TextureImporter.GetAtPath(path);
            if (!isNormalMap) importer.sRGBTexture = isSRGB;
            importer.maxTextureSize = _resolution;
            importer.crunchedCompression = _crunch;
            importer.SaveAndReimport();
        }
    }

    public static void CreateMaterial(string suffix)
    {
        UniMsg($"Creating Material with suffix {suffix}");

        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        Material mat = new Material(Shader.Find(UWE.UniWranglerConstants.GetShaderPath()));
        string savePath = path.Substring(0, path.LastIndexOf('/') + 1);

        string newAssetName = savePath + suffix + ".mat";
        AssetDatabase.CreateAsset(mat, newAssetName);

        if (_diffuseT != null)mat.SetTexture(LUT.StandardLUT[(int)LUT.PBRTYPE.BASE_COLOR], _diffuseT);
        if (_metalT != null) mat.SetTexture(LUT.StandardLUT[(int)LUT.PBRTYPE.METALLIC], _metalT);
        if (_roughT != null) mat.SetTexture(LUT.StandardLUT[(int)LUT.PBRTYPE.ROUGHNESS], _roughT);
        if (_emissionT != null) mat.SetTexture(LUT.StandardLUT[(int)LUT.PBRTYPE.EMISSION], _emissionT);
        if (_normalT != null) mat.SetTexture(LUT.StandardLUT[(int)LUT.PBRTYPE.NORMAL], _normalT);
        if (_heightT != null) mat.SetTexture(LUT.StandardLUT[(int)LUT.PBRTYPE.HEIGHT], _heightT);
        if (_ambientT != null) mat.SetTexture(LUT.StandardLUT[(int)LUT.PBRTYPE.AO], _ambientT);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}

