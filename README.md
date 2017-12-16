# ranagams
Find all anagrams of a single word.
----

##### Anagram definition according with [Wikipedia](https://en.wikipedia.org/wiki/Anagram):
>An anagram is word or phrase formed by rearranging the letters of a different word or phrase, typically using all the original letters exactly once.
Any word or phrase that exactly reproduces the letters in another order is an anagram.

###### Examples:
> 1. Elvis
>>*levis,veils,slive,lives,evils*

>2. dictionary
>>*indicatory*

>3. Clint Eastwood
>>*old west action*
-----
### General approach

Given a single word generate all relevant permutations words and then verify their existance in *word list*.

**First phase** will only support finding anagrams that match the exact lenght of the original - examples 1 and 2.

**Second phase** will eventualy support phrase anagrams - example 3.

#### Non-functional goals:

* Support for english but it should be extensible to support other languages.
* Support word lists that are managed in different storage kinds: files, databases, web api.

#### Tools and platfoms
* C#
* .Net Core
* Cloud friendly
  
