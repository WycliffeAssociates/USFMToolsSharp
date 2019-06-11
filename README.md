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

There a couple useful classes that you'll want to use

## USFMDocument

This class is a tree of objects that represent a USFM document There are a couple of methods and properties that you'll find useful

```csharp
USFMDocument output = new USFMDocument();
// The contents of the document
output.Contents;
// To find all the child markers of a certain type (in this case chapters)
output.GetChildMarkers<CMarker>();
// To merge the contents of one USFMDocument with another
USFMDocument otherDocument = new USFMDocument();
output.Insert(otherDocument);
```

## USFMParser

This class creates an abstract syntax tree from a USFM string. It can also be passed a
list of specific markers as strings into its constructor to ignore them if needed.

Example:

```csharp
USFMParser parser = new USFMParser();
var contents = File.ReadAllText("01-GEN.usfm");
USFMDocument output = parser.ParseFromString(contents);
```

## HtmlRenderer

This class transforms a USFMDocument into an html string

Example:
```csharp
var contents = File.ReadAllText("01-GEN.usfm");
USFMDocument document = parser.ParseFromString(contents);
string html = renderer.Render(document);
File.WriteAllText("output.html", html);

```
# What tags are supported

Eventially the goal is to support the whole USFM spec but for the moment the following common tags are implemented

## By the parser

- id
- ide
- h
- toc1
- toc2
- toc3
- mt
- c
- p
- v
- q
- q1
- q2
- q3
- m
- d
- ms
- cl
- qa
- f
- qa
- nb
- fqa
- pi
- sp
- ft
- bd
- it

## By the html renderer

- p
- c
- v
- q
- q1
- q2
- q3
- m
- bd
- h
- mt
- f