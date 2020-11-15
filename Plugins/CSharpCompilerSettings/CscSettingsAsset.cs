using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using LVersion = Coffee.CSharpCompilerSettings.CSharpLanguageVersion;

namespace Coffee.CSharpCompilerSettings
{
    internal enum Nullable
    {
        Disable,
        Enable,
        Warnings,
        Annotations,
    }

    internal enum CompilerType
    {
        BuiltIn,
        CustomPackage
    }

    [Serializable]
    public struct NugetPackage
    {
        public enum CategoryType
        {
            Compiler,
            Analyzer,
        }

        [SerializeField] private string m_Name;
        [SerializeField] private string m_Version;
        [SerializeField] private CategoryType m_Category;

        public string PackageId
        {
            get { return m_Name + "." + m_Version; }
        }

        public string Name
        {
            get { return m_Name; }
        }

        public string Version
        {
            get { return m_Version; }
        }

        public CategoryType Category
        {
            get { return m_Category; }
        }

        public NugetPackage(string name, string version, CategoryType category = CategoryType.Compiler)
        {
            m_Name = name;
            m_Version = version;
            m_Category = category;
        }

        public override string ToString()
        {
            return PackageId;
        }
    }

    [Serializable]
    public struct AnalyzerFilter
    {
        [Tooltip("Include predefined assemblies (e.g. Assembly-CSharp.dll)")]
        [SerializeField] private bool m_PredefinedAssemblies;

        [Tooltip("Include assemblies filter. Separate multiple values with ';'. Prefix '!' to exclude (e.g. 'Assets/;!Packages/')")]
        [Split(';')]
        [SerializeField] private string m_IncludedAssemblies;

        public AnalyzerFilter(bool predefinedAssemblies, string includedAssemblies)
        {
            m_PredefinedAssemblies = predefinedAssemblies;
            m_IncludedAssemblies = includedAssemblies;
        }
    }

    public class SplitAttribute : PropertyAttribute
    {
        public char separater { get; }

        public SplitAttribute(char separater)
        {
            this.separater = separater;
        }
    }

    internal class CscSettingsAsset : ScriptableObject, ISerializationCallbackReceiver
    {
        public const string k_SettingsPath = "ProjectSettings/CSharpCompilerSettings.asset";

        [SerializeField] private bool m_UseDefaultCompiler = true;
        [SerializeField] private int m_Version = 0;
        [SerializeField] private CompilerType m_CompilerType = CompilerType.BuiltIn;
        [SerializeField] private string m_PackageName = "Microsoft.Net.Compilers";
        [SerializeField] private string m_PackageVersion = "3.5.0";
        [SerializeField] private CSharpLanguageVersion m_LanguageVersion = CSharpLanguageVersion.Latest;
        [SerializeField] private bool m_EnableLogging = false;
        [SerializeField] private bool m_EnableNullable = false;
        [SerializeField] private Nullable m_Nullable = Nullable.Disable;
        [SerializeField] private NugetPackage m_CompilerPackage = new NugetPackage("Microsoft.Net.Compilers", "3.5.0", NugetPackage.CategoryType.Compiler);
        [SerializeField] private NugetPackage[] m_AnalyzerPackages = new NugetPackage[0];
        [SerializeField] private AnalyzerFilter m_AnalyzerFilter = new AnalyzerFilter(true, "Assets/;!Packages/");

        [Tooltip(
            "When compiling this assembly, add or remove specific symbols separated with semicolons (;) or commas (,).\nSymbols starting with '!' will be removed.\n\ne.g. 'SYMBOL_TO_ADD;!SYMBOL_TO_REMOVE;...'")]
        [SerializeField]
        [Split(';')]
        private string m_ModifySymbols = "";

        private static CscSettingsAsset CreateFromProjectSettings()
        {
            s_Instance = CreateInstance<CscSettingsAsset>();
            if (File.Exists(k_SettingsPath))
                JsonUtility.FromJsonOverwrite(File.ReadAllText(k_SettingsPath), s_Instance);
            return s_Instance;
        }

        private static CscSettingsAsset s_Instance;
        public static CscSettingsAsset instance => s_Instance ? s_Instance : s_Instance = CreateFromProjectSettings();
        public NugetPackage CompilerPackage => m_CompilerPackage;
        public bool UseDefaultCompiler => m_CompilerType == CompilerType.BuiltIn;
        public bool ShouldToRecompile => m_CompilerType == CompilerType.CustomPackage || !string.IsNullOrEmpty(m_ModifySymbols);
        public Nullable Nullable => m_Nullable;
        public NugetPackage[] AnalyzerPackages => m_AnalyzerPackages;
        public AnalyzerFilter AnalyzerFilter => m_AnalyzerFilter;

        public string LanguageVersion
        {
            get
            {
                switch (m_LanguageVersion)
                {
                    case CSharpLanguageVersion.CSharp7: return "7";
                    case CSharpLanguageVersion.CSharp7_1: return "7.1";
                    case CSharpLanguageVersion.CSharp7_2: return "7.2";
                    case CSharpLanguageVersion.CSharp7_3: return "7.3";
                    case CSharpLanguageVersion.CSharp8: return "8.0";
                    case CSharpLanguageVersion.CSharp9: return "9.0";
                    case CSharpLanguageVersion.Preview: return "preview";
                    default: return "latest";
                }
            }
        }

        public bool EnableDebugLog => m_EnableLogging;

        public string AdditionalSymbols
        {
            get
            {
                var sb = new StringBuilder();
                if (!UseDefaultCompiler)
                {
                    var v = m_LanguageVersion;
                    if (v == LVersion.Preview) v = LVersion.CSharp9;
                    if (v == LVersion.Latest) v = LVersion.CSharp8;

                    sb.Append(LVersion.CSharp7 <= v ? "CSHARP_7_OR_NEWER;" : "!CSHARP_7_OR_NEWER;");
                    sb.Append(LVersion.CSharp7_1 <= v ? "CSHARP_7_1_OR_NEWER;" : "!CSHARP_7_1_OR_NEWER;");
                    sb.Append(LVersion.CSharp7_2 <= v ? "CSHARP_7_2_OR_NEWER;" : "!CSHARP_7_2_OR_NEWER;");
                    sb.Append(LVersion.CSharp7_3 <= v ? "CSHARP_7_3_OR_NEWER;" : "!CSHARP_7_3_OR_NEWER;");
                    sb.Append(LVersion.CSharp8 <= v ? "CSHARP_8_OR_NEWER;" : "!CSHARP_8_OR_NEWER;");
                    sb.Append(LVersion.CSharp9 <= v ? "CSHARP_9_OR_NEWER;" : "!CSHARP_9_OR_NEWER;");
                }

                sb.Append(m_ModifySymbols);

                return sb.ToString();
            }
        }

        public static CscSettingsAsset GetAtPath(string path)
        {
            try
            {
                return string.IsNullOrEmpty(Core.GetPortableDllPath(path))
                    ? null
                    : CreateFromJson(AssetImporter.GetAtPath(path).userData);
            }
            catch
            {
                return null;
            }
        }

        public static CscSettingsAsset CreateFromJson(string json = "")
        {
            var setting = CreateInstance<CscSettingsAsset>();
            JsonUtility.FromJsonOverwrite(json, setting);
            return setting;
        }

        public void OnBeforeSerialize()
        {
            m_UseDefaultCompiler = m_CompilerType == CompilerType.BuiltIn;
            m_EnableNullable = m_Nullable != Nullable.Disable;
            m_PackageName = m_CompilerPackage.Name;
            m_PackageVersion = m_CompilerPackage.Version;
        }

        public void OnAfterDeserialize()
        {
            if (m_Version < 110)
            {
                m_Version = 110;
                m_CompilerType = m_UseDefaultCompiler
                    ? CompilerType.BuiltIn
                    : CompilerType.CustomPackage;
            }

            if (m_Version < 130)
            {
                m_Version = 130;
                m_Nullable = m_EnableNullable
                    ? Nullable.Enable
                    : Nullable.Disable;
            }

            if (m_Version < 140)
            {
                m_Version = 140;
                m_CompilerPackage = new NugetPackage(m_PackageName, m_PackageVersion, NugetPackage.CategoryType.Compiler);
            }
        }
    }
}