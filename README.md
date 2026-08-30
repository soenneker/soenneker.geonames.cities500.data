[![](https://img.shields.io/nuget/v/soenneker.geonames.cities500.data.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.geonames.cities500.data/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.geonames.cities500.data/build-and-test.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.geonames.cities500.data/actions/workflows/build-and-test.yml)
[![](https://img.shields.io/nuget/dt/soenneker.geonames.cities500.data.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.geonames.cities500.data/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.geonames.cities500.data/codeql.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.geonames.cities500.data/actions/workflows/codeql.yml)

# Soenneker.GeoNames.Cities500.Data

A NuGet content package containing a snapshot of GeoNames' `cities500` gazetteer: populated places with more than 500 residents or administrative seats down to PPLA4.

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
    int geonameId = int.Parse(columns[0], CultureInfo.InvariantCulture);
    string name = columns[1];
    double latitude = double.Parse(columns[4], CultureInfo.InvariantCulture);
    double longitude = double.Parse(columns[5], CultureInfo.InvariantCulture);
}
```

For repeated searching and typed US-city records, use `Soenneker.GeoNames.Cities500.Lookup` instead of reparsing the file in application code.

## Row format

Each row follows GeoNames' main `geoname` table schema:

| Index | Field |
| ---: | --- |
| 0 | GeoName ID |
| 1 | Name |
| 2 | ASCII name |
| 3 | Comma-separated alternate names |
| 4 | Latitude in decimal WGS84 degrees |
| 5 | Longitude in decimal WGS84 degrees |
| 6 | Feature class |
| 7 | Feature code |
| 8 | ISO 3166-1 alpha-2 country code |
| 9 | Alternate country codes |
| 10-13 | Administrative division codes 1 through 4 |
| 14 | Population |
| 15 | Elevation in meters |
| 16 | GTOPO30 elevation in meters |
| 17 | IANA time-zone ID |
| 18 | Modification date (`yyyy-MM-dd`) |

Fields can be empty. Do not parse by splitting on spaces or commas; tabs delimit columns, while alternate names themselves are comma-separated.

The upstream schema, feature codes, and latest extracts are documented in the [GeoNames dump readme](https://download.geonames.org/export/dump/).

## Data license and freshness

GeoNames publishes this dataset under [Creative Commons Attribution 4.0](https://creativecommons.org/licenses/by/4.0/). Preserve GeoNames attribution when redistributing or presenting derived data. `GEONAMES-LICENSE.txt` is included in the package alongside the MIT license for the package's software and packaging.

GeoNames publishes updated extracts daily, but a NuGet version is a fixed snapshot. Pin a package version for reproducible deployments and upgrade deliberately when fresher place data is needed. GeoNames provides the data without guarantees of accuracy, timeliness, or completeness.
