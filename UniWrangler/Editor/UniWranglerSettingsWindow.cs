using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEngine.EventSystems;
using UWE = UniWrangler.Editor;

namespace UniWrangler.Editor
{
    public class UniWranglerSettingsWindow : EditorWindow
    {
        private static Vector2 MaxWindowSize = new Vector2(350, 275);
        TextField ShaderSelector;

        public Label CompressorDisplay;
        public int _compressorQualityValue;


        [MenuItem("Netcat/UniWrangler/Settings")]
        public static void ShowUniWranglerSettings()
        {
            UniWranglerSettingsWindow wnd = GetWindow<UniWranglerSettingsWindow>();
            wnd.titleContent = new GUIContent("UniWrangler Settings");
            wnd.maxSize = MaxWindowSize;
            wnd.minSize = MaxWindowSize;
        }

        public void CreateGUI()
        {
            VisualElement root = rootVisualElement;

            Label wInfo = new Label("Shader Settings:");
            wInfo.style.marginTop = 10;
            wInfo.style.unityFontStyleAndWeight = FontStyle.Bold;
            //Shader Selector

            //Populate Shader Selector:
            var choices = new List<string>();
            foreach (ShaderInfo s in ShaderUtil.GetAllShaderInfo())
            {
                choices.Add(s.name);
            }

            var ShaderSelector = new PopupField<string>("Select Shader", choices, 0);
            ShaderSelector.value = UniWrangler.Editor.UniWranglerConstants.GetShaderPath();

            ShaderSelector.RegisterCallback<ChangeEvent<string>>((evt) =>
            {
                UniWrangler.Editor.UniWranglerConstants.SetShaderPath(ShaderSelector.value);
                ShaderSelector.value = evt.newValue;
            });

            var resolutionList = new List<int> { 128, 256, 512, 1024, 2048, 4096 };
            var Resolution = new PopupField<int>("Texture Resolution", resolutionList, 0);
            Resolution.value = UWE.UniWranglerConstants.GetResolution();

            Resolution.RegisterCallback<ChangeEvent<int>>((evt) =>
            {
                Resolution.value = evt.newValue;
                UWE.UniWranglerConstants.SetResolution(evt.newValue);
            });

            var PreferedNormalMapFormatList = new List<string> { "OpenGL", "DirectX" };
            var PreferedNormalMapFormat = new PopupField<string>("Normalmap Format", PreferedNormalMapFormatList, 0);
            PreferedNormalMapFormat.value = UWE.UniWranglerConstants.GetNormalType();

            PreferedNormalMapFormat.RegisterCallback<ChangeEvent<string>>((evt) =>
            {
                PreferedNormalMapFormat.value = evt.newValue;
                UWE.UniWranglerConstants.SetNormalType(evt.newValue);
            });


            Toggle AllowExoticToggle = new Toggle();
            AllowExoticToggle.name = "AllowExoticSearchPatterns";

            AllowExoticToggle.label = "Exotic Search Patterns ";
            AllowExoticToggle.value = Convert.ToBoolean(UniWranglerConstants.GetSearchMode());
            AllowExoticToggle.RegisterValueChangedCallback(
                x => UniWranglerConstants.SetSearchMode(Convert.ToInt32(AllowExoticToggle.value))
            );

            Label wInfo2 = new Label("Automatic Import Settings:");
            wInfo2.style.marginTop = 10;
            wInfo2.style.unityFontStyleAndWeight = FontStyle.Bold;

            Toggle AutomaticTextureTypeSetup = new Toggle();
            AutomaticTextureTypeSetup.name = "AutomaticTextureSetup";
            AutomaticTextureTypeSetup.label = "Enable Automatic Setup ";
            AutomaticTextureTypeSetup.style.marginTop = 3;
            AutomaticTextureTypeSetup.value = Convert.ToBoolean(UniWranglerConstants.GetAutomaticTextureTypeSetup());
            AutomaticTextureTypeSetup.RegisterValueChangedCallback(
                x => UniWranglerConstants.SetAutomaticTextureTypeSetup(Convert.ToInt32(AutomaticTextureTypeSetup.value))
            );

            Toggle AllowHeightmap = new Toggle();
            AllowHeightmap.name = "AllowHeightmap";
            AllowHeightmap.label = "Allow Heightmap";
            AllowHeightmap.value = Convert.ToBoolean(UWE.UniWranglerConstants.GetIsHeightmapAllowed());
            AllowHeightmap.RegisterValueChangedCallback(
                x => UniWranglerConstants.SetIsHeightmapAllowed(Convert.ToInt32(AllowHeightmap.value))
            );

            Toggle AllowNormalTypeDetection = new Toggle();
            AllowNormalTypeDetection.name = "Allow Normal Detection";
            AllowNormalTypeDetection.label = "Allow Normal Detection";
            AllowNormalTypeDetection.value = Convert.ToBoolean(UWE.UniWranglerConstants.GetNormalDetection());
            AllowNormalTypeDetection.RegisterValueChangedCallback(
                x => UniWranglerConstants.SetNormalDetection(Convert.ToInt32(AllowNormalTypeDetection.value))
            );

            Toggle CrunchCompress = new Toggle();
            CrunchCompress.name = "Crunch Compression";
            CrunchCompress.label = "Crunch Compression";
            CrunchCompress.value = Convert.ToBoolean(UniWranglerConstants.GetCruch());
            CrunchCompress.RegisterValueChangedCallback(
                x => UniWranglerConstants.SetCrunch(Convert.ToInt32(CrunchCompress.value))
            );

            var CompressorQuality = new Slider("Compressor Quality", 0, 100);

            CompressorQuality.label = "Compressor Quality (" + UWE.UniWranglerConstants.GetCompressorValue() + "%)";
            CompressorQuality.value = UWE.UniWranglerConstants.GetCompressorValue();

            CompressorQuality.RegisterCallback<ChangeEvent<float>>((evt) =>
            {
                ///TODO make value save
                CompressorQuality.value = evt.newValue;
                UWE.UniWranglerConstants.SetCompressorValue((int)evt.newValue);
                CompressorQuality.label = "Compressor Quality ("+(int)evt.newValue + "%)";
            });

            //Initialize at least once
            CompressorDisplay = new Label();
            CompressorDisplay.text = "" + (int)CompressorQuality.value + "%";

            Label Credits = new Label($"UniWrangler v{UWE.UniWranglerConstants.VERSION} made with ♥ by Netcat.");

            Credits.style.paddingTop = MaxWindowSize.x/12;
            Credits.style.color = Color.cyan;
            Credits.style.alignSelf = Align.Center;

            root.Add(wInfo);
            root.Add(ShaderSelector);
            root.Add(AllowExoticToggle);
            root.Add(wInfo2);
            root.Add(AutomaticTextureTypeSetup);
            root.Add(AllowNormalTypeDetection);
            root.Add(AllowHeightmap);
            root.Add(CrunchCompress);
            root.Add(Resolution);
            root.Add(PreferedNormalMapFormat);
            root.Add(CompressorQuality);
            root.Add(Credits);
        }
    }
}