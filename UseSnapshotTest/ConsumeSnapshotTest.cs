using System;
using System.Diagnostics;
using NUnit.Framework;

namespace UseSnapshotTest {
  public class ConsumeSnapshotTest {
    [Test]
    public void TestIfSnapshotTestRunWithPS() {
      var p = Process.Start(
        new ProcessStartInfo( "powershell.exe", "-File Run.ps1" ) {
          WorkingDirectory = "SnapshotTest",
          UseShellExecute = false,
          RedirectStandardError = true,
          RedirectStandardOutput = true,
          CreateNoWindow = true
        } );

      var stdout = p.StandardOutput.ReadToEnd();
      Console.WriteLine( stdout );
      var stderr = p.StandardError.ReadToEnd();
      Console.WriteLine( stderr );
      if ( p.WaitForExit( 5 * 60 * 1000 ) ) {
        Assert.That( p.ExitCode, Is.EqualTo( 0 ), $"Script failed {stderr}" );
      }
      else {
        Assert.Fail( "Failed to start script" );
      }
    }


    [Test]
    public void TestIfSnapshotTestRunCmd() {
      Process cmd = new Process();
      cmd.StartInfo.FileName = "cmd.exe";
      cmd.StartInfo.RedirectStandardInput = true;
      cmd.StartInfo.RedirectStandardOutput = true;
      cmd.StartInfo.CreateNoWindow = true;
      cmd.StartInfo.UseShellExecute = false;
      cmd.StartInfo.WorkingDirectory = "SnapshotTest";
      cmd.StartInfo.Arguments = "/c dotnet vstest SnapshotTest.dll";
      cmd.Start();

      cmd.StandardInput.WriteLine("Running snapshot tests");
      cmd.StandardInput.Flush();
      cmd.StandardInput.Close();
      cmd.WaitForExit();
      Console.WriteLine(cmd.StandardOutput.ReadToEnd());
    }

    [Test]
    public void TestIfConfigitSnapshotTestRunCmd() {
      Process cmd = new Process();
      cmd.StartInfo.FileName = "cmd.exe";
      cmd.StartInfo.RedirectStandardInput = true;
      cmd.StartInfo.RedirectStandardOutput = true;
      cmd.StartInfo.CreateNoWindow = true;
      cmd.StartInfo.UseShellExecute = false;
      cmd.StartInfo.WorkingDirectory = "Service.SnapshotTest";
      cmd.StartInfo.Arguments = "/c dotnet vstest Configit.Snapshot.UnitTest.dll";
      cmd.Start();

      cmd.StandardInput.WriteLine("Running snapshot tests");
      cmd.StandardInput.Flush();
      cmd.StandardInput.Close();
      cmd.WaitForExit();
      Console.WriteLine(cmd.StandardOutput.ReadToEnd());
    }
  }
}
