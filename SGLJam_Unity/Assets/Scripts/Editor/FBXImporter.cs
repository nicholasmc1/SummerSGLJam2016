using UnityEngine;
using System.Collections;
using UnityEditor;

public class FBXImporter : AssetPostprocessor
{
    void OnPreprocessModel()
    {
        if (assetImporter.userData != "Asset Post Proccesed")
        {
            ModelImporter importer = assetImporter as ModelImporter;
            string path = importer.assetPath.ToLower();
            if (!path.Contains("extensions"))//applies to all meshes I  made.
            {
                importer.materialName = ModelImporterMaterialName.BasedOnMaterialName;
                importer.materialSearch = ModelImporterMaterialSearch.Everywhere;
                importer.importBlendShapes = false;
                if (path.Contains("environment") || path.Contains("props"))
                {
                    importer.animationType = ModelImporterAnimationType.None;
                    importer.importAnimation = false;
                }
            }
            
            
            assetImporter.userData = "Asset Post Proccesed";
        }
    }
}