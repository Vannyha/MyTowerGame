using UnityEngine;

namespace Helpers
{
    public static class ShaderHelper
    {
        private const string ColorShaderPropertyName = "_Color";
        private static MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
        
        public static readonly int ColorPropertyID = Shader.PropertyToID(ColorShaderPropertyName);

        public static void SetMaterialsColorProperty(Renderer target, int propertyID, Color value)
        {
            if (target == null)
            {
                return;
            }

            Renderer colorizedPart = target;
            colorizedPart.GetPropertyBlock(materialPropertyBlock);
            materialPropertyBlock.SetColor(propertyID, value);
            colorizedPart.SetPropertyBlock(materialPropertyBlock);
        }
    }
}