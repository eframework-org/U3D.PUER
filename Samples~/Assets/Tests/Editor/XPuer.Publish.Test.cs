// Copyright (c) 2025 EFramework Organization. All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

#if UNITY_INCLUDE_TESTS
using NUnit.Framework;
using EFramework.Editor;
using EFramework.Utility;
using UnityEngine;
using System.Text.RegularExpressions;
using static EFramework.Puer.Editor.XPuer;
using UnityEngine.TestTools;
using UnityEngine.Networking;
using EFramework.Puer;

/// <summary>
/// Publish 的单元测试类，验证发布过程的正确性。
/// </summary>
public class TestXPuerPublish
{
    [Test]
    [PrebuildSetup(typeof(TestXPuerBuild))]
    public void Process()
    {
        // 复制资源到本地
        var buildDir = XFile.PathJoin(XPrefs.GetString(Build.Prefs.Output, Build.Prefs.OutputDefault), XPrefs.GetString(XEnv.Prefs.Channel, XEnv.Prefs.ChannelDefault), XEnv.Platform.ToString());
        if (XFile.HasDirectory(XPuer.Const.LocalPath)) XFile.DeleteDirectory(XPuer.Const.LocalPath);
        XFile.CopyDirectory(buildDir, XPuer.Const.LocalPath);
        Assert.IsTrue(XFile.HasDirectory(XPuer.Const.LocalPath));

        // 设置测试环境
        XPrefs.Asset.Set(Publish.Prefs.Host, "http://localhost:9000");
        XPrefs.Asset.Set(Publish.Prefs.Bucket, "default");
        XPrefs.Asset.Set(Publish.Prefs.Access, "admin");
        XPrefs.Asset.Set(Publish.Prefs.Secret, "adminadmin");
        XPrefs.Asset.Set(Publish.Prefs.LocalUri, "Scripts");
        XPrefs.Asset.Set(Publish.Prefs.RemoteUri, $"TestXPuerPublish/Builds-{XTime.GetMillisecond()}/Scripts");

        var handler = new Publish() { ID = "Test/TestXPuerPublish" };

        // 执行推送脚本
        LogAssert.Expect(LogType.Error, new Regex(@"<ERROR> Object does not exist.*"));
        LogAssert.Expect(LogType.Error, new Regex(@"XEditor.Cmd.Run: finish mc.*"));
        var report = XEditor.Tasks.Execute(handler);

        // 验证Result
        Assert.AreEqual(report.Result, XEditor.Tasks.Result.Succeeded, "脚本发布应当成功");

        var manifestUrl = $"{XPrefs.Asset.GetString(Publish.Prefs.Host)}/{XPrefs.Asset.GetString(Publish.Prefs.Bucket)}/{XPrefs.Asset.GetString(Publish.Prefs.RemoteUri)}/{XMani.Default}";
        var req = UnityWebRequest.Get(manifestUrl);
        req.timeout = 10;
        req.SendWebRequest();
        while (!req.isDone) { }
        Assert.IsTrue(req.responseCode == 200, "资源清单应当请求成功");

        var manifest = new XMani.Manifest();
        Assert.IsTrue(manifest.Parse(req.downloadHandler.text, out _), "资源清单应当读取成功");
    }
}
#endif
