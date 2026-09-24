using Soenneker.Utils.MemoryStream;
using Microsoft.Extensions.Logging.Abstractions;
using Soenneker.Utils.File.Abstract;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Soenneker.GeoNames.Cities500.Data.Tests;

public sealed class GeonamesCities500DataTests
{
    private static readonly IFileUtil _fileUtil = new Soenneker.Utils.File.FileUtil(NullLogger<Soenneker.Utils.File.FileUtil>.Instance, new MemoryStreamUtil());

    [Test]
    public async Task Declares_cities500_resource_for_runner_packaging()
    {
        string projectPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "..", "src",
            "Soenneker.GeoNames.Cities500.Data", "Soenneker.GeoNames.Cities500.Data.csproj"));
        string projectXml = await _fileUtil.Read(projectPath);

        await Assert.That(projectXml).Contains("Resources\\cities500.txt");
    }
}
