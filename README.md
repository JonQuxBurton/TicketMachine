# Ticket Machine

# Overview

The solution consists of the following projects:

## TicketMachine
This is the source code for the Ticket Machine.

To perform the search for matching stations, I have implemented two algorithms which are in the folders BasicStrategy and TreeBasedStrategy.
BasicStrategy is a simple implementation using LINQ, which is used as a baseline for the performance comparison tests. TreeBased Strategy is an implementation using a tree for faster retrieval time at the cost slower start up time (as the tree gets created). 

Diagram of the tree structure
![alt text](https://raw.githubusercontent.com/JonQuxBurton/TicketMachine/master/Tree.png)

The data for the stations is loaded by the code in the Data folder.

## TicketMachine.ConsoleApp
This is console app to interactively test the Ticket Machine. 

It can be run with the following command:

`dotnet run -c Release`

(from a Powershell/command prompt and from the folder \TicketMachine\TicketMachine.ConsoleApp\)

It can also run a performance test where a large number of GetSuggestion() calls are performed and the total execution time returned. This is done for the both Basic and TreeBased strategies. The command for this test is:

`dotnet run -c Release perf`

It can also be run with the Basic strategy (instead of the default TreeBased strategy) using the following command:

`dotnet run -c Release basic`

## TicketMachine.Tests
These are the tests for the TicketMachine.

# Performance Comparisons

## Execution Time

Running the performance tests gave me the following result (in seconds):

|Run #  |Basic Strategy |TreeBased Strategy |
|-------|---------------|-------------------|
|1		  |16.9			      |0.020              |
|2		  |17.3 			    |0.028              |
|3		  |18.8 			    |0.035              |
|Average|17.7			      |0.028              |

So the TreeBased Strategy is considerably faster (approximately ~600 times faster).

## Memory

Using the dotMemory profiler, and performing a search with the letters 'DART' gave me the following results:

|           |.NET Total |
|-----------|-----------|
|Basic      | 321 KB    |
|TreeBased	| 8.74 MB   |

So the TreeBased Strategy uses much more memory than the Basic, however it is still only a small amount (less than 9 MB).



