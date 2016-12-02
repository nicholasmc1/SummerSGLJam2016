using UnityEngine;
using System.Collections;
using UnityEditor;

public class TexImporter : AssetPostprocessor
{
    void OnPreprocessTexture()
    {

        if (assetImporter.userData != "Asset Post Proccesed")
        {
            TextureImporter importer = assetImporter as TextureImporter;
            string path = importer.assetPath.ToLower();
            if (!path.Contains("extentions"))
            {
                if (path.Contains("assets/textures"))
                {
                    if (path.Contains("_nm"))
                    {
                        importer.normalmap = true;
                        importer.textureType = TextureImporterType.NormalMap;
                    }
                }

                if (path.Contains("assets/ui/textures"))
                    importer.textureType = TextureImporterType.Sprite;


                assetImporter.userData = "Asset Post Proccesed";
            }
        }
    }
}
