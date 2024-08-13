# UniWrangler

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![Unity Version](https://img.shields.io/badge/Unity-2019.3%2B-blue.svg)](https://unity.com/releases/2019-lts)
[![Version](https://img.shields.io/badge/Version-1.0.0-brightgreen.svg)](https://github.com/netcatvr/UniWrangler/releases)

![logo](https://data.netcat.moe/UniWranglerLogo600x112.png)

**UniWrangler** is a powerful and customizable Unity Editor Extension that simplifies the creation of PBR materials. </br>
When you select multiple textures, right-click, and choose `Create UniWrangler Material`,
UniWrangler automatically sets up a material with all your PBR textures in place.

![coolgraphicthing](https://data.netcat.moe/uniwrangler_setup.webp)

## 🎨 Features

- **Automated Material Creation**: Quickly create materials with all PBR textures properly assigned.
- **Custom Shader Support**: Easily apply custom shaders to your materials.
- **Fixed Resolution Adjustment**: Automatically resize your textures to a specified resolution.
- **Crunch Compression**: Enable automatic crunch compression to optimize texture memory usage.
- **Exotic Texture Name Support**: Handles non-standard texture names (e.g., `_Diffuse`, `_BaseColor`).
- **Heightmap Setup**: Seamlessly integrate heightmaps into your materials.
- **Normal Map Detection**: Automatically detect and correctly set up DirectX and OpenGL normal maps.

## 🚀 Getting Started

### Installation

1. **Download the UniWrangler Package**: [UniWrangler v1.0.0](https://github.com/netcatvr/UniWrangler/releases/download/release/UniWrangler_Release_1.0.0.unitypackage)
2. **Import the Package**: In Unity, go to `Assets` > `Import Package` > `Custom Package` and select the `UniWrangler.unitypackage` file.
3. **Enjoy!**: UniWrangler is now ready to use in your Unity project.

### Usage

1. **Select Textures**: In the Unity Editor, select the textures you want to use for your material.
2. **Right-Click**: Right-click on your selected textures.
3. **Create UniWrangler Material**: Choose `Create UniWrangler Material` from the context menu.
4. **Customization**: Customize your material setup using UniWrangler's settings in the Inspector under the netcat section.

## ⚙️ Customization Options

- **Shader Selection**: Choose from a list of shaders, or input your custom shader path.
- **Fixed Resolution**: Set a fixed resolution (e.g., 1024x1024) for all textures.
- **Crunch Compression**: Enable or disable crunch compression.
- **Name Lookup**: Automatically detect and match exotic texture names.
- **Heightmap Support**: Toggle heightmap inclusion.
- **Normal Map Detection**: Automatically detect and configure DirectX/OpenGL normals.

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🙏 Acknowledgments

- Thanks to the VRChat community for their amazing support and resources.
- Special thanks to all contributors who helped/will help to improve UniWrangler

---


