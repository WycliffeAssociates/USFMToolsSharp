![GitHub](https://img.shields.io/github/license/WycliffeAssociates/USFMToolsSharp?color=blue)
![Travis (.com) branch](https://img.shields.io/travis/com/WycliffeAssociates/USFMToolsSharp/master)
![Nuget](https://img.shields.io/nuget/v/USFMToolsSharp?color=blue)
![Nuget](https://img.shields.io/nuget/dt/USFMToolsSharp?color=blue)

# USFMToolsSharp
A .net parser and rendering toolkit for USFM.

# Description
USFMToolsSharp is a parser and a collection of renderers for .net

# Installation

You can install this package from nuget https://www.nuget.org/packages/USFMToolsSharp/

# Requirements

In the past e targeted .net standard 1.0 to allow use in .net framework. We've since moved past 
that to .net 8 since the performance increases of moving to that are rather significant.


# Building

## From Source

To build USFMToolsSharp from source:

```bash
# Clone the repository
git clone https://github.com/WycliffeAssociates/USFMToolsSharp.git
cd USFMToolsSharp

# Build using .NET CLI
dotnet build

# Run tests
dotnet test
```

Or open `USFMToolsSharp.sln` in Visual Studio and build the solution.

# Contributing

We welcome contributions! Here are some ways you can help:

- **Testing**: Test with various USFM documents and report any parsing or rendering issues
- **Marker Support**: Add support for additional USFM markers to the parser
- **Documentation**: Improve examples and documentation
- **Renderers**: Create new renderers (LaTeX, PDF, EPUB, etc.) or enhance existing ones

Please submit issues for bugs or feature requests, and pull requests for contributions.

# Usage

USFMToolsSharp provides a parser and document model for working with USFM (Unified Standard Format Markers) content. Below are detailed examples to help you get started.

## Installation

Install the package from NuGet:

**.NET CLI**
```bash
dotnet add package USFMToolsSharp
```

**Package Manager Console**
```powershell
Install-Package USFMToolsSharp
```

**PackageReference**
```xml
<PackageReference Include="USFMToolsSharp" Version="*" />
```

Or visit the [NuGet package page](https://www.nuget.org/packages/USFMToolsSharp/).

## Quick Start

```csharp
using USFMToolsSharp;
using USFMToolsSharp.Models.Markers;

// Create a parser
USFMParser parser = new USFMParser();

// Parse USFM content
string usfmContent = @"\id GEN
\h Genesis
\c 1
\v 1 In the beginning God created the heavens and the earth.
\v 2 The earth was without form and void.";

USFMDocument document = parser.ParseFromString(usfmContent);
```

## Core Classes

### USFMParser

The `USFMParser` class converts USFM text into an abstract syntax tree (`USFMDocument`).

#### Basic Parsing

```csharp
USFMParser parser = new USFMParser();
var contents = File.ReadAllText("01-GEN.usfm");
USFMDocument document = parser.ParseFromString(contents);
```

#### Ignoring Specific Markers

You can configure the parser to ignore certain markers during parsing:

```csharp
// Ignore bold markers
var markersToIgnore = new List<string> { "bd", "bd*" };
USFMParser parser = new USFMParser(markersToIgnore);

string usfm = @"\v 1 In the beginning \bd God \bd* created";
USFMDocument document = parser.ParseFromString(usfm);
// The bold markers will be ignored, text "God " will be preserved
```

#### Ignoring Unknown Markers

To ignore markers that aren't part of the USFM specification:

```csharp
// Second parameter controls unknown marker handling
USFMParser parser = new USFMParser(null, ignoreUnknownMarkers: true);
USFMDocument document = parser.ParseFromString(usfmContent);
```

### USFMDocument

The `USFMDocument` class represents a parsed USFM document as a tree structure. Each node in the tree is a `Marker` object.

#### Accessing Document Contents

```csharp
USFMDocument document = parser.ParseFromString(usfmContent);

// Access all top-level markers
List<Marker> contents = document.Contents;

// Get total number of markers parsed
int markerCount = document.NumberOfTotalMarkersAtParse;
```

#### Finding Specific Markers

Use `GetChildMarkers<T>()` to find all markers of a specific type:

```csharp
// Find all chapters in the document
var chapters = document.GetChildMarkers<CMarker>();

foreach (var chapter in chapters)
{
    Console.WriteLine($"Chapter {chapter.Number}");
}

// Find all verses
var verses = document.GetChildMarkers<VMarker>();

foreach (var verse in verses)
{
    Console.WriteLine($"Verse {verse.VerseNumber}");
}

// Find all section headings
var sections = document.GetChildMarkers<SMarker>();
```

#### Working with Chapters and Verses

```csharp
// Get the first chapter
var firstChapter = document.GetChildMarkers<CMarker>().FirstOrDefault();

if (firstChapter != null)
{
    Console.WriteLine($"Chapter {firstChapter.Number}");
    
    // Get verses within this chapter
    var verses = firstChapter.GetChildMarkers<VMarker>();
    
    foreach (var verse in verses)
    {
        // Get the text content of the verse
        var textBlocks = verse.Contents.OfType<TextBlock>();
        string verseText = string.Join("", textBlocks.Select(t => t.Text));
        Console.WriteLine($"  Verse {verse.VerseNumber}: {verseText}");
    }
}
```

#### Merging Documents

You can merge multiple USFM documents together:

```csharp
USFMDocument document1 = parser.ParseFromString(content1);
USFMDocument document2 = parser.ParseFromString(content2);

// Merge document2 into document1
document1.Insert(document2);

// Or insert individual markers
Marker marker = new PMarker();
document1.Insert(marker);

// Insert multiple markers at once
document1.InsertMultiple(listOfMarkers);
```

## Performance Tips

- **Reuse Parser Instances**: Create one parser and reuse it for multiple documents
- **Use Specific Queries**: Use `GetChildMarkers<T>()` instead of traversing all markers
- **Batch Hierarchy Queries**: If you need to get the hierarchy to multiple markers, use `GetHierachyToMultipleMarkers()` instead of calling `GetHierarchyToMarker()` in a loop

# Renderers

## HTMLRenderer 
For more information, please look into the [repository](https://github.com/WycliffeAssociates/USFMToolsSharp.Renderers.HTML). 
> HTML Renderer for USFM
## DocxRenderer
For more information, please look into the [repository](https://github.com/WycliffeAssociates/USFMToolsSharp.Renderers.Docx). 
> Docx Renderer for USFM
## JSONRenderer
For more information, please look into the [repository](https://github.com/WycliffeAssociates/USFMToolsSharp.Renderers.JSON). 
> JSON Renderer for USFM

# Practical Examples

## Extract Verse Text

```csharp
var chapter1 = document.GetChildMarkers<CMarker>().FirstOrDefault(c => c.Number == 1);
var verses = chapter1?.GetChildMarkers<VMarker>();
foreach (var verse in verses)
{
    var text = string.Join("", verse.Contents.OfType<TextBlock>().Select(t => t.Text));
    Console.WriteLine($"{verse.VerseNumber}. {text.Trim()}");
}
```

## Process Footnotes

```csharp
var verses = document.GetChildMarkers<VMarker>();
foreach (var verse in verses)
{
    var footnotes = verse.GetChildMarkers<FMarker>();
    foreach (var footnote in footnotes)
    {
        var refMarker = footnote.Contents.OfType<FRMarker>().FirstOrDefault();
        Console.WriteLine($"Verse {verse.VerseNumber} has footnote: {refMarker?.VerseReference}");
    }
}
```
