using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

/**
 * Build settings
 */ 
public class BuildConfiger
{
	/**
	 * Compress bundles
	 */ 
	public static bool Compress
	{
		get{return BMDataAccessor.BMConfiger.compress;}
		set
		{
			if(BMDataAccessor.BMConfiger.compress != value)
			{
				BMDataAccessor.BMConfiger.compress = value;
				BundleManager.UpdateAllBundleChangeTime();
			}
		}
	}
	
	/**
	 * Build deterministic Bundles
	 */ 
	public static bool DeterministicBundle
	{
		get{return BMDataAccessor.BMConfiger.deterministicBundle;}
		set
		{
			if(BMDataAccessor.BMConfiger.deterministicBundle != value)
			{
				BMDataAccessor.BMConfiger.deterministicBundle = value;
				BundleManager.UpdateAllBundleChangeTime();
			}
		}
	}
	
	/** 
	 * Target platform
	 */ 
	public static BuildPlatform BundleBuildTarget
	{
		get
		{
			return BMDataAccessor.Urls.bundleTarget;
		}
		set
		{
			BMDataAccessor.Urls.bundleTarget = value;
		}
	}

	/** 
	 * Target platform
	 */
	public static bool UseEditorTarget
	{
		get
		{
			return BMDataAccessor.Urls.useEditorTarget;
		}
		set
		{
			BMDataAccessor.Urls.useEditorTarget = value;
		}
	}
	
	/**
	 * Bundle file's suffix
	 */ 
	public static string BundleSuffix
	{
		get{return BMDataAccessor.BMConfiger.bundleSuffix;}
		set{BMDataAccessor.BMConfiger.bundleSuffix = value;}
	}
	 
	/**
	 * Current output string for target platform
	 */
	public static string BuildOutputStr
	{
		get
		{
			return BMDataAccessor.Urls.outputs[BMDataAccessor.Urls.bundleTarget.ToString()];
		}
		set
		{
			var urls = BMDataAccessor.Urls.outputs;
			string platformStr = BMDataAccessor.Urls.bundleTarget.ToString();
			string origValue = urls[platformStr];
			urls[platformStr] = value;
			if(origValue != value)
				BMDataAccessor.SaveUrls();
		}
	}
	 
	internal static string InterpretedOutputPath
	{
		get
		{
			return BMDataAccessor.Urls.GetInterpretedOutputPath(BMDataAccessor.Urls.bundleTarget);
		}
	}
	
	internal static BuildOptions BuildOptions
	{
		get
		{
			return BMDataAccessor.BMConfiger.compress ? 0 : BuildOptions.UncompressedAssetBundle;
		}
	}
	
	internal static BuildTarget UnityBuildTarget
	{
		get
		{
			if(BuildConfiger.UseEditorTarget)
				BuildConfiger.UnityBuildTarget = EditorUserBuildSettings.activeBuildTarget;

			switch(BundleBuildTarget)
			{
			case BuildPlatform.Standalones:
				if(Application.platform == RuntimePlatform.OSXEditor)
					return BuildTarget.StandaloneOSXIntel;
				else
					return BuildTarget.StandaloneWindows;
			case BuildPlatform.WebPlayer:
				return BuildTarget.WebPlayer;
			case BuildPlatform.IOS:
				return BuildTarget.iPhone;
			case BuildPlatform.Android:
				return BuildTarget.Android;
			default:
				Debug.LogError("Internal error. Cannot find BuildTarget for " + BundleBuildTarget);
				return BuildTarget.StandaloneWindows;
			}
		}
		set
		{
			switch(value)
			{
			case BuildTarget.StandaloneGLESEmu:
			case BuildTarget.StandaloneLinux:
			case BuildTarget.StandaloneLinux64:
			case BuildTarget.StandaloneLinuxUniversal:
			case BuildTarget.StandaloneOSXIntel:
			case BuildTarget.StandaloneWindows:
			case BuildTarget.StandaloneWindows64:
				BundleBuildTarget = BuildPlatform.Standalones;
				break;
			case BuildTarget.WebPlayer:
			case BuildTarget.WebPlayerStreamed:
				BundleBuildTarget = BuildPlatform.WebPlayer;
				break;
			case BuildTarget.iPhone:
				BundleBuildTarget = BuildPlatform.IOS;
				break;
			case BuildTarget.Android:
				BundleBuildTarget = BuildPlatform.Android;
				break;
			default:
				Debug.LogError("Internal error. Bundle Manager dosn't support for platform " + value);
				BundleBuildTarget = BuildPlatform.Standalones;
				break;
			}
		}
	}
}