# Problem One: Trains

## Build

To build application, must install via NuGet, the following packages:

* CommandLine Parser 2.2.1
* NUnit 3.9.0

Next, must compile project with:

* .NET Framework 4.5.2

Run command:

* `msbuild.exe Trains_csharp.sln`

## Unit Tests

Class `Tests/TranisTests.cs` is available to execute unit tests in order to evaluate application logic.

## Execution

This is a command line application. So, must use it with parameters:

`Trains_csharp.exe -r <route of graph text file> -q <set of question parameters>`

### Input Graph
`-r` is the route where is located file with description of graph. E.g.:

- `graphInput.txt`
- `c:\input\graphInput.txt`

**Note:** A file `Input\graphInput.txt` with the example graph is provided inside the solution.

### Question
`-q` must be question type, followed by corresponding parameters. These are allowed:

- Distance of a route `d`

	- "The distance of the route A-B-C": `Trains_csharp.exe -r graphInput.txt -q "d" "a, b, c"`


- Number of maximum trips `tmax` **(not implemented)**
	
	- "Number of trips starting at C and ending at C with a maximum of 3 stops": `Trains_csharp.exe -r "c:\input\graphInput.txt" -q "tmax" "c, c" "3"`


- Number of exact trips `texact` **(not implemented)**
	
	- "Number of trips starting at A and ending at C getting exactly 4 stops": `Trains_csharp.exe -r "graphInput.txt" -q "texact" "a, c" "4"`


- Lenght of the shortest route `l`

	- "The lenght of the shortest route from A to C": `Trains_csharp.exe -r graphInput.txt -q "l" "a, c"`


- Number of different routes `r` **(not implemented)**

	- "The number of different routes from C to C with distance of less than 30": `Trains_csharp.exe -r graphInput.txt -q "r" "c, c" "30"`

### Output

Output looks like:

* `Pregunta:<question>; Salida: <result>`

Where **question** contains a description with question, and **result** the value of the calculus (or "**NO SUCH ROUTE"** instead)