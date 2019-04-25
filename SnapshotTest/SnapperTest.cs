using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Is = Snapper.Json.Nunit.Is;

namespace SnapShotTest {
  public class SnapperTest {
    [Test]
    public void TestIfStoredSnapshotIsMatching() {
      var actual = new JObject {
        { "TestProperty", "TestValue" }
      };
      Assert.That( actual, Is.EqualToSnapshot("NamedSnapshot") );
    }

    [Test]
    public async Task TestIfStoredSnapshotIsMatchingAsync() {
      var actual = new JObject {
        { "TestProperty", "TestValue" }
      };

      // Dummy code to simulate the async call in the problematic project
      await Task.Run( () => Thread.Sleep( 1 ) ).ConfigureAwait( false );

      Assert.That( actual, Is.EqualToSnapshot("NamedSnapshot") );
    }
  }
}