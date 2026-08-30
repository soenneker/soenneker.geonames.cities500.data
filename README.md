[![](https://img.shields.io/nuget/v/soenneker.geonames.cities500.data.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.geonames.cities500.data/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.geonames.cities500.data/build-and-test.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.geonames.cities500.data/actions/workflows/build-and-test.yml)
[![](https://img.shields.io/nuget/dt/soenneker.geonames.cities500.data.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.geonames.cities500.data/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.geonames.cities500.data/codeql.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.geonames.cities500.data/actions/workflows/codeql.yml)

# Soenneker.GeoNames.Cities500.Data

A NuGet content package containing a US-only, four-column extract derived from GeoNames' `cities500` gazetteer.

## Install

```bash
dotnet add package Soenneker.GeoNames.Cities500.Data
```

The package has no callable API and requires no service registration. It copies the UTF-8, tab-delimited dataset to the consuming application's output:

```text
Resources/cities500.txt
```

For example:

```csharp
string path = Path.Combine(AppContext.BaseDirectory, "Resources", "cities500.txt");

await foreach (string line in File.ReadLinesAsync(path))
{
    string[] columns = line.Split('\t');
    string city = columns[0];
    string stateCode = columns[1];
    double latitude = double.Parse(columns[2], CultureInfo.InvariantCulture);
    double longitude = double.Parse(columns[3], CultureInfo.InvariantCulture);
}
```

For repeated searching and typed US-city records, use `Soenneker.GeoNames.Cities500.Lookup` instead of reparsing the file in application code.

## Row format

The update runner selects rows whose GeoNames country code is `US` and writes:

| Index | Field |
| ---: | --- |
| 0 | City/place name from the GeoNames `name` field |
| 1 | GeoNames first-level administrative code (US state/territory code) |
| 2 | Latitude in decimal WGS84 degrees |
| 3 | Longitude in decimal WGS84 degrees |

Do not treat this as the complete GeoNames schema: IDs, alternate names, population, feature codes, time zone, and modification date are not retained. Tabs delimit the four output columns.

The upstream schema, feature codes, and latest extracts are documented in the [GeoNames dump readme](https://download.geonames.org/export/dump/).

## Data license and freshness

GeoNames publishes this dataset under [Creative Commons Attribution 4.0](https://creativecommons.org/licenses/by/4.0/). Preserve GeoNames attribution when redistributing or presenting derived data. `GEONAMES-LICENSE.txt` is included in the package alongside the MIT license for the package's software and packaging.

GeoNames publishes updated source extracts daily, but a NuGet version is a fixed transformed snapshot. Pin a package version for reproducible deployments and upgrade deliberately when fresher place data is needed. GeoNames provides the source data without guarantees of accuracy, timeliness, or completeness.
