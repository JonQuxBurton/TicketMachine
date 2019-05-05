# Ticket Machine

# Overview

The solution consists of the following projects:

## TicketMachine
This is the source code for the Ticket Machine.

To perform the search for matching stations, I have implemented two algorithms which are in the folders BasicStrategy and TrieBasedStrategy. BasicStrategy is a simple implementation using LINQ, which is used as a baseline for the performance comparison tests. The TrieBased Strategy is an implementation using a trie for faster retrieval time at the cost slower start up time (as the trie gets created).

The Trie is made up of Letter Nodes which represent each letter of the station name, and Leaf Nodes which contain the full name of a station.

For example, for the stations: Aber, Abercynon, Aberdare Aberdeen, the fragment of the trie is shown in the following diagram.
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
|1		  |16.0			      |0.175              |
|2		  |18.1 			    |0.160              |
|3		  |15.9 			    |0.167              |
|Average|16.7			      |0.167              |

So the TreeBased Strategy is considerably faster (approximately ~100 times faster).

## Memory

Using the dotMemory profiler, and performing a search with the letters 'DART' gave me the following results:

|           |.NET Total |
|-----------|-----------|
|Basic      | 321 KB    |
|TreeBased	| 7.00 MB   |

So the TreeBased Strategy uses much more memory than the Basic, however it is still only a small amount (7 MB).



