#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace UMotionEditor
{
    public static class VersionCompatibilityUtility
    {
        //********************************************************************************
        // Public Properties
        //********************************************************************************

        public enum EditorPlatform
        {
            Windows = 0,
            Mac,
            Linux,
            Invalid
        }

        public static EditorPlatform CurrentEditorPlatform
        {
            get
            {
                switch (Application.platform)
                {
                    case RuntimePlatform.WindowsEditor:
                        return EditorPlatform.Windows;

                    case RuntimePlatform.OSXEditor:
                        return EditorPlatform.Mac;

                    #if UNITY_5_5_OR_NEWER
                    case RuntimePlatform.LinuxEditor:
                        return EditorPlatform.Linux;
                    #endif

                    default:
                        return EditorPlatform.Invalid;
                }
            }
        }

        public static bool Unity5_4_OrNewer
        {
            get
            {
                #if UNITY_5_4_OR_NEWER
                return true;
                #else
                return false;
                #endif
            }
        }

        public static bool Unity2017_1_OrNewer
        {
            get
            {
                #if UNITY_2017_1_OR_NEWER
                return true;
                #else
                return false;
                #endif
            }
        }

        public static bool Unity2017_3_OrNewer
        {
            get
            {
                #if UNITY_2017_3_OR_NEWER
                return true;
                #else
                return false;
                #endif
            }
        }

        public static bool Unity2018_1_OrNewer
        {
            get
            {
                #if UNITY_2018_1_OR_NEWER
                return true;
                #else
                return false;
                #endif
            }
        }

        public static bool HasDepthSortingBug
        {
            get
            {
                #if UNITY_2017_3 || UNITY_2017_4
                return true;
                #else
                return false;
                #endif
            }
        }

        public static bool UsesScriptableRenderPipeline
        {
            get
            {
                #if UNITY_2018_1_OR_NEWER
                return (UnityEngine.Experimental.Rendering.RenderPipelineManager.currentPipeline != null);
                #else
                return false;
                #endif
            }
        }

        public static string GetCurrentAssemblyName()
        {
            return Assembly.GetExecutingAssembly().GetName().Name;
        }

        //********************************************************************************
        // Private Properties
        //********************************************************************************

        //********************************************************************************
        // Public Methods
        //********************************************************************************

        //********************************************************************************
        // Private Methods
        //********************************************************************************
    }
}
#endif