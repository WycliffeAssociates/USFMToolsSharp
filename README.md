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

We targeted .net standard 1.0 so .net core 1.0, .net framework 4.5, and mono 4.6 and
higher are the bare minimum.

# Building

With Visual Studio just build the solution. With the .net core tooling use `dotnet build`

# Contributing

Yes please! A couple things would be very helpful

- Testing: Because I can't test every single possible USFM document in existance. If you find something that doesn't look right in the parsing or rendering please submit an issue.
- Adding support for other markers to the parser. There are still plenty of things in the USFM spec that aren't implemented.
- Adding support for other markers to the HTML renderer
- Adding other renderers (LaTeX, PDF, EPUB, JSON, etc.). Some of those renderers might not be possible in .net standard and if that is the case we'll just need to create another repo to contain the renderer

# Usage

USFMToolsSharp provides a parser and document model for working with USFM (Unified Standard Format Markers) content. Below are detailed examples to help you get started.

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

## Common Marker Types

USFMToolsSharp supports a wide range of USFM markers. Here are some of the most commonly used:

### Identification Markers

```csharp
// \id - Book identifier
var idMarker = document.GetChildMarkers<IDMarker>().FirstOrDefault();
if (idMarker != null)
{
    Console.WriteLine($"Book: {idMarker.TextIdentifier}");
}

// \h - Running header text
var headerMarker = document.GetChildMarkers<HMarker>().FirstOrDefault();
if (headerMarker != null)
{
    Console.WriteLine($"Header: {headerMarker.HeaderText}");
}
```

### Title Markers

```csharp
// \mt - Main title (levels 1-3)
var titles = document.GetChildMarkers<MTMarker>();
foreach (var title in titles)
{
    Console.WriteLine($"Title (level {title.Weight}): {title.Title}");
}
```

### Chapter and Verse Markers

```csharp
// \c - Chapter marker
var chapters = document.GetChildMarkers<CMarker>();

// \v - Verse marker
var verses = document.GetChildMarkers<VMarker>();
```

### Section Markers

```csharp
// \s - Section heading (levels 1-5)
var sections = document.GetChildMarkers<SMarker>();
foreach (var section in sections)
{
    Console.WriteLine($"Section (level {section.Weight}): {section.Text}");
}

// \r - Parallel passage reference
var references = document.GetChildMarkers<RMarker>();
```

### Paragraph Markers

```csharp
// \p - Normal paragraph
var paragraphs = document.GetChildMarkers<PMarker>();

// \q - Poetry (levels 1-4)
var poetry = document.GetChildMarkers<QMarker>();
foreach (var line in poetry)
{
    Console.WriteLine($"Poetry (level {line.Depth})");
}

// \m - Margin paragraph (continuation without indentation)
var marginParagraphs = document.GetChildMarkers<MMarker>();
```

### Character Formatting Markers

```csharp
// \bd - Bold text
// \it - Italic text
// \bk - Book name
// \add - Added text
// \wj - Words of Jesus
// \nd - Name of God
// These are typically found within paragraph or verse content

var verse = document.GetChildMarkers<VMarker>().FirstOrDefault();
if (verse != null)
{
    var wordsOfJesus = verse.GetChildMarkers<WJMarker>();
}
```

### Note Markers

```csharp
// \f - Footnote
var footnotes = document.GetChildMarkers<FMarker>();

// \x - Cross reference
var crossRefs = document.GetChildMarkers<XMarker>();
```

## Practical Examples

### Example 1: Extract All Verse Text from a Chapter

```csharp
string usfm = File.ReadAllText("bible-book.usfm");
USFMParser parser = new USFMParser();
USFMDocument document = parser.ParseFromString(usfm);

// Get chapter 1
var chapter1 = document.GetChildMarkers<CMarker>()
    .FirstOrDefault(c => c.Number == 1);

if (chapter1 != null)
{
    var verses = chapter1.GetChildMarkers<VMarker>();
    foreach (var verse in verses)
    {
        var textBlocks = verse.Contents.OfType<TextBlock>();
        string text = string.Join("", textBlocks.Select(tb => tb.Text));
        Console.WriteLine($"Verse {verse.VerseNumber}. {text.Trim()}");
    }
}
```

### Example 2: Generate a Table of Contents

```csharp
USFMDocument document = parser.ParseFromString(usfmContent);

// Get book information
var idMarker = document.GetChildMarkers<IDMarker>().FirstOrDefault();
var toc1 = document.GetChildMarkers<TOC1Marker>().FirstOrDefault();
var toc2 = document.GetChildMarkers<TOC2Marker>().FirstOrDefault();
var toc3 = document.GetChildMarkers<TOC3Marker>().FirstOrDefault();

Console.WriteLine($"Book: {toc1?.LongTableOfContentsText ?? idMarker?.TextIdentifier}");
Console.WriteLine($"Short: {toc2?.ShortTableOfContentsText}");
Console.WriteLine($"Abbr: {toc3?.BookAbbreviation}");

// List all chapters
var chapters = document.GetChildMarkers<CMarker>();
Console.WriteLine($"\nChapters: {chapters.Count}");
```

### Example 3: Find and Process Introduction Material

```csharp
// Introduction markers often come before chapter 1
var introMarkers = new List<string>();

// \imt - Introduction major title
var introTitles = document.GetChildMarkers<IMTMarker>();
foreach (var title in introTitles)
{
    Console.WriteLine($"Intro Title: {title.IntroTitle}");
}

// \ip - Introduction paragraph
var introParagraphs = document.GetChildMarkers<IPMarker>();

// \is - Introduction section heading
var introSections = document.GetChildMarkers<ISMarker>();
```

### Example 4: Working with Footnotes

```csharp
var verses = document.GetChildMarkers<VMarker>();

foreach (var verse in verses)
{
    var footnotes = verse.GetChildMarkers<FMarker>();
    
    foreach (var footnote in footnotes)
    {
        // Get footnote reference
        var refMarker = footnote.Contents.OfType<FRMarker>().FirstOrDefault();
        
        // Get footnote text
        var textMarker = footnote.Contents.OfType<FTMarker>().FirstOrDefault();
        
        Console.WriteLine($"Verse {verse.VerseNumber} - Footnote: {refMarker?.VerseReference}");
    }
}
```

### Example 5: Custom Processing with Marker Traversal

```csharp
// Traverse all markers in the document
void ProcessMarkers(List<Marker> markers, int depth = 0)
{
    foreach (var marker in markers)
    {
        string indent = new string(' ', depth * 2);
        Console.WriteLine($"{indent}{marker.GetType().Name} at position {marker.Position}");
        
        if (marker.Contents.Count > 0)
        {
            ProcessMarkers(marker.Contents, depth + 1);
        }
    }
}

ProcessMarkers(document.Contents);
```

## Error Handling

The parser is designed to be robust, but you should handle potential issues:

```csharp
try
{
    string usfmContent = File.ReadAllText("bible-book.usfm");
    USFMParser parser = new USFMParser();
    USFMDocument document = parser.ParseFromString(usfmContent);
    
    // Check if we got any content
    if (document.Contents.Count == 0)
    {
        Console.WriteLine("Warning: Document has no content");
    }
}
catch (FileNotFoundException)
{
    Console.WriteLine("USFM file not found");
}
catch (Exception ex)
{
    Console.WriteLine($"Error parsing USFM: {ex.Message}");
}
```

## Performance Tips

- **Reuse Parser Instances**: Create one parser and reuse it for multiple documents
- **Use Specific Queries**: Use `GetChildMarkers<T>()` instead of traversing all markers
- **Lazy Processing**: Process markers on-demand rather than loading everything into memory at once

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
