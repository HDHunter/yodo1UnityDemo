using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.IO;
using System.Linq;
using System.Collections.Generic;
#if UNITY_IOS
using UnityEditor.iOS.Xcode;
#endif
using System.Xml;

namespace Yodo1Ads
{
    public class Yodo1PostProcess
    {
        private static string[] mSKAdNetworkId = new string[]
        {
            "c3frkrj4fj.skadnetwork", "275upjj5gd.skadnetwork", "294l99pt4k.skadnetwork", "2fnua5tdw4.skadnetwork",
            "2u9pt9hc89.skadnetwork", "3rd42ekr43.skadnetwork", "4468km3ulz.skadnetwork", "44jx6755aq.skadnetwork",
            "44n7hlldy6.skadnetwork", "7rz58n8ntl.skadnetwork", "cg4yq2srnc.skadnetwork", "kbmxgpxpgc.skadnetwork",
            "4fzdc2evr5.skadnetwork", "4pfyvq9l8r.skadnetwork", "523jb4fst2.skadnetwork", "5l3tpt7t6e.skadnetwork",
            "5lm9lj6jb7.skadnetwork", "6964rsfnh4.skadnetwork", "6g9af3uyq4.skadnetwork", "74b6s63p6l.skadnetwork",
            "7ug5zh24hu.skadnetwork", "84993kbrcf.skadnetwork", "8s468mfl3y.skadnetwork", "9nlqeag3gk.skadnetwork",
            "9rd848q2bz.skadnetwork", "9t245vhmpl.skadnetwork", "a7xqa6mtl2.skadnetwork", "c6k4g5qg8m.skadnetwork",
            "cj5566h2ga.skadnetwork", "e5fvkxwrpn.skadnetwork", "ejvt5qm6ak.skadnetwork", "g28c52eehv.skadnetwork",
            "g2y4y55b64.skadnetwork", "gta9lk7p23.skadnetwork", "hs6bdukanm.skadnetwork", "kbd757ywx3.skadnetwork",
            "klf5c3l5u5.skadnetwork", "m8dbw4sv7c.skadnetwork", "mlmmfzh3r3.skadnetwork", "mtkv5xtk9e.skadnetwork",
            "n6fk4nfna4.skadnetwork", "n9x2a789qt.skadnetwork", "ppxm28t8ap.skadnetwork", "prcb7njmu6.skadnetwork",
            "pwa73g5rt2.skadnetwork", "v72qych5uu.skadnetwork", "4dzt52r2t5.skadnetwork", "v4nxqhlyqp.skadnetwork",
            "pwdxu55a5a.skadnetwork", "qqp299437r.skadnetwork", "r45fhb6rf7.skadnetwork", "rx5hdcabgc.skadnetwork",
            "t38b2kh725.skadnetwork", "tl55sbb4fm.skadnetwork", "u679fj5vs4.skadnetwork", "uw77j35x4d.skadnetwork",
            "wg4vff78zm.skadnetwork", "wzmmz9fp6w.skadnetwork", "yclnxrl5pm.skadnetwork", "ydx93a7ass.skadnetwork",
            "3qcr597p9d.skadnetwork", "3qy4746246.skadnetwork", "3sh42y64q3.skadnetwork", "424m5254lk.skadnetwork",
            "578prtvx9j.skadnetwork", "5a6flpkh64.skadnetwork", "8c4e2ghe7u.skadnetwork", "av6w8kgt66.skadnetwork",
            "cstr6suwn9.skadnetwork", "f38h382jlk.skadnetwork", "p78axxw29g.skadnetwork", "s39g8k73mm.skadnetwork",
            "zq492l623r.skadnetwork", "24t9a8vw3c.skadnetwork", "32z4fx6l9h.skadnetwork", "54nzkqm89y.skadnetwork",
            "6xzpu9s2p8.skadnetwork", "79pbpufp6p.skadnetwork", "9b89h5y424.skadnetwork", "feyaarzu9v.skadnetwork",
            "ggvn48r87g.skadnetwork", "4w7y6s5ca2.skadnetwork", "mls7yz5dvl.skadnetwork", "v9wttpbfk9.skadnetwork",
            "glqzh8vgby.skadnetwork", "k674qkevps.skadnetwork", "ludvb6z3bs.skadnetwork", "rvh3l7un93.skadnetwork",
            "x8jxxk4ff5.skadnetwork", "xy9t38ct57.skadnetwork", "zmvfpc5aq8.skadnetwork", "22mmun2rn5.skadnetwork",
            "5tjdwbrq8w.skadnetwork", "6p4ks3rnbw.skadnetwork", "737z793b9f.skadnetwork", "97r2b46745.skadnetwork",
            "dzg6xy7pwj.skadnetwork", "f73kdq92p3.skadnetwork", "hdw39hrw9y.skadnetwork", "lr83yxwka7.skadnetwork",
            "mp6xlyr22a.skadnetwork", "s69wq72ugq.skadnetwork", "su67r6k2v3.skadnetwork", "w9q455wk68.skadnetwork",
            "x44k69ngh6.skadnetwork", "x8uqf25wch.skadnetwork", "y45688jllp.skadnetwork", "n38lu8286q.skadnetwork",
            "252b5q8x7y.skadnetwork", "9g2aggbj52.skadnetwork", "b9bk5wbcq9.skadnetwork", "r26jy69rpl.skadnetwork",
            "3l6bd9hu43.skadnetwork", "488r3q3dtq.skadnetwork", "52fl2v3hgk.skadnetwork", "6v7lgmsu45.skadnetwork",
            "89z7zv988g.skadnetwork", "ecpz2srf59.skadnetwork", "7953jerfzd.skadnetwork", "f7s53z58qe.skadnetwork",
            "8m87ys6875.skadnetwork", "bxvub5ada5.skadnetwork", "hb56zgv37p.skadnetwork", "m297p6643m.skadnetwork",
            "m5mvw97r93.skadnetwork", "vcra2ehyfk.skadnetwork", "238da6jt44.skadnetwork", "9yg77x724h.skadnetwork",
            "gvmwg8q7h5.skadnetwork", "n66cz3y3bx.skadnetwork", "nzq8sh4pbs.skadnetwork", "pu4na253f3.skadnetwork",
            "v79kvwwj4g.skadnetwork", "yrqqpx2mcb.skadnetwork", "z4gj7hsk7h.skadnetwork", "bvpn9ufa9b.skadnetwork",
        };

        [PostProcessBuild()]
        public static void OnPostprocessBuild(BuildTarget buildTarget, string pathToBuiltProject)
        {
            if (buildTarget == BuildTarget.iOS)
            {
#if UNITY_IOS
                Yodo1AdSettings settings = Yodo1AdSettingsSave.Load();
                if (CheckConfiguration_iOS(settings))
                {
                    UpdateIOSPlist(pathToBuiltProject, settings);
                    UpdateIOSProject(pathToBuiltProject);
                }
#endif
            }
        }

        #region iOS Content

#if UNITY_IOS
        public static bool CheckConfiguration_iOS(Yodo1AdSettings settings)
        {
            if (settings == null)
            {
                string message = "MAS iOS settings is null, please check the configuration.";
                Debug.LogError("[Yodo1 Ads] " + message);
                return false;
            }

            if (settings.iOSSettings.GlobalRegion && string.IsNullOrEmpty(settings.iOSSettings.AdmobAppID))
            {
                string message = "MAS iOS AdMob App ID is null, please check the configuration.";
                Debug.LogError("[Yodo1 Ads] " + message);
                return false;
            }

            return true;
        }
        private static void UpdateIOSPlist(string path, Yodo1AdSettings settings)
        {
            string plistPath = Path.Combine(path, "Info.plist");
            PlistDocument plist = new PlistDocument();
            plist.ReadFromString(File.ReadAllText(plistPath));

            //Get Root
            PlistElementDict rootDict = plist.root;
            PlistElementDict transportSecurity = rootDict.CreateDict("NSAppTransportSecurity");
            transportSecurity.SetBoolean("NSAllowsArbitraryLoads", true);

            //Set SKAdNetwork
            PlistElementArray skItem = rootDict.CreateArray("SKAdNetworkItems");
            foreach (string sk in mSKAdNetworkId)
            {
                PlistElementDict skDic = skItem.AddDict();
                skDic.SetString("SKAdNetworkIdentifier", sk);
            }

            //Set AppLovinSdkKey
            rootDict.SetString("AppLovinSdkKey",
                "xcGD2fy-GdmiZQapx_kUSy5SMKyLoXBk8RyB5u9MVv34KetGdbl4XrXvAUFy0Qg9scKyVTI0NM4i_yzdXih4XE");

            //Set AdMob APP Id
            if (settings.iOSSettings.GlobalRegion)
            {
                rootDict.SetString("GADApplicationIdentifier", settings.iOSSettings.AdmobAppID);
            }

            PlistElementString privacy = (PlistElementString) rootDict["NSLocationAlwaysUsageDescription"];
            if (privacy == null)
            {
                rootDict.SetString("NSLocationAlwaysUsageDescription",
                    "Some ad content may require access to the location for an interactive ad experience.");
            }

            PlistElementString privacy1 = (PlistElementString) rootDict["NSLocationWhenInUseUsageDescription"];
            if (privacy1 == null)
            {
                rootDict.SetString("NSLocationWhenInUseUsageDescription",
                    "Some ad content may require access to the location for an interactive ad experience.");
            }

            PlistElementString attPrivacy = (PlistElementString) rootDict["NSUserTrackingUsageDescription"];
            if (attPrivacy == null)
            {
                rootDict.SetString("NSUserTrackingUsageDescription",
                    "This identifier will be used to deliver personalized ads to you.");
            }

            PlistElementString calendarsPrivacy = (PlistElementString) rootDict["NSCalendarsUsageDescription"];
            if (calendarsPrivacy == null)
            {
                rootDict.SetString("NSCalendarsUsageDescription",
                    "The application wants to use your calendar. Is that allowed?");
            }

            File.WriteAllText(plistPath, plist.WriteToString());
        }

        private static void UpdateIOSProject(string path)
        {
            PBXProject proj = new PBXProject();
            string projPath = PBXProject.GetPBXProjectPath(path);
            proj.ReadFromFile(projPath);

            string mainTargetGuid = string.Empty;
            string unityFrameworkTargetGuid = string.Empty;

#if UNITY_2019_3_OR_NEWER
            mainTargetGuid = proj.GetUnityMainTargetGuid();
            unityFrameworkTargetGuid = proj.GetUnityFrameworkTargetGuid();
            string frameworksPath = path + "/Frameworks/Plugins/iOS/Yodo1Ads/";
            if (Directory.Exists(frameworksPath))
            {
                string[] directories =
                    Directory.GetDirectories(frameworksPath, "*.bundle", SearchOption.AllDirectories);
                for (int i = 0; i < directories.Length; i++)
                {
                    var dirPath = directories[i];
                    var suffixPath = dirPath.Substring(path.Length + 1);
                    var fileGuid = proj.AddFile(suffixPath, suffixPath);
                    proj.AddFileToBuild(mainTargetGuid, fileGuid);

                    fileGuid = proj.FindFileGuidByProjectPath(suffixPath);
                    if (fileGuid != null)
                    {
                        proj.RemoveFileFromBuild(unityFrameworkTargetGuid, fileGuid);
                    }
                }
            }
#else
            mainTargetGuid = proj.TargetGuidByName("Unity-iPhone");
            unityFrameworkTargetGuid = mainTargetGuid;
#endif

#if UNITY_2019_3_OR_NEWER
            var unityFrameworkGuid = proj.FindFileGuidByProjectPath("UnityFramework.framework");
            if (unityFrameworkGuid == null)
            {
                unityFrameworkGuid = proj.AddFile("UnityFramework.framework", "UnityFramework.framework");
                proj.AddFileToBuild(mainTargetGuid, unityFrameworkGuid);
            }

            proj.AddFrameworkToProject(mainTargetGuid, "UnityFramework.framework", false);
            proj.SetBuildProperty(mainTargetGuid, "ENABLE_BITCODE", "NO");
#endif

            proj.SetBuildProperty(unityFrameworkTargetGuid, "ENABLE_BITCODE", "NO");
            proj.AddBuildProperty(unityFrameworkTargetGuid, "OTHER_LDFLAGS", "-ObjC -lxml2");

            // rewrite to file
            File.WriteAllText(projPath, proj.WriteToString());
        }

#endif

        #endregion
    }
}